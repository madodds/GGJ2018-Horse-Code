using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour {
	
	public delegate void ClickAction();
	public static event ClickAction NewWord;

	public int MaxFontSize = 40;
	public float FontSizeGrowthMod = 0.9f;

	public GUIText WordOutputText;

	public GUIText TriviaText;

	public static readonly Dictionary<char, string> MorseCodeKey = new Dictionary<char, string>() {
		{'a', ".-"},
		{'b', "-..."},
		{'c', "-.-."},
		{'d', "-.."},
		{'e', "."},
		{'f', "..-."},
		{'g', "--."},
		{'h', "...."},
		{'i', ".."},
		{'j', ".---"},
		{'k', "-.-"},
		{'l', ".-.."},
		{'m', "--"},
		{'n', "-."},
		{'o', "---"},
		{'p', ".--."},
		{'q', "--.-"},
		{'r', ".-."},
		{'s', "..."},
		{'t', "-"},
		{'u', "..-"},
		{'v', "...-"},
		{'w', ".--"},
		{'x', "-..-"},
		{'y', "-.--"},
		{'z', "--.."},
		{'0', "-----"},
		{'1', ".----"},
		{'2', "..---"},
		{'3', "...--"},
		{'4', "....-"},
		{'5', "....."},
		{'6', "-...."},
		{'7', "--..."},
		{'8', "---.."},
		{'9', "----."},
		{' ', "/" }
	};

	[HideInInspector]
	public CoolWord CurrentWord
	{
		get { return _currentWord; }
	}
	private CoolWord _currentWord;

	private List<CoolWord> _unusedWordList;

	private bool _renderTrivia;

	private float _fontIncreaseAmount; 

	// Use this for initialization
	void Start () {
		Reset();
	}

	private void Reset()
	{
		_unusedWordList = null;
		_renderTrivia = false;
		_currentWord = null;
		TriviaText.fontSize = 1;
		GetNewWord();
	}


	// Update is called once per frame
	void Update () {
		if (_renderTrivia)
		{
			TriviaText.gameObject.SetActive(true);
			if (TriviaText.fontSize < MaxFontSize)
			{
				_fontIncreaseAmount = Time.deltaTime + FontSizeGrowthMod;
				int increase = Mathf.RoundToInt(_fontIncreaseAmount);
				if (increase >= 1)
				{
					_fontIncreaseAmount = 0;
					if (TriviaText.fontSize + increase > MaxFontSize)
					{
						TriviaText.fontSize = MaxFontSize;
					}
					else
					{
						TriviaText.fontSize += increase;
					}
				}
			}
			if (TriviaText.fontSize == MaxFontSize)
			{
				float alphaMod = Time.deltaTime + FontSizeGrowthMod * - 1;
				if (alphaMod < 0)
				{
					alphaMod = 1;
					TriviaText.gameObject.SetActive(false);
					_renderTrivia = false;
				}
				Color myColor = new Color(TriviaText.color.r, TriviaText.color.g, TriviaText.color.b, alphaMod);
				TriviaText.color = myColor;
				
			}
		}
	}

	public void GetNewWord()
	{
		if (_unusedWordList == null || _unusedWordList.Count == 0)
		{
			_unusedWordList = new List<CoolWord>(_wordList);
		}

		if (_currentWord != null /*&& _currentWord.Trivia != ""*/)
		{
			_currentWord.Trivia = "Cool Test!";
			_renderTrivia = true;
			TriviaText.text = _currentWord.Trivia;
		}
		int index = Random.Range(0, _unusedWordList.Count);
		CoolWord word = _unusedWordList[index];
		_unusedWordList.RemoveAt(index);
		_currentWord = word;
		WordOutputText.text = _currentWord.Word;

		if (NewWord != null)
			NewWord();
	}

	public string GetTranslation(string word)
	{
		string output = "";
		foreach(char character in word)
		{
			output += MorseCodeKey[character];
		}
		return output;
	}

	public class CoolWord
	{
		public CoolWord(string word, string trivia)
		{
			Word = word;
			Trivia = trivia;
		}

		public string Word;

		public string Trivia;
	}
	
	private List<CoolWord> _wordList = new List<CoolWord>()
	{
		new CoolWord("Able", ""),
		new CoolWord("Also", ""),
		new CoolWord("Away", ""),
		new CoolWord("Best", ""),
		new CoolWord("Bird", "bird, bird, bird, bird is the word!"),
		new CoolWord("Boat", ""),
		new CoolWord("Calm", ""),
		new CoolWord("Cold", ""),
		new CoolWord("Crew", ""),
		new CoolWord("Dawn", ""),
		new CoolWord("Diet", ""),
		new CoolWord("Duck", ""),
		new CoolWord("Earn", ""),
		new CoolWord("Easy", ""),
		new CoolWord("Evil", ""),
		new CoolWord("Fast", ""),
		new CoolWord("Fate", ""),
		new CoolWord("Free", ""),
		new CoolWord("Gift", ""),
		new CoolWord("Gold", ""),
		new CoolWord("Grow", ""),
		new CoolWord("Hard", ""),
		new CoolWord("Hero", ""),
		new CoolWord("Huge", ""),
		new CoolWord("Inch", ""),
		new CoolWord("Iron", ""),
		new CoolWord("Item", ""),
		new CoolWord("Join", ""),
		new CoolWord("Jump", ""),
		new CoolWord("Just", ""),
		new CoolWord("Kick", ""),
		new CoolWord("Kind", ""),
		new CoolWord("Knee", ""),
		new CoolWord("Land", ""),
		new CoolWord("Last", ""),
		new CoolWord("Luck", ""),
		new CoolWord("Mane", "like a horse’s mane!"),
		new CoolWord("Mind", ""),
		new CoolWord("Move", ""),
		new CoolWord("Name", "that horse’s name was Batholomew, but he didn’t bother to ask."),
		new CoolWord("News", ""),
		new CoolWord("Next", ""),
		new CoolWord("Once", ""),
		new CoolWord("Only", ""),
		new CoolWord("Open", ""),
		new CoolWord("Park", ""),
		new CoolWord("Poll", ""),
		new CoolWord("Pure", ""),
		new CoolWord("Race", ""),
		new CoolWord("Read", ""),
		new CoolWord("Rush", ""),
		new CoolWord("Salt", ""),
		new CoolWord("Save", ""),
		new CoolWord("Stay", ""),
		new CoolWord("Task", ""),
		new CoolWord("Text", ""),
		new CoolWord("True", ""),
		new CoolWord("Unit", ""),
		new CoolWord("User", ""),
		new CoolWord("Vast", ""),
		new CoolWord("Vice", ""),
		new CoolWord("Vote", ""),
		new CoolWord("Walk", ""),
		new CoolWord("Warm", ""),
		new CoolWord("Word", ""),
		new CoolWord("Year", ""),
		new CoolWord("Your", ""),
		new CoolWord("Zero", ""),
		new CoolWord("Zone", ""),

		new CoolWord("Acute", ""),
		new CoolWord("Armor", "Check out our horse armor DLC!"),
		new CoolWord("Avoid", ""),
		new CoolWord("Basic", ""),
		new CoolWord("Break", ""),
		new CoolWord("Build", ""),
		new CoolWord("Catch", ""),
		new CoolWord("Class", ""),
		new CoolWord("Crash", ""),
		new CoolWord("Delay", ""),
		new CoolWord("Doubt", ""),
		new CoolWord("Dream", ""),
		new CoolWord("Earth", "Horses are native to the planets Earth and Unchyakwa"),
		new CoolWord("Enemy", ""),
		new CoolWord("Exist", ""),
		new CoolWord("Final", ""),
		new CoolWord("Flight", "most horses are not capable of flight"),
		new CoolWord("Fresh", ""),
		new CoolWord("Heavy", ""),
		new CoolWord("Horse", "a horse is a horse, of course, of course!"),
		new CoolWord("Ideal", ""),
		new CoolWord("Input", ""),
		new CoolWord("Issue", ""),
		new CoolWord("Joint", ""),
		new CoolWord("Judge", ""),
		new CoolWord("Known", ""),
		new CoolWord("Large", ""),
		new CoolWord("Learn", ""),
		new CoolWord("Limit", ""),
		new CoolWord("Magic", "most horses are not capable of magic"),
		new CoolWord("Minor", ""),
		new CoolWord("Music", ""),
		new CoolWord("Never", ""),
		new CoolWord("Night", ""),
		new CoolWord("Noise", ""),
		new CoolWord("Often", ""),
		new CoolWord("Occur", ""),
		new CoolWord("Ought", ""),
		new CoolWord("Party", ""),
		new CoolWord("Plane", "Have a stroke of my mane, I’ll turn into a plane"),
		new CoolWord("Power", "like HORSEpower!"),
		new CoolWord("Quick", ""),
		new CoolWord("Queen", ""),
		new CoolWord("Quite", ""),
		new CoolWord("Radio", ""),
		new CoolWord("Rapid", ""),
		new CoolWord("River", ""),
		new CoolWord("Scene", ""),
		new CoolWord("Sense", ""),
		new CoolWord("Skill", ""),
		new CoolWord("Teach", ""),
		new CoolWord("Throw", ""),
		new CoolWord("Trust", ""),
		new CoolWord("Under", ""),
		new CoolWord("Unity", ""),
		new CoolWord("Usual", ""),
		new CoolWord("Virus", ""),
		new CoolWord("Visit", ""),
		new CoolWord("Voice", ""),
		new CoolWord("Water", "you can lead a horse to water but you can’t make it drink!"),
		new CoolWord("World", ""),
		new CoolWord("Write", ""),
		new CoolWord("Yield", ""),
		new CoolWord("Young", ""),

		new CoolWord("Access", ""),
		new CoolWord("Answer", ""),
		new CoolWord("Attack", ""),
		new CoolWord("Battle", ""),
		new CoolWord("Belong", ""),
		new CoolWord("Bright", ""),
		new CoolWord("Carrot", "DELICIOUS!"),
		new CoolWord("Center", ""),
		new CoolWord("Crisis", ""),
		new CoolWord("Defend", ""),
		new CoolWord("Desert", "that horse’s name was Batholomew, but he didn’t bother to ask."),
		new CoolWord("Domain", ""),
		new CoolWord("Effort", ""),
		new CoolWord("Engine", ""),
		new CoolWord("Extend", ""),
		new CoolWord("Failed", ""),
		new CoolWord("Finish", ""),
		new CoolWord("Friend", ""),
		new CoolWord("Global", ""),
		new CoolWord("Ground", ""),
		new CoolWord("Guilty", ""),
		new CoolWord("Happen", ""),
		new CoolWord("Height", ""),
		new CoolWord("Honest", ""),
		new CoolWord("Impact", ""),
		new CoolWord("Intent", ""),
		new CoolWord("Invest", ""),
		new CoolWord("Junior", ""),
		new CoolWord("Launch", ""),
		new CoolWord("Leader", ""),
		new CoolWord("Luxury", ""),
		new CoolWord("Manual", ""),
		new CoolWord("Master", ""),
		new CoolWord("Museum", "Visit the Kentucky Horse Park!"),
		new CoolWord("Narrow", ""),
		new CoolWord("Native", "Horses are native to the planets Earth and Unchyakwa"),
		new CoolWord("Notice", ""),
		new CoolWord("Object", ""),
		new CoolWord("Origin", ""),
		new CoolWord("Output", ""),
		new CoolWord("Permit", ""),
		new CoolWord("Planet", "Horses are native to the planets Earth and Unchyakwa"),
		new CoolWord("Public", ""),
		new CoolWord("Random", ""),
		new CoolWord("Record", ""),
		new CoolWord("Repeat", ""),
		new CoolWord("Scheme", ""),
		new CoolWord("Secure", ""),
		new CoolWord("Symbol", ""),
		new CoolWord("Talent", ""),
		new CoolWord("Theory", ""),
		new CoolWord("Threat", ""),
		new CoolWord("Unique", ""),
		new CoolWord("United", ""),
		new CoolWord("Update", ""),
		new CoolWord("Varied", ""),
		new CoolWord("Victim", ""),
		new CoolWord("Visual", ""),
		new CoolWord("Weekly", ""),
		new CoolWord("Window", ""),
		new CoolWord("Worker", ""),

		new CoolWord("Ability", ""),
		new CoolWord("Adverse", ""),
		new CoolWord("Assault", ""),
		new CoolWord("Balance", ""),
		new CoolWord("Believe", ""),
		new CoolWord("Binding", ""),
		new CoolWord("Captain", ""),
		new CoolWord("Chronic", ""),
		new CoolWord("Culture", ""),
		new CoolWord("Deficit", ""),
		new CoolWord("Devoted", ""),
		new CoolWord("Dynamic", ""),
		new CoolWord("Equine", "THAT MEANS HORSE"),
		new CoolWord("Essence", ""),
		new CoolWord("Exhibit", ""),
		new CoolWord("Federal", ""),
		new CoolWord("Finance", ""),
		new CoolWord("Freedom", ""),
		new CoolWord("Gallery", ""),
		new CoolWord("Genuine", ""),
		new CoolWord("Greater", ""),
		new CoolWord("Hanging", ""),
		new CoolWord("History", ""),
		new CoolWord("However", ""),
		new CoolWord("Illegal", ""),
		new CoolWord("Interim", ""),
		new CoolWord("Instead", ""),
		new CoolWord("Journey", ""),
		new CoolWord("Justice", ""),
		new CoolWord("Kingdom", ""),
		new CoolWord("Kitchen", ""),
		new CoolWord("Knowing", ""),
		new CoolWord("Landing", ""),
		new CoolWord("Learned", ""),
		new CoolWord("Liberty", ""),
		new CoolWord("Machine", ""),
		new CoolWord("Massive", ""),
		new CoolWord("Mystery", ""),
		new CoolWord("Network", ""),
		new CoolWord("Neutral", ""),
		new CoolWord("Nowhere", ""),
		new CoolWord("Offense", ""),
		new CoolWord("Operate", ""),
		new CoolWord("Organic", ""),
		new CoolWord("Parking", ""),
		new CoolWord("Pattern", ""),
		new CoolWord("Private", ""),
		new CoolWord("Quality", ""),
		new CoolWord("Quarter", ""),
		new CoolWord("Radical", ""),
		new CoolWord("Reality", ""),
		new CoolWord("Routine", ""),
		new CoolWord("Serious", ""),
		new CoolWord("Similar", ""),
		new CoolWord("Supreme", ""),
		new CoolWord("Thought", ""),
		new CoolWord("Trouble", ""),
		new CoolWord("Typical", ""),
		new CoolWord("Uniform", ""),
		new CoolWord("Upgrade", ""),
		new CoolWord("Utility", ""),
		new CoolWord("Vehice", ""),
		new CoolWord("Violent", ""),
		new CoolWord("Visible", ""),
		new CoolWord("Waiting", ""),
		new CoolWord("Warning", ""),
		new CoolWord("Weather", ""),

		new CoolWord("Absolute", ""),
		new CoolWord("Accuracy", ""),
		new CoolWord("Attitude", ""),
		new CoolWord("Bacteria", ""),
		new CoolWord("Boundary", ""),
		new CoolWord("Business", ""),
		new CoolWord("Campaign", ""),
		new CoolWord("Champion", ""),
		new CoolWord("Coconuts", "A common horse substitute"),
		new CoolWord("Database", ""),
		new CoolWord("Document", ""),
		new CoolWord("Dramatic", ""),
		new CoolWord("Efficacy", ""),
		new CoolWord("Electric", ""),
		new CoolWord("Exposure", ""),
		new CoolWord("Facility", ""),
		new CoolWord("Finished", ""),
		new CoolWord("Frequent", ""),
		new CoolWord("Generous", ""),
		new CoolWord("Graphics", ""),
		new CoolWord("Guidance", ""),
		new CoolWord("Heritage", ""),
		new CoolWord("Hospital", ""),
		new CoolWord("Humanity", ""),
		new CoolWord("Identify", ""),
		new CoolWord("Internal", ""),
		new CoolWord("Isolated", ""),
		new CoolWord("Judgement", ""),
		new CoolWord("Junction", ""),
		new CoolWord("Keyboard", ""),
		new CoolWord("Language", ""),
		new CoolWord("Lemonade", "Sweet Lemonade. Mmmmm, sweet lemonade."),
		new CoolWord("Location", ""),
		new CoolWord("Magnetic", ""),
		new CoolWord("Medicine", ""),
		new CoolWord("Movement", ""),
		new CoolWord("National", ""),
		new CoolWord("Negative", ""),
		new CoolWord("Numerous", ""),
		new CoolWord("Official", ""),
		new CoolWord("Opponent", ""),
		new CoolWord("Optimism", ""),
		new CoolWord("Parallel", ""),
		new CoolWord("Position", ""),
		new CoolWord("Probable", ""),
		new CoolWord("Quantity", ""),
		new CoolWord("Question", ""),
		new CoolWord("Rational", ""),
		new CoolWord("Rigorous", ""),
		new CoolWord("Romantic", ""),
		new CoolWord("Scrutiny", ""),
		new CoolWord("Software", ""),
		new CoolWord("Superior", ""),
		new CoolWord("Tangible", ""),
		new CoolWord("Tomorrow", ""),
		new CoolWord("Tracking", ""),
		new CoolWord("Ultimate", ""),
		new CoolWord("Universe", "Get on my horse, I’ll take you all around the universe"),
		new CoolWord("Variable", ""),
		new CoolWord("Vertical", ""),
		new CoolWord("Volatile", ""),
		new CoolWord("Warranty", ""),
		new CoolWord("Wireless", ""),
		new CoolWord("Withdraw", ""),

		new CoolWord("Abhorrent", ""),
		new CoolWord("Adventure", ""),
		new CoolWord("Beasthood", ""),
		new CoolWord("Briefcase", ""),
		new CoolWord("Captivate", ""),
		new CoolWord("Chevalier", ""),
		new CoolWord("Dexterity", ""),
		new CoolWord("Dupelxity", ""),
		new CoolWord("Ectoplasm", ""),
		new CoolWord("Empathize", ""),
		new CoolWord("Fabricate", ""),
		new CoolWord("Flummoxed", ""),
		new CoolWord("Galloping", "insert horse joke here!"),
		new CoolWord("Gravitate", ""),
		new CoolWord("Haphazard", ""),
		new CoolWord("Hydraulic", ""),
		new CoolWord("Impending", ""),
		new CoolWord("Integrity", ""),
		new CoolWord("Jambalaya", "DELICIOUS!"),
		new CoolWord("Juxtapose", ""),
		new CoolWord("Kerfuffle", ""),
		new CoolWord("Kilometer", ""),
		new CoolWord("Lavashing", ""),
		new CoolWord("Livestock", ""),
		new CoolWord("Malicious", ""),
		new CoolWord("Mortician", ""),
		new CoolWord("Neglected", ""),
		new CoolWord("Nutrition", ""),
		new CoolWord("Obnoxious", ""),
		new CoolWord("Outplayed", "Player X got outplayed!"),
		new CoolWord("Persuaded", ""),
		new CoolWord("Prevalent", ""),
		new CoolWord("Quadratic", ""),
		new CoolWord("Quixotism", "Say what?!"),
		new CoolWord("Recollect", ""),
		new CoolWord("Roguishly", ""),
		new CoolWord("Scapegoat", ""),
		new CoolWord("Shrubbery", "Ni!"),
		new CoolWord("Tactician", ""),
		new CoolWord("Telegraph", ""),
		new CoolWord("Ultimatum", ""),
		new CoolWord("Violation", ""),
		new CoolWord("Whimsical", ""),
		new CoolWord("Xylophone", ""),

		new CoolWord("Yegorlykskaya", "population: 17,660"),


	};
}
