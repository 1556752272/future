using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : EnemyDamagers
{
    public PlayerBubble dad;
    [HideInInspector] public float damageAmount;

    [HideInInspector] public float lifeTime, growSpeed = 5f;//��������ʱ��
    private Vector3 targetSize;

    public bool shouldKnockBack;


    public bool destroyParent;//�Ƿ�Ҫ�ݻٸ��࣬������������

    public bool damageOverTime;//�Ƿ�ӵ���˺����
    [HideInInspector] public float attackTime;

    private float damageCounter;//�˺����ʱ�䣬��Ӧ�������

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//һ����������͸����
    public float normalAttribute = 1.0f; // ��ҵ���������ֵ
    public float buffedAttribute = 3.0f; // ����������ֵ
    private float buffDuration = 10.0f; // �������ʱ��
    [HideInInspector] public float powerattributeTimer; // �������������ʱ��
    private float searchRadius = 24.0f; // �����뾶
    [HideInInspector] public List<Vector3> enemiesInLittleRange;
    public EnemyDamager littleBubble;
    public float shootForce = 3.0f; // �����
    void Start()
    {//������С�����Ч
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//�����ȸ�һ��Ĭ�ϵ��˺����
        dad = PlayerBubble.instance;

    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//��������ʱ��

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0)
            {
                damageCounter = attackTime;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                            enemiesInRange[i].TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                        

                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
    void FindEnemiesInRange()//���¸�����������
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, searchRadius);

        enemiesInLittleRange.Clear();

        foreach (Collider2D hit in hits)
        {
            // ȷ���ǵ��˲��Ҳ��������Լ�
            if (hit.CompareTag("Enemy") && hit.transform != this.transform)
            {
                enemiesInLittleRange.Add(new Vector3(hit.transform.position.x, hit.transform.position.y, 0));//enemiesInRange.Add(hit.transform);
            }
        }
    }
    public Vector3 GetClosestEnemy()
    {
        Vector3 closestEnemy = Vector3.zero;
        float shortestDistance = float.MaxValue;

        foreach (Vector3 enemy in enemiesInLittleRange)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;

        // ��ȡ��ײ�����ڵĲ�
        int layerIndex = collidingObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);



        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<Enemy>() != null)
                {
                    collision.GetComponent<Enemy>().TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    if (collision.GetComponent<Enemy>().health - damageAmount <= 0)
                    {
                        PlayerBubble.instance.addKillnum();
                    }
                }
                

                
                destroyOnImpactTimes--;//���伸��
                if (PlayerBubble.instance.weaponLevel >= 6)
               {
                    FindEnemiesInRange();
                    Vector3 v = GetClosestEnemy();
                    AttackEnemy();

                }
                
                if (destroyOnImpactTimes == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    //Transform enemyTran;
                    Vector3 enemyTran=Vector3.zero; //Transform enemyTran2 ; ;enemyTran2.position = new position;
                    List<Vector3> enemies = PlayerController.instance.enemiesInRange;
                    if (enemies.Count > 0)
                    {
                        int nextIndex = Random.Range(0, enemies.Count);
                        enemyTran = enemies[nextIndex];
                    }
                    //else
                    //{
                    //    // ���û�е��ˣ�����ֹͣ���ݻ�����������  
                    //    enemyTran =Vector3.zero;
                    //}
                    //Transform enemyTran = PlayerController.instance.GetClosestEnemy();
                    if (enemyTran== Vector3.zero) Destroy(gameObject);//û�е����������Լ�����
                    // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);


                    Vector3 direction = enemyTran - this.transform.position; //enemyTran.position - this.transform.position;
                    direction.Normalize(); // ȷ�����������ĳ���Ϊ1

                    // Ϊ�ӵ�����ٶȺͷ���
                    Rigidbody2D bulletRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
                    
                    bulletRigidbody.velocity = direction * dad.shootForce;
                }
            }
        }
        else
        {
            if (layerName == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<Enemy>());
            }
        }
    }
    public void powertimeup()//ʳ��ǿ��
    {
        powerattributeTimer = buffDuration;
    }
    public void AttackEnemy()
    {
        Vector3 enemyTran = PlayerController.instance.GetClosestEnemy();
        if (enemyTran == null) return;
        // GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position , Quaternion.identity);

        Vector3 direction = enemyTran - PlayerController.instance.transform.position;
        direction.Normalize(); // ȷ�����������ĳ���Ϊ1
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        EnemyDamager bullet = Instantiate(littleBubble, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
        bullet.gameObject.SetActive(true);
        // Ϊ�ӵ�����ٶȺͷ���
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * shootForce;

    }
}
