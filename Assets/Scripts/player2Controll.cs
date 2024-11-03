using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XXX.UI.Popup;

public class Player2Controll : MonoBehaviour
{
    public enum StatePlayer
    {
        Normal,
        Damaged,
        Die
    }

    private StatePlayer statePlayer;
    private static Player2Controll instance;
    private static readonly object instanceLock = new object();

    public static Player2Controll GetInstance()
    {
        return instance;
    }

    private Player1Controll player1script;

    [HideInInspector] public Rigidbody2D rib;
    [HideInInspector] public Animator ani;
    [Header("healthy")]
    public int healthy = 100;
    [Header("speed")]
    public float speedMove = 5, speedJump = 26;
    [Header("power")]
    public int power = 100;
    [Header("damagedPower")]


    public GameManager gameManager;

    private float horizontal1, vertical1;
    private float scaleX, scaleY, gravity;
    private bool isGround, hit, kick, superHit, superKick, jump;
    public int curHealthy, curPower;
    [HideInInspector] public bool isDamaged;
    //temp
    private float timeHit, timeKick, timeJump;

    // Use this for initialization
    void Awake()
    {
        lock (instanceLock)
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        player1script = GameObject.FindGameObjectWithTag("Player2")?.GetComponent<Player1Controll>();
        gameManager = GameManager.GetIntance();
        statePlayer = StatePlayer.Normal; // khởi tạo trạng thái ban đầu cho nhân vật 
        rib = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        gravity = rib.gravityScale;

        curHealthy = healthy;
        curPower = 100;
        gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.GetIntance().state != GameState.Playing)
        //      {
        //	return;
        //      }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0); // 
        MovePlayer();
        AttackEnemy();
        //}
        Blocking();
        ChangeAnimation();

    }


    private void MovePlayer()
    {
        horizontal1 = Input.GetAxis("Horizontal2");
        vertical1 = Input.GetAxis("Vertical2");
        //move
        if ((Input.GetKey(KeyCode.RightArrow) || (kick && transform.localScale.x > 0) || (hit && transform.localScale.x > 0)))
        {
            transform.Translate(Vector2.right * speedMove * Time.deltaTime);
            if (isGround)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 0);
            }
        }
        else if (((Input.GetKey(KeyCode.LeftArrow)) || (kick && transform.localScale.x < 0) || (hit && transform.localScale.x < 0)))
        {
            transform.Translate(-Vector2.right * speedMove * Time.deltaTime);
            if (isGround)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 0);
            }
        }
        if (vertical1 > 0.1f && isGround)
        {
            jump = true;
        }
        if (jump)
        {
            Jump(speedJump);
            jump = false;
        }
    }
    public void Jump(float speedJump)
    {
        rib.velocity = new Vector2(rib.velocity.x, speedJump);
        //isGround = false;
        ani.SetBool("jump", jump);
    }


    private void ChangeAnimation()
    {
        ani.SetBool("isGround", isGround);
        ani.SetFloat("speed", Mathf.Abs(horizontal1));
        ani.SetBool("isDamaged", isDamaged);
        ani.SetInteger("power", curPower);
    }

    private int countHit;
    public GameObject gameOverObj;
    public void GameOver()
    {
        BasePopupManager.Instance.ShowPopupLose();
        GameManager.GetIntance().state = GameState.Pause;
        //Time.timeScale = 0;
        //resetGame();
    }

    private void AttackEnemy()
    {
        //hit
        if (Input.GetKeyDown(KeyCode.Alpha1) && isGround)
        {
            ani.SetTrigger("hit");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isGround)
        {
            ani.SetTrigger("kick");
        }
        //super hit
        if (Input.GetKeyDown(KeyCode.Alpha5) && isGround)
        {
            if (curPower == 100)
            {
                curPower = curPower - 100;
                ani.SetTrigger("superHit");
                gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            }
        }
        //shoot
        if (Input.GetKeyDown(KeyCode.Alpha4) && isGround)
        {
            if (curPower >= 60)
            {
                curPower = curPower - 1;
                ani.SetTrigger("shoot");
                gameManager.UpdateHealthyPlayer1(ref curHealthy, ref healthy, ref curPower, ref power, 1);
            }
        }
        if (curHealthy <= 0)
        {

            GameOver();
        }
    }


    private float timerDamaged, damagedRate;

    private void Blocking()
    {
        if (isDamaged)
        {
            statePlayer = StatePlayer.Damaged;
            timerDamaged += Time.deltaTime;
            if (rib.gravityScale < gravity)
            {
                rib.gravityScale += Time.deltaTime;
            }
            if (timerDamaged > damagedRate)
            {
                isDamaged = false;
                timerDamaged = 0;
                statePlayer = StatePlayer.Normal;
                rib.gravityScale = gravity;
            }
        }
    }

    private void CreateEffectDamaged(float velocityX, float velocityY)
    {
        GameObject prefab = Instantiate(PrefabGO.GetInstance().particalDamaged, transform.position + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        prefab.transform.parent = this.transform;
        Destroy(prefab, 0.3f);
        //quay chieu

        if (transform.position.x > player1script.gameObject.transform.position.x)   
        {
            transform.localScale = new Vector3(-scaleX, scaleY, 0);
            rib.velocity = new Vector2(velocityX, velocityY);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, scaleY, 0);
            rib.velocity = new Vector2(-velocityX, velocityY);
        }
    }
    private void Delay(ref bool attack, ref float time)
    {
        if (attack)
        {
            time += Time.deltaTime;
            if (time >= 0.3f)
            {
                attack = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGround = true;
        }
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

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player1" && other.gameObject.tag != "healing" && other.gameObject.tag != "power")
        {
            CreateEffectDamaged(0.5f, 1);
            isDamaged = true;
            damagedRate = 0.5f;
        }

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
        gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
    }
}

