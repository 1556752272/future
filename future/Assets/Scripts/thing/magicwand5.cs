using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System;
public class magicwand5 : wand
{
    [SerializeField] private float attackSpeed;//����Խ����Խ��
    [SerializeField] private float searchRadius = 50f; // ������Χ
    [SerializeField] private int attack;
    [SerializeField] private MagicBullet5 bullet;//���ﲻ��Ҳû��
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rgbullet;
    float num;
    public LayerMask targetLayerMask;// ����ָ��Ҫ������Layer  
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
            // �ȴ��������  

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
            //����ѡ���ӵ�����ĺ���
            bullet.Init(attack);
            rgbullet = bullet.GetComponent<Rigidbody2D>();
            animator.SetBool("fire", true);
        }
        else
        {//û���ҵ�Ŀ��ɶ�����ɻ��߸ɱ��

        }
    }
    public void animationEnd()
    {
        animator.SetBool("fire", false);
    }
}
