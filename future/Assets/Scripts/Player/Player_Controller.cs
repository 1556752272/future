using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System.Security.Cryptography;

public enum PlayerState
{ 
    Normal,
    ReLoad,
    GetHit,
    Die
}
public class Player_Controller : SingletonMono<Player_Controller>
{
    //[SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator; 

    private PlayerState playerState;
    private Rigidbody2D rigidbody;
    private float inputX, inputY;
    private float stopX, stopY;
    #region 参数
    private float moveSpeed;
    private int currBulletNum;
    private int maxBulletNum;
    private float shootInterval;
    private float bulletMovePower;
    private int attack;
    private bool canShoot = true;
    private int hp;
    private int HPmax;
    public int HPmaxx()
    {
        return HPmax;
    }
    private int shieldmax;
    public int shieldmaxx()
    {
        return shieldmax;
    }
    public int HP { get => hp;
        set {
            hp = value;
            // 更新血条
            EventManager.EventTrigger<int>("UpdateHP", hp);
        }
    }
    private int shield;
    public int Shield
    {
        get => shield;
        set
        {
            shield = value;
            // 更新血条
            EventManager.EventTrigger<int>("UpdateShield", shield);
        }
    }
    #endregion
    [SerializeField] private Animator shadow;
    public PlayerState PlayerState
    {
        get => playerState;
        set
        {
            playerState = value;
            switch (playerState)
            { 
                case PlayerState.ReLoad:
                    StartCoroutine(ReLoad());
                    break;
                case PlayerState.GetHit:
                    // 重置上一次受伤带来的效果
                    StopCoroutine(DoGetHit());
                    animator.SetBool("GetHit", false);

                    // 开始这一次受伤带来的效果
                    animator.SetBool("GetHit", true);
                    StartCoroutine(DoGetHit());
                    break;
                case PlayerState.Die:
                    EventManager.EventTrigger("GameOver");
                    animator.SetTrigger("Die");
                    break;
            }
        }
    }



    public void Init(Player_Config config)
    {
        HPmax = config.HP;
        shieldmax= config.Shield;
        Debug.Log("sheid" + shield);
        HP = config.HP;
        Shield= config.Shield;
        moveSpeed = config.MoveSpeed;
        maxBulletNum = config.MaxBulletNum;
        currBulletNum = maxBulletNum;
        bulletMovePower = config.BulletMovePower;
        attack = config.Attack;
        rigidbody=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            //float bulletSpeed = 20;
            
            //MagicBullet bullet = ResManager.Load<MagicBullet>("MagicBullet", LVManager.Instance.TempObjRoot);
            //bullet.transform.position = firePoint.position;
            ////这里选择子弹自身的函数
            //bullet.Init(firePoint.forward, bulletMovePower, attack);
       // }
        if (Time.timeScale != 0)
        {
            StateOnUpdate();
            Vector2 input = InputControler.Instance.getDirection();
            //Debug.Log(input);
            //Debug.Log(rigidbody.velocity);
            if (input != Vector2.zero)
            {
                // animator.SetBool("isMoving", true);
                // stopX = inputX;
                //stopY = inputY;
                if (input.x > 0)
                {
                    animator.SetFloat("MoveX", input.x);
                    animator.SetInteger("Stay", 1);
                    shadow.SetInteger("moving", 1);
                    //Debug.Log(input.x);
                }
                else
                {
                    animator.SetFloat("MoveX", input.x);
                    animator.SetInteger("Stay", 1);
                    shadow.SetInteger("moving", 1);
                }
            }
            else
            {
                //animator.SetTrigger("stay");
                animator.SetFloat("MoveX", input.x);
                animator.SetInteger("Stay", 0);
                shadow.SetInteger("moving", 0);
            }
        }
        //Debug.Log(playerState);
    }
    private void FixedUpdate()
    {
        
        switch (PlayerState)
        {
            case PlayerState.Normal:
                //Move();
                //Shoot();
                //if (currBulletNum < maxBulletNum && Input.GetKeyDown(KeyCode.R))
                //{
                //    PlayerState = PlayerState.ReLoad;
                //}
                if (Time.timeScale != 0)
                {
                    //inputX = Input.GetAxisRaw("Horizontal");
                    //inputY = Input.GetAxisRaw("Vertical");
                    Vector2 input = InputControler.Instance.getDirection();
                    //Debug.Log(input);
                    //rigidbody.velocity = input * moveSpeed ;
                    moveSpeed = 10;
                    Vector3 move = input * moveSpeed * Time.fixedDeltaTime;
                    // 移动角色  
                    //transform.Translate(new Vector2(1,1), Space.World);
                    transform.position += move;

                }
                break;
            case PlayerState.ReLoad:
                
                break;
        }
    }
    private void StateOnUpdate()
    {
        switch (PlayerState)
        {
            case PlayerState.Normal:
                //Move();
                Shoot();
                if (currBulletNum<maxBulletNum&&Input.GetKeyDown(KeyCode.R))
                {
                    PlayerState = PlayerState.ReLoad;
                }
                break;
            case PlayerState.ReLoad:
                //Move();
                break;
        }
    }


    private void Shoot()
    {
        if (canShoot&&currBulletNum>0&&Input.GetMouseButton(0))
        {
            StartCoroutine(DoShoot());
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private IEnumerator DoShoot()
    {
        currBulletNum -= 1;
        // 修改UI
        EventManager.EventTrigger<int, int>("UpdateBullet", currBulletNum, maxBulletNum);
        animator.SetBool("Shoot", true);
        canShoot = false;
        AudioManager.Instance.PlayOnShot("Audio/Shoot/laser_01", transform.position);
        // 生成子弹
        Bullet bullet = ResManager.Load<Bullet>("Bullet", LVManager.Instance.TempObjRoot);
        //bullet.transform.position = firePoint.position;
       // bullet.Init(firePoint.forward, bulletMovePower, attack);
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
        // 子弹打完，需要换弹
        if (currBulletNum == 0)
        {
            PlayerState = PlayerState.ReLoad;
        }
        yield return new WaitForSeconds(1);
    }

    private IEnumerator ReLoad()
    {
        animator.SetBool("ReLoad", true);
        AudioManager.Instance.PlayOnShot("Audio/Shoot/ReLoad", this);
        yield return new WaitForSeconds(1.9f);
        animator.SetBool("ReLoad", false);
        PlayerState = PlayerState.Normal;
        currBulletNum = maxBulletNum;
        EventManager.EventTrigger<int, int>("UpdateBullet", currBulletNum, maxBulletNum);
    }

    public void GetHit(int damage)
    {

        if (hp == 0) return;
        if (shield>0)
        {
            shield -= damage;
            Shield = shield;
            if (shield < 0) Shield = 0;
            PlayerState = PlayerState.GetHit;
        }else
        {
            hp -= damage;
            if (hp <= 0)
            {
                HP = 0;
                PlayerState = PlayerState.Die;
            }
            else
            {
                HP = hp;
                PlayerState = PlayerState.GetHit;
            }
        }
    }

    private IEnumerator DoGetHit()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("GetHit", false);
        if (PlayerState==PlayerState.GetHit)
        {
            PlayerState = PlayerState.Normal;
        }
    }
}
