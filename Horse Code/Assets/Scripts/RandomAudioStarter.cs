using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioStarter : MonoBehaviour {
	void Start () {
        GetComponent<AudioRandomizer>().PlayNoise();
	}
}
