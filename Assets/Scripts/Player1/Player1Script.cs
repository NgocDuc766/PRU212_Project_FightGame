using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player1Script : MonoBehaviour
{
    // khởi tạo
    private static Player1Script instance;
    private static readonly object instanceLock = new object();

    private Player2Script Player2Script;

    public static Player1Script GetInstance()
    {
        return instance;
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
    // animator va rigibody
    Rigidbody2D rigi;
    Animator animator;
    // ===============
    [SerializeField]
    private float forceX, forceY;
    bool isFacingRight = true;
    bool isGrounded = true;

    [Header("healthy")]
    public int healthy = 100;
    [Header("power")]
    public int power = 100;
    public int curHealthy = 100;
    private int curPower = 100;
    private bool isDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        lock (instanceLock)
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        gameManager = GameManager.GetIntance();
        curHealthy = healthy;
        curPower = power;
        // get animator and rigidbody
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        // get player2Script
        Player2Script = Player2Script.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        isPlayer1Dead();
    }

    private void MovePlayer()
    {
        movement = Input.GetAxis("Horizontal1");
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

        }
        else
        { // movement > 0 thì hiển thị animation di chuyển  
            animator.SetInteger("speedX", 1);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
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
        //trừ máu đối phương - player2
        if (damage == 2)
        {
            curHealthy = curHealthy - 2;
            curPower += 1;
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
        animator.SetTrigger("isDamaged");
        gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "healing")
        {
            Debug.Log(curHealthy);
            curHealthy += 10;
            gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "power")
        {
            curPower += 10;
            gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            other.gameObject.SetActive(false);
        }
    }
    public void isPlayer1Dead()
    {
        if (curHealthy <= 0 && !isDead)
        {
            animator.SetTrigger("isDead");
            rigi.velocity = Vector2.zero;
            isDead = true;
        }
    }

    public void OnDeathAnimationComplete()
    {
        // Vô hiệu hóa các hành động của player sau khi animation chết hoàn thành
        rigi.isKinematic = true;
        this.enabled = false; // Vô hiệu hóa script điều khiển player
        Time.timeScale = 0;
    }

}