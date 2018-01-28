using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsacapeControl : MonoBehaviour {
    public KeyCode EscapeKey;

	void Update () {
        if (Input.GetKey(EscapeKey))
            Application.Quit();
	}
}
