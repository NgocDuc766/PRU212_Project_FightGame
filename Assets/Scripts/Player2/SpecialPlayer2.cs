using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlayer2 : MonoBehaviour
{
    private Animator animator;
    private static GameManager gameManager;
    private Player2Script Player2Script;
    int updatepower;
    float timeout = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player2Script = Player2Script.GetInstance();
        gameManager = GameManager.GetIntance();
        updatepower = Player2Script.curPower;
    }

    // Update is called once per frame
    void Update()
    {
        SpecialAttack1();
    }
    public bool isUsingSkillS;

    private void SpecialAttack1()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (updatepower >= 20 && Time.time - timeout >= 1f)
            {
                animator.SetTrigger("isSpecial1");
                updatepower = updatepower - 20;
                Player2Script.SetPower(updatepower);
                timeout = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("isSpecial2");
        }
    }
}
