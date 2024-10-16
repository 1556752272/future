using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime=3f, growSpeed = 5f;//��������ʱ��
    private Vector3 targetSize;

    public bool shouldKnockBack;


    public bool destroyParent;//�Ƿ�Ҫ�ݻٸ��࣬������������

    public bool damageOverTime;//�Ƿ�ӵ���˺����
    public float attackTime;

    private float damageCounter;//�˺����ʱ��

    public List<Enemy> enemiesInRange = new List<Enemy>();

    public int destroyOnImpactTimes;//һ����������͸����
    public float normalAttribute = 1.0f; // ��ҵ���������ֵ
    public float buffedAttribute = 3.0f; // ����������ֵ
    private float buffDuration = 10.0f; // �������ʱ��
    [HideInInspector] public float powerattributeTimer; // �������������ʱ��
    public int sign = 0;
    void Start()
    {//������С�����Ч
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        attackTime = 0.5f;//�����ȸ�һ��Ĭ�ϵ��˺����

    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;//��������ʱ��

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;
            Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;

        // ��ȡ��ײ�����ڵĲ�
        int layerIndex = collidingObject.layer;
        string layerName = LayerMask.LayerToName(layerIndex);

        //Debug.Log(damageOverTime);
        //Debug.Log(collision.tag);
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy")
            {

                if (collision.GetComponent<Enemy>() != null)
                {   
                    collision.GetComponent<Enemy>().TakeDamage(FoodController.instance.powerbuff * damageAmount, shouldKnockBack);
                    if(sign==1)collision.GetComponent<Enemy>().makeFired(10);

                }
               

                destroyOnImpactTimes--;
                if (destroyOnImpactTimes == 0)
                {
                    Destroy(gameObject);
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
}
