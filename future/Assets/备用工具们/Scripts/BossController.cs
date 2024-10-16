using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Enemy
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;//�����˺�

    public float hitWaitTime = 1f;//��ײ��ɫ�´ε���ȴʱ��
    private float hitCounter;



    public float knockBackTime = 0.5f;//���˹���ʱ��
    private float knockBackCounter;

    public int expToGive = 1;//���侭��

    public int coinValue = 1;//����������
    public float coinDropRate = .5f;//�����Ҹ���
                                    //*********************************************************

    public GameObject bulletPrefab; // �ӵ�Prefab
    public Transform bulletSpawnPoint; // �ӵ����ɵ�

    public float shootCooldown = 2.0f; // �����ȴʱ��
    public float chargeCooldown = 3.0f; // ��ײ��ȴʱ��
    public float chargetime;//��ײ��ʱ�����ж�
    public float chargeCounter = 2.0f;
    public Vector3 chargeDirection;
    public float chargeSpeed = 10.0f; // ��ײ�ٶ�
    private float shootTimer; // �����ʱ��
    private float chargeTimer; // ��ײ��ʱ��
    public float attackRange = 20.0f; // ����Ĺ������
    //public float attackCooldown = 1.0f; // ������ȴʱ��

    public bool faceRight = false;

    public float shootForce = 5.0f; // �����

    private int stoneattacktimer;
    private bool stoned;
    float stonedTimer;

    private bool isIgnited = false; // �Ƿ񱻵�ȼ  
    private Coroutine igniteCoroutine; // ��ȼЭ��  
    void Start()
    {
        maxhealth = health;
        bulletSpawnPoint = this.transform;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
        //attackTimer = attackCooldown; // ��ʼ��������ʱ��
        shootTimer = shootCooldown;
        chargeTimer = chargeCooldown;
    }

    void Update()
    {
        if ((PlayerController.instance.transform.position.x < transform.position.x) && faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = false;
        }
        if ((PlayerController.instance.transform.position.x > transform.position.x) && !faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = true;
        }
        if (stonedTimer > 0)
        {
            stonedTimer -= Time.deltaTime;
            if (stonedTimer <= 0)
            {
                stoned = false;
            }
        }
        if (stonedTimer > 0)
        {
            stonedTimer -= Time.deltaTime;
            this.theRB.velocity = Vector3.zero;
            if (stonedTimer <= 0)
            {
                stoned = false;
            }
        }
        
            if (PlayerController.instance.gameObject.activeSelf == true)//������һ�������ǹ����ƶ���
            {
                //// �������Ƿ��������
                //if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                //{
                //    // ֹͣ�����ƶ�
                //    theRB.velocity = Vector2.zero;

                //    // ÿ����������ȴʱ�䣬����һ�ι���
                //    if (attackTimer <= 0)
                //    {
                //        AttackPlayer();
                //        attackTimer = attackCooldown;
                //    }
                //    else
                //    {
                //        attackTimer -= Time.deltaTime;
                //    }
                //if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                // {
                // ÿ���������ȴʱ�䣬����һ�����
                if (shootTimer <= 0)
                {
                    AttackPlayer();//������ʹ��ĳ����ʽ
                    shootTimer = shootCooldown;
                }
                else
                {
                    shootTimer -= Time.deltaTime;
                }
                //}


                // ÿ������ײ��ȴʱ�䣬����һ�γ�ײ
                if (chargeTimer <= 0)
                {

                    chargeDirection = (target.position - transform.position).normalized;
                    chargetime = chargeCounter;//���ĳ��ʱ��ͷ���
                                               //ChargeAtPlayer();
                    chargeTimer = chargeCooldown;
                }
                else
                {
                    chargeTimer -= Time.deltaTime;
                }
            if (stoned)
            {
                theRB.velocity = Vector3.zero;
            }
            else
            {
                bossmove();
            }

            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter < 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }

    }

    public override void TakeDamage(float damageToTake)
    {
        SFXManager.instance.PlaySFXPitched(2);
        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);

            Destroy(gameObject);

            if (Random.value <= coinDropRate)//һ����ʵ�����
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

            // SFXManager.instance.PlaySFXPitched(0);
        }
        else
        {
            //  SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
        //Debug.Log("�ҵ�Ҫ��������: " + transform.position);

    }

    public override void TakeDamage(float damageToTake, bool shouldKnockback)//��������Ƿ��л�����Ч
    {
        if (poison) { TakeDamage(damageToTake * 1.3f); } //�����ǹ����϶��ؼӱ��˺�
        else { TakeDamage(damageToTake); }
        if (shouldKnockback)//����Ϊboss���ñ�����
        {
            knockBackCounter = knockBackTime;
        }
    }
    public override void TakeDamage(float damageToTake, int fire)
    {
        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            Die();
            Destroy(gameObject);

            if (Random.value <= coinDropRate)//һ����ʵ�����
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

        }
        else
        {
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position, true);

    }
    public void AttackPlayer()
    {
        if (PlayerController.instance.transform.position.x < transform.position.x)
        { //���������
          // ʵ�����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position - new Vector3(0.3f, 0, 0), Quaternion.identity);

            // �����ӵ��ķ���������ָ�����
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // ȷ�����������ĳ���Ϊ1

            // Ϊ�ӵ�����ٶȺͷ���
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * shootForce;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + new Vector3(0.3f, 0, 0), Quaternion.identity);

            // �����ӵ��ķ���������ָ�����
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // ȷ�����������ĳ���Ϊ1

            // Ϊ�ӵ�����ٶȺͷ���
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * shootForce;
        }
    }
    private void bossmove()
    {
        if (chargetime >= 0)
        {
            chargetime -= Time.deltaTime;
            theRB.velocity = chargeDirection * chargeSpeed;
            return;
        }
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
        if (stoned)
        {
            theRB.velocity = Vector3.zero;
        }
        else
        {
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
        }

        if (hitCounter >= 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }
    void ChargeAtPlayer()
    {
        // ����Ӧ�ð���������λ�õ��߼���������ҳ�ײ
        // ��ʾ���У����Ǽ���Boss�����������ײ
        Vector3 direc = (target.position - transform.position).normalized;
        //// ��ӳ�ײ��
        //theRB.AddForce(transform.forward * chargeSpeed, ForceMode2D.Impulse);
        theRB.velocity = direc * chargeSpeed;
    }
    public override void makeStoned()
    {
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 0;
            stoned = true;
            stonedTimer = 2f;
        }
    }
    public override void makemaxStoned()
    {
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 5;
            stoned = true;
            stonedTimer = 2f;
        }
    }

    public override void makeFired(int num)
    {
        igniteDamage = num;
        if (!isIgnited)
        {
            isIgnited = true;
            igniteCoroutine = StartCoroutine(TakeIgniteDamage());
        }
    }

    // ÿ�����Ѫ��Э��  
    private IEnumerator TakeIgniteDamage()
    {
        while (isIgnited && health > 0)
        {
            yield return new WaitForSeconds(igniteInterval);
            TakeDamage(igniteDamage, 2);
        }
    }

    // ���������߼�  
    private void Die()
    {
        if (isIgnited)
        {
            isIgnited = false;
            StopCoroutine(igniteCoroutine);

            // ��Ⲣ�˺���Χ�ĵ���  
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                Enemy enemy = col.GetComponent<Enemy>(); // ��������һ��Enemy�ű��ڵ�������  
                if (enemy != null)
                {
                    enemy.TakeDamage(deathDamage,2);
                }
            }
        }

    }
}
