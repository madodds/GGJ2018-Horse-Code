using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamScript : MonoBehaviour
{
	/// <summary>
	/// The key on your keyboard for inputting your Morse code.
	/// </summary>
	public string AxisName;

	public AudioClip[] Clips;

	private Animator animator;

	private bool isDown = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
		{
			if (Input.GetAxis(AxisName) != 0f && !isDown)
			{
				isDown = true;
				animator.SetTrigger("Whatever");
				GetComponent<AudioSource>().PlayOneShot(Clips[Random.Range(0, Clips.Length - 1)]);
			}
			else if (Input.GetAxis(AxisName) == 0f)
			{
				isDown = false;
			}
			//animator.SetTrigger("Whatever");
		}
    }
}
