﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float attackDamage;
    private Player2Script Player2Script;
    private Player1Script Player1Script;
    private Player1Controll Player1Controll;
    private Player2Controll Player2Controll;

    // Start is called before the first frame update
    void Start()
    {
        Player1Script = Player1Script.GetInstance();
        Player2Script = Player2Script.GetInstance();
        Player1Controll = Player1Controll.GetInstance();
        Player2Controll = Player2Controll.GetInstance();
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
       
        // neu la player1 thi tru dame cua player2
        if(collision.CompareTag("Player2"))
        {

            Debug.Log("tru player2222");
            Debug.Log(collision.gameObject.name);

            // trừ máu player2
            if (collision.gameObject.name == "Gotenk2")
            {
                Player2Controll.Attack(attackDamage);
            } else if (collision.gameObject.name == "FatBuu2")
            {
                Player2Controll.Attack(attackDamage);
            }else if (collision.gameObject.name == "Songoku2")
            {
                Player2Controll.Attack(attackDamage);
                Debug.Log("goncu");

            }
            else
            { 
                Player2Script.Attack(attackDamage);
            }
        }
        // neu la player2 thi tru dame cua player1
        if (collision.CompareTag("Player1"))
        {
            Debug.Log("tru player1111");
            // trừ máu player1
            if (collision.gameObject.name == "Gotenk1")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "FatBuu1")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "Songoku1")
            {
                Player1Controll.Attack(attackDamage);
            }
            else
            {
                Debug.Log("tru scriptplayer1");

                Player1Script.Attack(attackDamage);

            }
        }

    }

}
