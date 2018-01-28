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

	private void OnEnable()
	{
		Controls.RestartGame += clearState;
	}

	private void OnDisable()
	{
		Controls.RestartGame -= clearState;
	}
}
