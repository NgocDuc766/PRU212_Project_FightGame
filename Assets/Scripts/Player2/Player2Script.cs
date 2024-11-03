using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    // khởi tạo
    private static Player2Script instance;
    private static readonly object instanceLock = new object();

    public static Player2Script GetInstance()
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

    private float movement;
    private static GameManager gameManager;
    Rigidbody2D rigi;
    public Animator animator;
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

    private bool isDead= false;
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
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        isPlayer2Dead();
    }

    private void MovePlayer()
    {
        movement = Input.GetAxis("Horizontal2");
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
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
    // attack trừ máu đối phương - player1
    public void Attack(float damage)
    {
        //trừ máu đối phương - player2
        if (damage == 2)
        {
            curHealthy = curHealthy - 2;
            curPower += 1;
            Debug.Log("-2" + "va" + curHealthy);
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
        gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
    }

    public void isPlayer2Dead()
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
        // Vô hiệu hóa các hành động của player2 sau khi animation chết hoàn thành
        rigi.isKinematic = true;
        this.enabled = false; // Vô hiệu hóa script điều khiển player
        Time.timeScale = 0;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "healing")
        {
            curHealthy += 10;
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "power")
        {
            curPower += 10;
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            other.gameObject.SetActive(false);
        }
    }     
}
