using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse1Script : MonoBehaviour
{
	public KeyCode InputKey;

	private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey(InputKey))
            {
                animator.SetBool("Button", true);
            }
            else     
                animator.SetBool("Button", false);        
    }
}
