using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public float healAmount = 5.0f; // ÿ�λظ���Ѫ��
    private float healInterval = 2f; // �ظ�Ѫ����ʱ����

    private bool isPlayerInside; // �������Ƿ�����ײ����
    private float healTimer; // ���ڿ��ƻ�Ѫʱ��ļ�ʱ��
    public float healTimelong = 10.0f;
    void OnEnable()
    {
        // �ڽű�����ʱ��ʼ��ʱ
        healTimer = healInterval;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        // ��⵽��ҽ�����ײ��
        if (other.gameObject.tag=="Player")
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ��⵽����뿪��ײ��
        if (other.gameObject.tag == "Player")
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        // ���ټ�ʱ��
        healTimer -= Time.deltaTime;
        healTimelong -= Time.deltaTime;
        if (healTimelong <= 0)
        {
            Destroy(gameObject);
        }
        // �������Ƿ�����ײ�����Ҽ�ʱ���Ѿ�����
        if (isPlayerInside && healTimer <= 0)
        {
            // ���÷����ظ����Ѫ��
            HealPlayer();

            // ���ü�ʱ��
            healTimer = healInterval;
        }
    }

    void HealPlayer()
    {
        // ������һ���������Ի�ȡ��ҵ����ò��ظ�Ѫ��
        // PlayerHealth playerHealth = ...;
        // playerHealth.Heal(healAmount);
        Debug.Log("Player healed for " + healAmount);
        PlayerHealthController.instance.addHealth((int)healAmount);
    }
}
