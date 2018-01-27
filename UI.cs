using UnityEngine;
using UnityEngine.UI;

public class GameUI {
	private static Color GREEN = Color.green;
	private static Color RED   = Color.red;

	public GameObject[] playerWords   = new GameObject[2];
	public GameObject[] playerHorses  = new GameObject[2];
	public GameObject[] playerStrikes = new GameObject[2];

	public GameUI(GameObject[] words, GameObject[] horses, GameObject[] strikes) {
		playerWords   = words;
		playerHorses  = horses;
		playerStrikes = strikes;
	}

	public void setPlayerWord(int player, string full_word, string player_correct) {
		playerWords[player].GetComponent<Text>().text = "<color=#FFFFFF>" + player_correct + "</color><color=#777777>" + full_word.Substring(player_correct.Length) + "</color>";
	}

	public void setPlayerHorse(int player, string horse) {
		playerHorses[player].GetComponent<Text>().text = "<color=#FFFFFF>" + horse + "</color><color=#777777>" + ("HORSES").Substring(horse.Length) + "</color>";
	}

	public void setPlayerStrikes(int player, int strikes) {
		playerStrikes[player].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = strikes > 0 ? RED : GREEN;
		playerStrikes[player].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = strikes > 1 ? RED : GREEN;
		playerStrikes[player].transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = strikes > 2 ? RED : GREEN;
	}
}
