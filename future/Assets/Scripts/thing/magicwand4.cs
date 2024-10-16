using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
using System;
public class magicwand4 : wand
{
    [SerializeField] private float attackSpeed;//����Խ����Խ��
    [SerializeField] private float movePower;
    [SerializeField] private float searchRadius = 50f;
    [SerializeField] private int attack;
    [SerializeField] private MagicBullet4 bullet;//���ﲻ��Ҳû��
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rgbullet;
    float num;
    public LayerMask targetLayerMask;// ����ָ��Ҫ������Layer  

    public string name;
    private void Start()
    {
        
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
        //Debug.Log(monsters.Length);
        for (int i=0;i>monsters.Length;i++)
        {
            Debug.Log(monsters[i].gameObject.layer);
        }
        if (monsters.Length > 0)
        {
            // �ҵ�����Ĺ���  
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
            bullet = ResManager.Load<MagicBullet4>("MagicBullet4", LVManager.Instance.TempObjRoot);
            bullet.transform.position = firePoint.position;
            //����ѡ���ӵ������ĺ���
            bullet.Init(attack);
            rgbullet = bullet.GetComponent<Rigidbody2D>();
            // ������Ĺ��﷢���ӵ�  
            rgbullet.AddForce(dir.normalized * movePower);
            //trailRenderer.emitting = true;
            //collider.enabled = true;//�����
            bullet.moveDir = dir;
            
        }
        else
        {//û���ҵ�Ŀ��ɶ�����ɻ��߸ɱ��
            
        }
    }
}