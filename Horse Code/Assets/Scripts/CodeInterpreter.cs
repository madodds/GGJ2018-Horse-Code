using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInterpreter : MonoBehaviour {

	public Controls PlayerContols;
	public WordManager WordGod;

	private int _currentCharacterIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
		_currentCharacterIndex = 0;

	}
}
