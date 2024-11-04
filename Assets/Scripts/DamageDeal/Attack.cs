using System.Collections;
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
        if (collision.CompareTag("Player2"))
        {

            Debug.Log(collision.gameObject.name);

            // trừ máu player2
            if (collision.gameObject.name == "Gotenk2(Clone)")
            {
                Player2Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "FatBuu2(Clone)")
            {
                Player2Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "Songoku2(Clone)")
            {
                Player2Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "scale")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "bulletGoku(Clone)")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "scale")
            {
                Player1Controll.Attack(attackDamage);
            }
            else
            {
                Player2Script.Attack(attackDamage);
            }
        }
        // neu la player2 thi tru dame cua player1
        if (collision.CompareTag("Player1"))
        {
            // trừ máu player1
            if (collision.gameObject.name == "Gotenk1(Clone)")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "FatBuu1(Clone)")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "Songoku1(Clone)")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "bulletGoku(Clone)")
            {
                Player1Controll.Attack(attackDamage);
            }
            else if (collision.gameObject.name == "scale")
            {
                Player1Controll.Attack(attackDamage);
            }
            else
            {
                Player1Script.Attack(attackDamage);
            }
        }

    }

}
