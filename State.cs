public class PlayerState {
	public string	letters { get; set; }
	public string	horse   { get; set; }
	public int		strikes { get; set; }

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

	private string loadNewWord() {
		return "Norway";
	}

	public void loadNewState() {
		player1State.clearState();
		player2State.clearState();

		word = loadNewWord().ToUpper();
	}
}
