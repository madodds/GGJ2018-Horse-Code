using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour {
	
	public delegate void ClickAction();
	public static event ClickAction NewWord;

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
	private List<CoolWord> _wordList = new List<CoolWord>()
	{
		new CoolWord("Banana", "Banana Trivia"),
		new CoolWord("Sandwich", "Sandwich Trivia"),
		new CoolWord("Hope", "Hope Trivia")
	};

	// Use this for initialization
	void Start () {
		Reset();
	}

	private void Reset()
	{
		_unusedWordList = null;
		GetNewWord();
	}


	// Update is called once per frame
	void Update () {

	}

	public void GetNewWord()
	{
		if (_unusedWordList == null || _unusedWordList.Count == 0)
		{
			_unusedWordList = new List<CoolWord>(_wordList);
		}
		int index = Random.Range(0, _unusedWordList.Count);
		CoolWord word = _unusedWordList[index];
		_unusedWordList.RemoveAt(index);
		_currentWord = word;

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
}
