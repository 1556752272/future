using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager damager;
    public Transform Spawnplace;
    private float spawnTime,spawnCounter;//�����ٴ����ɵļ�ʱ

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {   //����յ�statsUpdated == true�����¸�������״̬��ÿ�ε���spawntime,��������������EnemyDamager damager;
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;
            Instantiate(damager, transform).gameObject.SetActive(true);
            
            //Instantiate(damager, new Vector3(0, 0, 0), Quaternion.identity,transform).gameObject.SetActive(true);

             //SFXManager.instance.PlaySFXPitched(10);
        }
    }

    void SetStats()
    {
        //damage�˺� duration����ʱ�� speed = timeBetweenDamage��localScale = range��ǰ������С������
        //    spawntime = timebetweenAttacksÿ���������ɵ�ʱ��
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.attackTime = stats[weaponLevel].speed;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        spawnTime = stats[weaponLevel].timeBetweenAttacks;

        spawnCounter = 0f;

    }
}
