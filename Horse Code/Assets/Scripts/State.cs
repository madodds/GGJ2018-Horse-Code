using UnityEngine;

public class PlayerState {
	public string  letters { get; set; }
	public string  horse   { get; set; }
	public int	   strikes { get; set; }

    public PlayerState() {
        clearState();
    }

	public void clearState() {
		letters = "";
		horse   = "";
		strikes = 0;
	}
}

public class LogicState {
	public PlayerState	player1State;
	public PlayerState	player2State;
	public string		word;

	public LogicState() {
		player1State = new PlayerState();
		player2State = new PlayerState();

		loadNewState();
	}

	public string loadNewWord() {
		return "Norway".ToUpper();
	}

	public void loadNewState() {
		player1State.clearState();
		player2State.clearState();

		word = loadNewWord();
	}

/*    public bool strikePlayer(int player) {
        PlayerState state = player == 0 ? player1State : player2State;

        if (state.strikes++ == 2) {
            player1State.strikes = 0;
            player2State.strikes = 0;
            player1State.letters = 0;
            player2State.letters = 0;
            state.horse++;
            word = loadNewWord();

            return true;
        }

        return false;
    }

    public bool acceptLetter(int player) {
        PlayerState state = player == 0 ? player1State : player2State;
        PlayerState other = player == 0 ? player2State : player1State;

        if (state.letters++ == word.Length - 1) {
            player1State.strikes = 0;
            player2State.strikes = 0;
            player1State.letters = 0;
            player2State.letters = 0;
            other.horse++;
            word = loadNewWord();

            return true;
        }

        return false;
    }*/
}
