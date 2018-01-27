using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	/// <summary>
	/// The length in seconds for how long a tick/dot takes to register.
	/// </summary>
	public float TickLength = 0.3f;

	/// <summary>
	/// The key on your keyboard for inputting your Morse code.
	/// </summary>
	public KeyCode InputKey;

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
		_errorTolorance = TickLength * 0.5f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(InputKey))
		{
			_pressed = true;
			_elapsedHoldTime += Time.deltaTime;
			_expectedInput = GetExpectedInput(_elapsedHoldTime);
			_elapsedOffTime = 0f;
		}
		else
		{
			_pressed = false;
			_elapsedOffTime += Time.deltaTime;
			_elapsedHoldTime = 0f;
			_currentInput += ExpectedInput;
			_expectedInput = ' ';
		}
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
