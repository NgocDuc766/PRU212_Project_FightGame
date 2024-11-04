using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlayer1: MonoBehaviour
{
    private Animator animator;
    private static GameManager gameManager;
    private Player1Script Player1Script;
    int updatepower;
    float timeout = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        Player1Script = Player1Script.GetInstance();
        animator = GetComponent<Animator>();
        gameManager = GameManager.GetIntance();
        updatepower = Player1Script.curPower;

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
            if (updatepower >= 20 && Time.time - timeout >= 1f) {
                animator.SetTrigger("isSpecial1");
                updatepower = updatepower - 20;
                Player1Script.SetPower(updatepower);
                timeout = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("isSpecial2");
        }
    }
}
