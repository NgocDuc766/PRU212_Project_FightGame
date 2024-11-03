using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float attackDamage;
    private PlayerScript PlayerScript ;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = PlayerScript.GetInstance();
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
            Debug.Log("player2 - " +  attackDamage);
            if(PlayerScript == null)
            {
                Debug.Log("a");
            }
            PlayerScript.Attack(attackDamage);
        }
        // neu la player2 thi tru dame cua player1
        if (collision.CompareTag("Player1"))
        {
            // trừ máu player2
            Debug.Log("player1 - " + attackDamage);
        }

    }

}
