using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
//using DragonBones;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    public float moveSpeed;

    //public Animator anim;

    public float pickupRange = 1;

    public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignWeapons;
    public int maxWeapons;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();
    public float normalAttribute=1.0f; // ��ҵ���������ֵ
    public float buffedAttribute=3.0f; // ����������ֵ
    public float buffDuration = 10.0f; // �������ʱ��
    public float powerattributeTimer; // �������������ʱ��
    public float speedattributeTimer; // �ٶ����������ʱ��
    public float magnetattributeTimer; // ��ʰ���������ʱ��
    public LayerMask coinLayer; // ������ڵĲ�
    public LayerMask ExpLayer; // ���ڵĲ�
    public float detectionRadius = 20.0f; // �������뾶
    private List<CoinPickup> coinAll = new List<CoinPickup>(); // �洢��Χ�ڵĽ��
    private List<ExpPickup> exps = new List<ExpPickup>();


    private float searchRadius = 24.0f; // �����뾶
                                       
    private float findenemytime;
    [HideInInspector]public List<Vector3> enemiesInRange; // �洢�ڷ�Χ�ڵĵ���


   // public UnityDragonBones dragonBonesComponent; // ������ʹ�õ���DragonBones��Unity���  

    // ��������  
    private string idleAnimationName = "idle";
    private string walkRightAnimationName = "walk_right";
    private string walkLeftAnimationName = "walk_left"; // ���ʹ�÷�ת������ܲ���Ҫ���  
    private string deathAnimationName = "death";

    private bool isDead = false;
    void Start()
    {
        


        enemiesInRange = new List<Vector3>();
        if (assignWeapons.Count == 0)
        {
            //AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
        //״̬���ִ�״̬��PlayerStatController�л�ȡ
        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);
    }

    // Update is called once per frame
    void Update()
    {
        
        pickcoinAndExp();
        //if (moveInput != Vector3.zero)
        //{
        //    anim.SetBool("isMoving", true);
        //}
        //else
        //{
        //    anim.SetBool("isMoving", false);
        //}
        findenemytime -= Time.deltaTime;
        if (findenemytime < 0)
        {
            FindEnemiesInRange();
            findenemytime = 0.5f;
        }
        
    }
    private void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if(PlayerHealthController.instance.currentHealth > 0) { 
            if (speedattributeTimer > 0)
            {//�ƶ�����
                speedattributeTimer -= Time.fixedDeltaTime;
                transform.position += moveInput * moveSpeed * Time.fixedDeltaTime * buffedAttribute;
            }
            else
            {
                transform.position += moveInput * moveSpeed * Time.fixedDeltaTime;
            }
        }
    }
    public void magnetfunc()
    {
        magnetattributeTimer = buffDuration;
    }
    public void pickcoinAndExp()
    {
        if (magnetattributeTimer > 0)
        {
            magnetattributeTimer -= Time.deltaTime;
            Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, detectionRadius, coinLayer);
            Collider2D[] Exps = Physics2D.OverlapCircleAll(transform.position, detectionRadius, ExpLayer);
            // ����⵽�Ľ����ӵ��б���
            foreach (Collider2D coin in coins)
            {
                CoinPickup c = coin.GetComponent<CoinPickup>();//�������������ʲô
                coinAll.Add(c);
                c.movingToPlayer = true;
                c.moveSpeed *= 3;
            }
            foreach (Collider2D e in Exps)
            {
                ExpPickup ex= e.GetComponent<ExpPickup>();//�������������ʲô
                exps.Add(ex);
                ex.movingToPlayer = true;
                ex.moveSpeed *= 3;
            }
        }
    }
    void FindEnemiesInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, searchRadius);

        enemiesInRange.Clear();

        foreach (Collider2D hit in hits)
        {
            // ȷ���ǵ��˲��Ҳ��������Լ�
            if (hit.CompareTag("Enemy") && hit.transform != this.transform)
            {
                enemiesInRange.Add(new Vector3(hit.transform.position.x, hit.transform.position.y,0));//enemiesInRange.Add(hit.transform);
            }
        }
    }

    // �ҵ������ؾ�����̵ĵ���
    public Vector3 GetClosestEnemy()
    {
        Vector3 closestEnemy = Vector3.zero;
        float shortestDistance = float.MaxValue;

        foreach (Vector3 enemy in enemiesInRange)
        {
           // if (enemy == null) continue;//������˾����������Ҿ���ÿ��ʱ�̶��ڸ����������λ�ã��Ҿ����е�����ν
            float distance = Vector2.Distance(this.transform.position, enemy);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        
        return closestEnemy;//���������������
    }
    public GameObject ch2skill;
    public Weapon ch2weapon1;
    public Weapon ch2weapon2;
    public GameObject ch2sprite;
    public GameObject ch2button;
    public void ch2Join()
    {
        ch2skill.SetActive(true);
        ch2weapon1.gameObject.SetActive(true);
        ch2weapon2.gameObject.SetActive(true);
        ch2sprite.SetActive(true);
        assignWeapons.Add(ch2weapon1);
        assignWeapons.Add(ch2weapon2);
        ch2button.SetActive(true);
    }
    public GameObject ch3skill;
    public Weapon ch3weapon1;
    public Weapon ch3weapon2;
    public GameObject ch3sprite;
    public GameObject ch3button;
    public void ch3Join()
    {
        ch3skill.SetActive(true);
        ch3weapon1.gameObject.SetActive(true);
        ch3weapon2.gameObject.SetActive(true);
        ch3sprite.SetActive(true);
        assignWeapons.Add(ch3weapon1);
        assignWeapons.Add(ch3weapon2);
        ch3button.SetActive(true);
    }
    public GameObject ch4skill;
    public Weapon ch4weapon1;
    public Weapon ch4weapon2;
    public GameObject ch4sprite;
    public GameObject ch4button;
    public void ch4Join()
    {
        ch4skill.SetActive(true);
        ch4weapon1.gameObject.SetActive(true);
        ch4weapon2.gameObject.SetActive(true);
        ch4sprite.SetActive(true);
        assignWeapons.Add(ch4weapon1);
        assignWeapons.Add(ch4weapon2);
        ch4button.SetActive(true);
    }
}
