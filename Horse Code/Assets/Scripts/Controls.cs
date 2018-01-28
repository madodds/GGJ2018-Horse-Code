using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
	private static bool WinPause = false;
	/// <summary>
	/// The length in seconds for how long a tick/dot takes to register.
	/// </summary>
	public float TickLength = 0.175f;

	/// <summary>
	/// The key on your keyboard for inputting your Morse code.
	/// </summary>
	public string AxisName;

	public WordManager WordGod;

	public PlayerState State = new PlayerState();

    public GameUI UI;
    public Controls OpponentControls;

    public AudioRandomizer Neigh;
    public AudioRandomizer Strike;

    public bool InputDisabled = false;

	public GameObject ExpectedOutput;
	public GameObject Output;
    public GameObject WinnerScreen;
	public GameObject LoserScreen;

    public IntermissionHandler IntermissionGod;

	public delegate void ResetGame();
	public static event ResetGame RestartGame;

	public float RestartAfterElapsedTime = 5f;

	[HideInInspector]
	public bool Pressed
	{
		get { return _pressed; }
	}
	private bool _pressed = false;

	[HideInInspector]
	public string CurrentInput
	{
		get { return _currentInput; }
	}
	private string _currentInput;
    
	[HideInInspector]
	public string DisplayInput
	{
		get { return _displayInput; }
	}
	private string _displayInput;

	[HideInInspector]
	public char ExpectedInput
	{
		get { return _expectedInput; }
	}
	private char _expectedInput;

	private float _elapsedHoldTime;
	private float _elapsedOffTime;

	/// <summary>
	/// The current word to solve for.
	/// </summary>
	private string _currentWord = "";

	/// <summary>
	/// The index of <see cref="_currentWord"/> the player is trying to solve.
	/// </summary>
	private int _currentCharacterIndex;

	/// <summary>
	/// The current character the player is trying to solve for.
	/// </summary>
	private char _currentCharacter;

	/// <summary>
	///  The sequence of Morse code characters to represent <see cref="_currentCharacter"/>.
	/// </summary>
	private string _currentCharacterCode;

	/// <summary>
	/// The index of <see cref="_currentCharacterCode"/> the player is trying to solve.
	/// </summary>
	private int _currentCharacterCodeIndex;


	// Use this for initialization
	void Start () {
		Reset();
	}

	public void Reset()
    {
        State.strikes = 0;
        State.letters = "";
        _pressed = false;
		_expectedInput = ' ';
		_currentInput = "";
        _displayInput = "";
        _elapsedOffTime = 0f;
		_elapsedHoldTime = 0f;
		_currentCharacterIndex = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		ExpectedOutput.GetComponent<Text>().text = _expectedInput.ToString();
		Output.GetComponent<Text>().text = _currentInput;
		if (State.horse.Length == 5 || OpponentControls.State.horse.Length == 5) {
			_currentWord = "";

			if (OpponentControls.State.horse.Length == 5)
			{
				WinnerScreen.SetActive(true);
				LoserScreen.SetActive(false);
			}
			else if (State.horse.Length == 5)
			{
				LoserScreen.SetActive(true);
				WinnerScreen.SetActive(false);
			}
			_elapsedOffTime += Time.deltaTime;
			if (_elapsedOffTime >= RestartAfterElapsedTime)
			{
				Reset();
				RestartGame();
				State.clearState();

				WinnerScreen.SetActive(false);
				LoserScreen.SetActive(false);
				OpponentControls.WinnerScreen.SetActive(false);
				OpponentControls.LoserScreen.SetActive(false);

				if (!WinPause)
				{
					WinPause = true;
				}
				else
				{
					IntermissionGod.Pause();
					WinPause = false;
				}
			}
		} else {
		    WinnerScreen.SetActive(false);
			if (Input.GetAxis(AxisName) != 0f)
			{
				_pressed = true;
				_elapsedHoldTime += Time.deltaTime;
				_expectedInput = GetExpectedInput(_elapsedHoldTime);
			}
			else if (_pressed == true)
			{
				_pressed = false;
				_elapsedHoldTime = 0f;
				if (ExpectedInput == '_' || ExpectedInput == '.')
					_currentInput += _expectedInput;
                if (_currentInput.Length > 0)
				{
					//Debug.Log(string.Format("_currentInput: {0}[{1}] | _currentCharacterCode: {2}|[{3}]", _currentInput, CurrentInput.Length, _currentCharacterCode, _currentCharacterCodeIndex));
					if (_currentInput[CurrentInput.Length - 1] == _currentCharacterCode[_currentCharacterCodeIndex])
					{
                        // The specific Morse code character is correct for this character.
                        _displayInput += _expectedInput;
                        _currentCharacterCodeIndex++;
					}
					else
                    {
                        // Player failed, reset the players word progress.
                        State.strikes++;
						State.letters = "";
						_currentCharacterIndex = 0;
						_currentInput = "";
                        _displayInput = "";
                        ResetCharacterProgress(_currentCharacterIndex);

                        if (State.strikes == 3) {
                            State.horse += "_";
                            Neigh.PlayNoise();
                            Reset();
                            OpponentControls.Reset();
                            WordGod.GetNewWord();

                            if (State.horse.Length != 5) {
                                OpponentControls.WinnerScreen.SetActive(true);
                                IntermissionGod.Pause();
                            }
                        } else {
                            Strike.PlayNoise();
                        }
					}
                }
                _expectedInput = ' ';
            }
			
			if (_currentCharacterCodeIndex > 0 && _currentCharacterCodeIndex == _currentCharacterCode.Length)
			{
                // This character in the word is complete
                State.letters += _currentCharacter;
                _displayInput = "";

                if (_currentCharacterIndex < _currentWord.Length - 1)
				{
					// Move to the next character in the word.
					_currentCharacterIndex++;
					ResetCharacterProgress(_currentCharacterIndex);
				}
				else
				{
					// The word is complete.
					OpponentControls.State.horse += "_";
                    Neigh.PlayNoise();
                    Reset();
                    OpponentControls.Reset();
                    WordGod.GetNewWord();

                    if (OpponentControls.State.horse.Length != 5) {
                        WinnerScreen.SetActive(true);
                        IntermissionGod.Pause();
                    }
                }
			}
		}
        
        UI.setPlayerWord   (_currentWord.ToUpper(), State.letters.Length);
        UI.setPlayerMorse  (_displayInput);
        UI.setPlayerHorse  (State.horse.Length);
        UI.setPlayerStrikes(State.strikes);
	}

	private void OnEnable()
	{
		WordManager.NewWord += MyEvent;
	}

	private void OnDisable()
	{
		WordManager.NewWord -= MyEvent;
	}

	private void MyEvent()
	{
		_currentWord = WordGod.CurrentWord.Word.ToLower();
		//Debug.Log(string.Format("_currentWord: {0}", _currentWord));
		_currentCharacterIndex = 0;
		ResetCharacterProgress(_currentCharacterIndex);
	}

	private void ResetCharacterProgress(int currentCharacterIndex)
	{
		_currentCharacter = _currentWord[currentCharacterIndex];
        _currentCharacterCode = WordManager.MorseCodeKey[_currentCharacter];
		_currentCharacterCodeIndex = 0;
	}

	private char GetExpectedInput(float elapsedTime)
	{
        if (elapsedTime < TickLength * 3)
            return '.';
        else
            return '_';
/*		if (elapsedTime >= TickLength - _errorTolorance && elapsedTime < TickLength + _errorTolorance)
			return '.';
		else if (elapsedTime >= _barLength - _errorTolorance && elapsedTime < _barLength + _errorTolorance)
			return '-';
		else
			return ' ';*/
	}
}
