using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System;
public class magicwand8 : wand
{
    [SerializeField] private float attackSpeed;//数字越大攻速越慢
    [SerializeField] private float movePower;
    [SerializeField] private float searchRadius = 50f; // 攻击范围
    [SerializeField] private int attack;
    [SerializeField] private MagicBullet8 bullet;//这里不放也没事
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rgbullet;
    float num;
    public LayerMask targetLayerMask;// 用于指定要搜索的Layer  
    Animator animator;
    public string name;
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
            bullet = ResManager.Load<MagicBullet8>("MagicBullet8", LVManager.Instance.TempObjRoot);
            bullet.transform.position = firePoint.position;
            //这里选择子弹自身的函数
            bullet.Init(attack);
            rgbullet = bullet.GetComponent<Rigidbody2D>();
            // 朝最近的怪物发射子弹  
            rgbullet.AddForce(dir.normalized * movePower);
            //trailRenderer.emitting = true;
            //collider.enabled = true;//很奇怪
            bullet.moveDir = dir;
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
}
