using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
	private static readonly Color GREEN = Color.green;
	private static readonly Color RED   = Color.red;

	public GameObject playerWord;
	public GameObject playerMorse;
	public GameObject playerHorse;
	public GameObject playerStrikes;

	public void setPlayerWord(string full_word, int player_correct) {
		playerWord.GetComponent<Text>().text = "<color=#FFFFFF>" + full_word.Substring(0, player_correct) + "</color><color=#777777>" + full_word.Substring(player_correct) + "</color>";
	}

	public void setPlayerHorse(int horse) {
		playerHorse.GetComponent<Text>().text = "<color=#FFFFFF>" + ("HORSES").Substring(0, horse) + "</color><color=#777777>" + ("HORSES").Substring(horse) + "</color>";
	}

	public void setPlayerStrikes(int strikes) {
		playerStrikes.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = strikes > 0 ? RED : GREEN;
		playerStrikes.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = strikes > 1 ? RED : GREEN;
		playerStrikes.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = strikes > 2 ? RED : GREEN;
	}

	public void setPlayerMorse(string code) {
        string fmt_code = "";

        for (int i=0;i<code.Length;i++)
            fmt_code += code[i] + " ";

        playerMorse.GetComponent<Text>().text = fmt_code;
    }
}
