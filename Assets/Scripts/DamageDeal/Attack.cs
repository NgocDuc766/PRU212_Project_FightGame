using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float attackDamage;
    private Player2Script Player2Script;
    private Player1Script Player1Script;

    // Start is called before the first frame update
    void Start()
    {
        Player1Script = Player1Script.GetInstance();
        Player2Script = Player2Script.GetInstance();
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
            // trừ máu player2
            Player2Script.Attack(attackDamage);
        }
        // neu la player2 thi tru dame cua player1
        if (collision.CompareTag("Player1"))
        {
            // trừ máu player1
            Player1Script.Attack(attackDamage);
        }

    }

}
