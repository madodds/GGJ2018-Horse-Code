using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	/// <summary>
	/// The length in seconds for how long a tick/dot takes to register.
	/// </summary>
	public float TickLength = 0.175f;

	/// <summary>
	/// The key on your keyboard for inputting your Morse code.
	/// </summary>
	public KeyCode InputKey;

	public WordManager WordGod;

	public PlayerState State;

	public bool InputDisabled;

	public GUIText ExpectedOutput;
	public GUIText Output;

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
	public char ExpectedInput
	{
		get { return _expectedInput; }
	}
	private char _expectedInput;

	private float _elapsedHoldTime;
	private float _elapsedOffTime;
	private float _barLength;
	private float _errorTolorance;

	/// <summary>
	/// The current word to solve for.
	/// </summary>
	private string _currentWord;

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
		_pressed = false;
		_expectedInput = ' ';
		_currentInput = "";
		_elapsedOffTime = 0f;
		_elapsedHoldTime = 0f;
		_barLength = TickLength * 3;
		_errorTolorance = TickLength * 1f;
		_currentCharacterIndex = 0;
		_currentWord = " ";
	}

	// Update is called once per frame
	void Update ()
	{
		ExpectedOutput.text = _expectedInput.ToString();
		Output.text = _currentInput;
		if (!InputDisabled)
		{
			if (Input.GetKey(InputKey))
			{
				_pressed = true;
				_elapsedHoldTime += Time.deltaTime;
				_expectedInput = GetExpectedInput(_elapsedHoldTime);
				_elapsedOffTime = 0f;
			}
			else if (_pressed == true)
			{
				_pressed = false;
				_elapsedOffTime += Time.deltaTime;
				_elapsedHoldTime = 0f;
				if (ExpectedInput == '-' || ExpectedInput == '.')
					_currentInput += _expectedInput;
				_expectedInput = ' ';
				if (_currentInput.Length > 0)
				{
					//Debug.Log(string.Format("_currentInput: {0}[{1}] | _currentCharacterCode: {2}|[{3}]", _currentInput, CurrentInput.Length, _currentCharacterCode, _currentCharacterCodeIndex));
					if (_currentInput[CurrentInput.Length - 1] == _currentCharacterCode[_currentCharacterCodeIndex])
					{
						// The specific Morse code character is correct for this character.
						_currentCharacterCodeIndex++;
					}
					else
					{
						// Player failed, reset the players word progress.
						State.Strikes++;
						State.Letters = "";
						_currentCharacterIndex = 0;
						_currentInput = "";
						ResetCharacterProgress(_currentCharacterIndex);
					}
				}
			}
			
			if (_currentCharacterCodeIndex > 0 && _currentCharacterCodeIndex == _currentCharacterCode.Length)
			{
				// This character in the word is complete
				if (_currentCharacterIndex < _currentWord.Length - 1)
				{
					// Move to the next character in the word.
					State.Letters += _currentCharacter;
					_currentCharacterIndex++;
					ResetCharacterProgress(_currentCharacterIndex);
				}
				else
				{
					// The word is complete.
					InputDisabled = true;
				}
			}
		}
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
		Debug.Log(string.Format("_currenstWord: {0}", _currentWord));
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
		if (elapsedTime >= TickLength - _errorTolorance && elapsedTime < TickLength + _errorTolorance)
			return '.';
		else if (elapsedTime >= _barLength - _errorTolorance && elapsedTime < _barLength + _errorTolorance)
			return '-';
		else
			return ' ';
	}
}
