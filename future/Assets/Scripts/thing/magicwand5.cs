using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System;
public class magicwand5 : wand
{
    [SerializeField] private float attackSpeed;//数字越大攻速越慢
    [SerializeField] private float searchRadius = 50f; // 攻击范围
    [SerializeField] private int attack;
    [SerializeField] private MagicBullet5 bullet;//这里不放也没事
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
        if (monsters.Length > 0)
        {
            bullet = ResManager.Load<MagicBullet5>("MagicBullet5", LVManager.Instance.TempObjRoot);
            bullet.transform.position = firePoint.position;
            //这里选择子弹自身的函数
            bullet.Init(attack);
            rgbullet = bullet.GetComponent<Rigidbody2D>();
            animator.SetBool("fire", true);
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
