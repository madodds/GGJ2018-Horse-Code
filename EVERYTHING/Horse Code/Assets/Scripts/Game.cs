using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	//Assigned in the engine
	public GameObject[]	words;
    public GameObject[] morses;
    public GameObject[]	horses;
	public GameObject[]	strikeContainers;
/*    public GameObject   winner;
    public GameObject   winnerText;

	//Assigned at start
	public GameUI		ui;
	public LogicState	state;
    public Decoder      decoder;
    public InputHandler player1Input;
    public InputHandler player2Input;

    private string player1CharMorse;
    private string player2CharMorse;

    void Start() {
		ui           = new GameUI(words, morses, horses, strikeContainers);
		state        = new LogicState();
        decoder      = new Decoder();
        player1Input = new InputHandler(KeyCode.A, 0.3f);
        player2Input = new InputHandler(KeyCode.L, 0.3f);

        player1CharMorse = ".__";
        player2CharMorse = ".__";
    }

	void Update() {
        if (state.player1State.horse != 5 && state.player2State.horse != 5) {
            player1Input.update();
            player2Input.update();

            ui.setPlayerWord   (0, state.word, state.player1State.letters);
            ui.setPlayerWord   (1, state.word, state.player2State.letters);
		    ui.setPlayerHorse  (0, state.player1State.horse);
		    ui.setPlayerHorse  (1, state.player2State.horse);
		    ui.setPlayerStrikes(0, state.player1State.strikes);
		    ui.setPlayerStrikes(1, state.player2State.strikes);
            ui.setPlayerMorse  (0, player1Input.currentLetter);
            ui.setPlayerMorse  (1, player2Input.currentLetter);

            if (player1Input.currentLetter.Length > 0 && state.player1State.strikes < 3) {
                if (player1Input.currentLetter[player1Input.currentLetter.Length - 1] != player1CharMorse[player1Input.currentLetter.Length - 1]) {
                    player1Input.currentLetter = "";

                    if (state.strikePlayer(0))
                        player2Input.currentLetter = "";
                }

                if (player1Input.currentLetter == player1CharMorse) {
                    player1Input.currentLetter = "";
                    state.acceptLetter(0);
                }
            }

            if (player2Input.currentLetter.Length > 0) {
                if (player2Input.currentLetter[player2Input.currentLetter.Length - 1] != player2CharMorse[player2Input.currentLetter.Length - 1]) {
                    player2Input.currentLetter = "";

                    if (state.strikePlayer(1))
                        player1Input.currentLetter = "";
                }

                if (player2Input.currentLetter == player2CharMorse) {
                    player2Input.currentLetter = "";
                }
            }
        } else {
            winner.SetActive    (true);
            winnerText.SetActive(true);

            winnerText.GetComponent<Text>().text = state.player1State.horse == 5 ? "Player 2!" : "Player 1!";
        }
	}*/
}

