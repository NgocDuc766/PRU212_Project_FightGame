using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    // khởi tạo
    private static PlayerScript instance ;
    private static readonly object  instanceLock = new object();

    public static PlayerScript GetInstance()
    {
                return instance;
    }

    private PlayerScript()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    /*
     Normal = 0,
     Damaged = 1,
     Die = 2
     */
    public enum StatePlayer
    {
        Normal,
        Damaged,
        Die
    }

    private float vertical1, movement;
    private static GameManager gameManager;
    [SerializeField]
    Rigidbody2D rigi;
    [SerializeField]
    Animator animator;
    [SerializeField]
    private float forceX, forceY;
    bool isFacingRight = true;
    bool isGrounded = true;

    [Header("healthy")]
    public int healthy = 100;
    [Header("power")]
    public int power = 100;
    private int curHealthy = 100;
    private int curPower =100;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetIntance();
        Debug.Log("abcd");
        curHealthy = healthy;
        curPower = power;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        movement = Input.GetAxis("Horizontal1");
        vertical1 = Input.GetAxis("Vertical1");
        // move 
        rigi.velocity = new Vector2(forceX * movement, rigi.velocity.y);
        Vector3 CharacterScale = transform.localScale;

        if (isFacingRight == true && movement < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(CharacterScale.x), CharacterScale.y, 1);
            isFacingRight = false;
        }
        if (isFacingRight == false && movement > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(CharacterScale.x), CharacterScale.y, 1);
            isFacingRight = true;
        }

        if (movement == 0)
        { // nếu movement = 0 thì đứng yên
            animator.SetInteger("speedX", 0);
            //dashanimator.SetBool("dash_effect", false);

        }
        else
        { // movement > 0 thì hiển thị animation di chuyển  
            animator.SetInteger("speedX", 1);
            //dashanimator.SetBool("dash_effect", true);
        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, forceY);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        animator.SetFloat("speedY", rigi.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    public void Attack(float damage)
    {
        //trừ máu player2
        if(damage == 2)
        {
            curHealthy = curHealthy - 2;
            curPower += 1;
            Debug.Log("-2" + "va" + curHealthy );
        }
        if (damage == 4)
        {
            curHealthy -= 4;
            curPower += 1;
        }
        if (damage == 5)
        {
            curHealthy -= 2;
            curPower += 1;
        }
        if (damage == 10)
        {
            curHealthy -= 2;
            curPower += 1;
        }
        if(gameManager == null)
        {
            Debug.Log("gamemanager null");
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
        }
        else
        {
            Debug.Log("gamemanager not null");
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);


        }

    }

}
