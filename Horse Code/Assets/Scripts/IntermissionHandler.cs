using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionHandler : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject IntermissionScreen;

    public Text Countdown;

    public float PauseTime = 2.5f;

    private bool _paused;
    private float _elapsedOffTime;

    public void Pause()
    {
        IntermissionScreen.SetActive(true);
        GameScreen.SetActive(false);
        _paused = true;
        _elapsedOffTime = 0.0f;
    }

    void Update ()
    {
        if (_paused)
        {
            Countdown.text = ((int)(PauseTime - _elapsedOffTime)).ToString();
            _elapsedOffTime += Time.deltaTime;
            if (_elapsedOffTime >= PauseTime)
            {
                IntermissionScreen.SetActive(false);
                GameScreen.SetActive(true);
                _paused = false;
            }
        }
    }
}
