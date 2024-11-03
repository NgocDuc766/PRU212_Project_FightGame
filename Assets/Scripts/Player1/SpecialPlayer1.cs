using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlayer1: MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SpecialAttack1();
    }

    private void SpecialAttack1()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("isSpecial1");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("isSpecial2");
        }
    }
}
