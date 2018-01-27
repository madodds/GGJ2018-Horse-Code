using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	//Assigned in the engine
	public GameObject[]	words;
	public GameObject[]	horses;
	public GameObject[]	strikeContainers;

	//Assigned at start
	public GameUI		ui;
	public LogicState	state;

	void Start() {
		ui    = new GameUI(words, horses, strikeContainers);
		state = new LogicState();
	}

	void Update() {
		ui.setPlayerWord   (0, state.word, state.player1State.letters);
		ui.setPlayerWord   (1, state.word, state.player2State.letters);
		ui.setPlayerHorse  (0, state.player1State.horse);
		ui.setPlayerHorse  (1, state.player2State.horse);
		ui.setPlayerStrikes(0, state.player1State.strikes);
		ui.setPlayerStrikes(1, state.player2State.strikes);
	}
}

