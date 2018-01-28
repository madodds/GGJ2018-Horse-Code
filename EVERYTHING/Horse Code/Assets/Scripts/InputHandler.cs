using UnityEngine;

public class InputHandler {
    public KeyCode  keyCode;
    public int      dashLatency;

    public int      keyTimer = 0;
    public bool     keyDown = false;
    public string   currentLetter = "";

    public InputHandler(KeyCode key, float dashTime) {
        keyCode     = key;
        dashLatency = (int) (dashTime * 1000.0f);
    }

    public void update() {
		if (Input.GetKeyDown(keyCode)) {
            keyDown = true;
        } else if (Input.GetKeyUp(keyCode)) {
            if (keyTimer != 0 && keyTimer < dashLatency)
                currentLetter += ".";
            else if (keyTimer >= dashLatency)
                currentLetter += "_";

            keyTimer = 0;
            keyDown  = false;
        }

        if (keyDown)
            keyTimer += (int) (Time.deltaTime * 1000.0f);
    }
}