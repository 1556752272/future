using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoy : MonoBehaviour
{
    [HideInInspector]public bool faceRight = false;
    private float attackRange = 15f; // ����Ĺ�����Χ  
    public float returnRange = 13f; // ����ص������ߵķ�Χ  
    private float speed =5.0f;
    private float findRange = 10f;
    private Enemy currentTarget; // ��ǰĿ�����  
    private bool isAttacking = false; // �Ƿ����ڹ��� 
    // Start is called before the first frame update
    //public List<Vector3> enemiesInRange; // �洢�ڷ�Χ�ڵĵ���


    public float chargeSpeed = 5f; // ����ٶ�  
    //public float chargeDuration = 0.2f; // ��̳���ʱ��  
    private bool isCharging = false; // ��ǳ����Ƿ����ڳ��  
    private Vector2 chargeDirection; // ��̷���  
    private float chargetime;
    private float chargetimer=0.5f;//0.5���һ��
    void Start()
    {
        speed= speed*Random.Range(0.75f, 1.25f);
        
    }

    void Update()
    {
        if (chargetime >= 0)
        {
            chargetime -= Time.deltaTime;
        }
        if (currentTarget != null)//�������﷽��
        {
            if (this.transform.position.x < currentTarget.transform.position.x)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        }
        
        if (currentTarget == null)
        {
            FindAndAttackNearestEnemy();
        }
        if ((PlayerController.instance.transform.position - transform.position).magnitude > returnRange)
        {

            ReturnToPlayer();
        }
        else if (currentTarget != null) // ������ڵ�ǰĿ��  
        {
            // �����ǰĿ�������򳬳�������Χ��Ѱ���µ�Ŀ��  
            if ((currentTarget.transform.position - transform.position).magnitude > findRange)
            {
                currentTarget = null;
                FindAndAttackNearestEnemy();
            }
            else
            {

                if (chargetime <= 0)
                {
                    chargetime = chargetimer;
                    AttackCurrentTarget();

                }
                // �������������ǰĿ��  
                
            }
        }
    }
    void FindAndAttackNearestEnemy()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(PlayerController.instance.transform.position, attackRange);

        EnemyController nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // �����˱�ǩ  
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();
                
                if (enemy != null) // ȷ�����˴����һ���  
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                       
                    }
                }
            }
        }

        // ��ʼ��������ĵ���  
        if (nearestEnemy != null)
        {
            currentTarget = nearestEnemy;
            isAttacking = true;
            //AttackCurrentTarget();
        }
    }

    void AttackCurrentTarget()
    {
        //if (chargetime <= 0)
        //{
        //    chargetime = chargetimer;


        //}
        // ����ǰĿ���ƶ���ִ�й����߼�  
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        //if (direction.x > 0) 
        //{

        //    faceRight = true;
        //}  
        //else
        //{

        //    faceRight = false;
        //}
        //transform.position += direction * Time.deltaTime * speed*2; // ���������ÿ��5��λ���ٶ��ƶ�  
        Vector3 velocity = direction * chargeSpeed;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void ReturnToPlayer()
    {
        // ������ƶ�  
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        //if (direction.x > 0)
        //{
        //    faceRight = true;
        //}
        //else
        //{
        //    faceRight = false;
        //}
        Vector3 velocity = direction * chargeSpeed*4f;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        isAttacking = false;
        currentTarget = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

        }
    }
}
