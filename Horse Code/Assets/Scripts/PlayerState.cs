using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

	public string Letters { get; set; }
	public string Horse { get; set; }
	public int Strikes { get; set; }
	public int HorseCount { get; set; }

	public GUIText StrikesText;
	public GUIText LettersText;
	public GUIText HorseText;

	public Controls Player;

	// Use this for initialization
	void Start () {
		Reset();
	}

	private void Reset()
	{
		HorseCount = 0;
		Horse = "";
		Strikes = 0;
		Letters = "";
	}

	// Update is called once per frame
	void Update () {
		StrikesText.text = Strikes.ToString();
		LettersText.text = Letters;
		HorseText.text = Horse;
		if (Strikes >= 3)
		{
			HorseCount++;
			Horse = "HORSE".Substring(0, HorseCount);
			Strikes = 0;
		}
		if (HorseCount == "HORSE".Length)
			Player.InputDisabled = true;
	}
}
