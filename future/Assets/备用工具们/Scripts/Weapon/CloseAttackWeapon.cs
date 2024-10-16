using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    public EnemyDamager damager;
    public Transform Spawnplace;
    private float attackCounter, timeBetweenSpawn, direction;//����Ӧ����attackTime�����Ǻ����
    private float weaponnumber;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        attackCounter -= Time.deltaTime;
        if(attackCounter <= 0)
        {
            attackCounter = timeBetweenSpawn;

            direction = Input.GetAxisRaw("Horizontal");

            if(direction != 0)//�����������һ�����ɵ�ʱ��˳�㸽������
            {
                if(direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }else
                {
                    damager.transform.rotation = Quaternion.Euler(0f,0f,180f);
                }
            }
            
            Instantiate(damager, Spawnplace.position,damager.transform.rotation,transform).gameObject.SetActive(true);

            for(int i = 1; i < weaponnumber; i++)//����һȦ��
            {
                float rot = (360f / weaponnumber) * i;

                Instantiate(damager, Spawnplace.position, Quaternion.Euler(0f,0f,damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }

           //  SFXManager.instance.PlaySFXPitched(9);
            
        }

    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        weaponnumber = stats[weaponLevel].amount;
        attackCounter = 0f;
    }
}
