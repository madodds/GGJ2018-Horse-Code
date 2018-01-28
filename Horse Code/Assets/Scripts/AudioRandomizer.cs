using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour {
	public AudioClip[] clips;

	public void PlayNoise() {
		GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
	}
}
