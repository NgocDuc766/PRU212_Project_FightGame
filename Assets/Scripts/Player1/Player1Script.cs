﻿using DG.Tweening.Core.Easing;
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
    private int curHealthy = 100;
    private int curPower = 100;


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
        gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
    }

}