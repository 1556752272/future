using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System;
using Cinemachine.Utility;
using UnityEngine.UIElements;
public class magicwand7 : wand
{
    [SerializeField] private float attackSpeed;//数字越大攻速越慢
    [SerializeField] private float movePower;
    [SerializeField] private float searchRadius = 50f; // 攻击范围
    [SerializeField] private int attack;
    [SerializeField] private MagicBullet7 bullet;//这里不放也没事
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rgbullet;
    float num;
    public LayerMask targetLayerMask;// 用于指定要搜索的Layer  
    Animator animator;
    public string name;
    Vector3 direction;
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            // 等待攻击间隔  

            yield return new WaitForSeconds(attackSpeed);

            findenemy();
        }
    }
    private void Update()
    {
        
    }
    public void findenemy()
    {
         
    Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayerMask);
        //Debug.Log(monsters.Length);
        for (int i=0;i>monsters.Length;i++)
        {
            Debug.Log(monsters[i].gameObject.layer);
        }
        if (monsters.Length > 0)
        {
            // 找到最近的怪物  
            Collider2D nearestMonster = monsters[0];
            float nearestDistance = Vector3.Distance(transform.position, nearestMonster.transform.position);

            for (int i = 1; i < monsters.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, monsters[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestMonster = monsters[i];
                    nearestDistance = distance;
                }
            }
            Vector2 dir = nearestMonster.transform.position - Player_Controller.Instance.transform.position;
            
            direction = nearestMonster.transform.position - Player_Controller.Instance.transform.position;
            direction.Normalize(); // 确保方向向量的长度为1
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
            InstantiateBullet(firePoint.position, angle);
            // 朝最近的怪物发射子弹  
            
            animator.SetBool("fire",true);
        }
        else
        {//没有找到目标啥都不干或者干别的
            
        }
    }
    public void animationEnd()
    {
        animator.SetBool("fire", false);
    }
    private void InstantiateBullet(Vector3 position, float angle)
    {
        bullet = ResManager.Load<MagicBullet7>("MagicBullet7", LVManager.Instance.TempObjRoot);
        bullet.transform.position = firePoint.position;
        //这里选择子弹自身的函数
        bullet.Init(attack);
        rgbullet = bullet.GetComponent<Rigidbody2D>();
        rgbullet.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * movePower*0.1f, Mathf.Sin(angle * Mathf.Deg2Rad) * movePower * 0.1f);
        //rgbullet.AddForce(direction.normalized * movePower);
        bullet.moveDir = direction;
        //FireDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        //bullet.gameObject.SetActive(true);
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
    }
}
