using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XXX.UI.Popup;

public class playerControl2 : MonoBehaviour
{
    public enum StatePlayer
    {
        Normal,
        Damaged,
        Die
    }

    private StatePlayer statePlayer;
    private static playerControl2 instance;

    public static playerControl2 GetInstance()
    {
        return instance;
    }

    private playerControl2()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private PlayerControl player1Script;

    [HideInInspector] public Rigidbody2D rib;
    [HideInInspector] public Animator ani;
    [Header("healthy")]
    public int healthy = 100;
    [Header("speed")]
    public float speedMove = 5, speedJump = 26;
    [Header("damagedHealthy")]
    public int damagedHit = 10;
    public int damagedKick = 10;
    public int damagedKick2 = 15;
    public int damagedSuperHit = 40;
    public int damagedShoot = 5;
    [Header("power")]
    public int power = 100;
    [Header("damagedPower")]
    public int powerHit = 60;
    public int powerShoot = 40;

    private GameManager gameManager;

    private float horizontal2, vertical2;
    private float scaleX, scaleY, gravity;
    private bool isGround, hit, kick, superHit, superKick, jump;
    public int curHealthy, curPower;
    [HideInInspector] public bool isDamaged;
    //temp
    private float timeHit, timeKick, timeJump;
    public bool isUsingSkillSpecial;
    // Use this for initialization
    void Start()
    {
        player1Script = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerControl>();
        gameManager = GameManager.GetIntance();

        statePlayer = StatePlayer.Normal; // khởi tạo trạng thái ban đầu cho nhân vật 
        rib = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        gravity = rib.gravityScale;

        curHealthy = healthy;
        curPower = 0;
        gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);

    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.GetIntance().state != GameState.Playing)
        //      {
        //	return;
        //      }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0); 
// 
//if (AIScript == null) {
//	AIScript = GameObject.FindGameObjectWithTag("AI").GetComponent<AIControl> ();
//}
//if (statePlayer != StatePlayer.Damaged) {
        MovePlayer();
        Attack();
        //}
        Blocking();
        ChangeAnimation();

    }


    private void MovePlayer()
    {
        horizontal2 = Input.GetAxis("Horizontal2");
        vertical2 = Input.GetAxis("Vertical2");
        //move
        if ((Input.GetKey(KeyCode.RightArrow) || (kick && transform.localScale.x > 0) || (hit && transform.localScale.x > 0)) )
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
        if (vertical2 > 0.1f && isGround)
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
        ani.SetFloat("speed", Mathf.Abs(horizontal2));
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
    //public void resetGame()
    //{
    //	SceneManager.LoadScene(2);
    //}
    private void Attack()
    {
        //hit
        if (Input.GetKeyDown(KeyCode.Alpha1) && isGround)
        {
            if (timeHit > 0 && countHit == 1)
            {
                ani.SetFloat("timeHit", 0.6f);
            }
            else
            {
                ani.SetTrigger("hit");
                ani.SetFloat("timeHit", 0f);
                timeHit = 0.5f;
                countHit += 1;
            }
        }
        if (timeHit > 0)
        {
            timeHit -= Time.deltaTime;
        }
        else
        {
            countHit = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isGround)
        {
            if (timeHit > 0 && countHit == 1)
            {
                ani.SetFloat("timeHit", 0.6f);
            }
            else
            {
                ani.SetTrigger("kick");
                ani.SetFloat("timeHit", 0f);
                timeHit = 0.5f;
                countHit += 1;
            }
        }
        if (timeHit > 0)
        {
            timeHit -= Time.deltaTime;
        }
        else
        {
            countHit = 0;
        }
        //super hit
        if (Input.GetKeyDown(KeyCode.Alpha5) && isGround)
        {
            //if (curPower < powerHit) {
            //	curPower = curPower - powerHit;
            ani.SetTrigger("superHit");
            //update slider
            //gameManager.UpdatePower(curPower);
            //}
        }
        //shoot
        if (Input.GetKeyDown(KeyCode.Alpha4) && isGround)
        {
            //	if (curPower >= powerShoot) {
            curPower = curPower - powerShoot;
            ani.SetTrigger("shoot");
            //update slider
            //gameManager.UpdatePower(curPower);

            //}
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
        //rib.gravityScale = 0;
        //quay chieu

        if (transform.position.x > player1Script.gameObject.transform.position.x)
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

    //delay press
    //private void Delay(ref bool attack, ref float time)
    //{
    //    if (attack)
    //    {
    //        time += Time.deltaTime;
    //        if (time >= 0.3f)
    //        {
    //            attack = false;
    //        }
    //    }
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGround = true;
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
        //if (!AIScript.isDamaged)
        //{
            if (other.gameObject.tag == "hitP")
            {
            CreateEffectDamaged(0.5f, 1);
			isDamaged = true;
			damagedRate = 0.5f;
				curHealthy -= 10;
			    curPower += 5;
            if (curPower > 100)
            {
                curPower = 100;
            }
            gameManager.UpdateHealthyPlayer2 (ref curHealthy,ref healthy,ref curPower,ref power, 1);

            }
        //    if (other.gameObject.tag == "kick")
        //    {
        //        isDamaged = true;
        //        damagedRate = 1f;
        //        CreateEffectDamaged(0.5f, 1);
        //        //update healthy
        //        gameManager.UpdateHealthy(ref curHealthy, ref damagedKick, ref AIScript.curPowerEnemy, AIScript.power, 1);
        //    }

        if (other.gameObject.tag == "scale")
        {
            CreateEffectDamaged(0.5f, 1);
            isDamaged = true;
            damagedRate = 0.5f;
            curHealthy -= 30;
            curPower += 5;
            if (curPower > 100)
            {
                curPower = 100;
            }
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
        }

        //    if (other.gameObject.name == "kick2P")
        //    {
        //        isDamaged = true;
        //        damagedRate = 1;
        //        CreateEffectDamaged(1f, 1f);
        //        //update healthy
        //        gameManager.UpdateHealthy(ref curHealthy, ref damagedKick2, ref AIScript.curPowerEnemy, AIScript.power, 1);
        //    }

        if (other.gameObject.tag == "bulletPlayer")
        {
            Destroy(other.gameObject);
            CreateEffectDamaged(0.5f, 1);
            isDamaged = true;
            damagedRate = 0.5f;
            curHealthy -= 10;
            curPower += 10;
            if (curPower > 100)
            {
                curPower = 100;
            }
            gameManager.UpdateHealthyPlayer2(ref curHealthy, ref healthy, ref curPower, ref power, 1);
        }
    }


}
