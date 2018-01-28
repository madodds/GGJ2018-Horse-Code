﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamScript : MonoBehaviour
{
	public KeyCode InputKey;

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
        {
            if (Input.GetKeyDown(InputKey))
            {
                animator.SetTrigger("Whatever");
            }
                //animator.SetTrigger("Whatever");
        }
    }
}