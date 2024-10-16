using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : EnemyDamagers
{
    [HideInInspector]public float damageAmount;

    [HideInInspector] public float lifeTime,growSpeed = 5f;//��������ʱ��
    private Vector3 targetSize;

    public bool shouldKnockBack;
    public float sloweffect;

    public bool destroyParent;//�Ƿ�Ҫ�ݻٸ��࣬������������

    public bool damageOverTime;//�Ƿ�ӵ���˺����
    [HideInInspector]public float attackTime;
    
    private float damageCounter;//�˺����ʱ��

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//һ����������͸����
    public float normalAttribute = 1.0f; // ��ҵ���������ֵ
    public float buffedAttribute = 3.0f; // ����������ֵ
    private float buffDuration = 10.0f; // �������ʱ��

    public float externalDamage = 0f;
    public float externalScale = 0f;
    public float externalLifeTime = 0f;
    public bool poison = false;
    private bool lowbloodKill;

    public bool nowstone;
    public bool makeEnemyStone = false;
    void Start()
    {//������С�����Ч
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//�����ȸ�һ��Ĭ�ϵ��˺����
        if (externalDamage != 0f)
        {
            damageAmount = externalDamage;
        }
        if (externalScale != 0f)
        {
            transform.localScale = Vector3.one * externalScale;
        }
        if (externalLifeTime != 0f)
        {
            lifeTime = externalLifeTime;
        }
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize,growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//��������ʱ��

        if (lifeTime <= 0) {
            targetSize = Vector3.zero;//�о������Ƿϻ�
            Destroy(gameObject);
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if(destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if(damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if(damageCounter <= 0)
            {
                damageCounter = attackTime;

                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    if(enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(FoodController.instance.powerbuff*damageAmount, shouldKnockBack);
                        
                        
                    }else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
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
                //EnemyController ene = collision.gameObject.GetComponent<EnemyController>();
                //if (poison && ene != null)
                //{
                //    ene.poison = true;
                //    ene.isSlow = true;
                //    ene.slowNumber = sloweffect;
                //}
                //ShootEnemyController ene2 = collision.gameObject.GetComponent<ShootEnemyController>();
                //if (poison && ene2 != null)
                //{
                //    ene2.poison = true;
                //    ene2.isSlow = true;
                //    ene2.slowNumber = sloweffect;
                //}
                //BossController ene3 = collision.gameObject.GetComponent<BossController>();
                //if (poison&&ene3 != null)
                //{
                //    ene3.poison = true;
                //}
                if (collision.GetComponent<Enemy>() != null)
                {
                    Enemy em = collision.GetComponent<Enemy>();
                    em.TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    
                }
                    
                destroyOnImpactTimes--;
                if (destroyOnImpactTimes==0)
                {
                    Destroy(gameObject);
                }
            }
        }else
        {
            if(collision.tag == "Enemy")//layerName == "Enemy"
            {
                //EnemyController ene = collision.gameObject.GetComponent<EnemyController>();
                //if (ene != null)
                //{
                //    ene.isSlow = true;
                //    ene.slowNumber = sloweffect;
                //}
                //ShootEnemyController ene2 = collision.gameObject.GetComponent<ShootEnemyController>();
                //if (ene2 != null)
                //{
                //    ene2.isSlow = true;
                //    ene2.slowNumber = sloweffect;
                //}
                enemiesInRange.Add(collision.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(damageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                //EnemyController ene = collision.gameObject.GetComponent<EnemyController>();
                //if (ene != null)
                //{
                //    ene.isSlow = false;
                //}
                //ShootEnemyController ene2 = collision.gameObject.GetComponent<ShootEnemyController>();
                //if (ene2 != null)
                //{
                //    ene2.isSlow = false;
                //}
                enemiesInRange.Remove(collision.GetComponent<Enemy>());
            }
        }
    }
    //public void addPoison()
    //{
    //    poison = true;
    //}
    //public void KillLowBloodEnemy()
    //{
    //    lowbloodKill = true;
    //}
    //public void stoneskill()
    //{

    //}
    //public void makeStone()
    //{
    //    makeEnemyStone = true;
    //}
}
