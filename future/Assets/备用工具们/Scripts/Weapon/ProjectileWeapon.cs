using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;
    public Transform Spawnplace;
    public float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;
    private float weaponnumber,shotTime;

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

        shotCounter -=Time.deltaTime;
        if(shotCounter <= 0)
        {
            shotCounter = shotTime;
            //���д���ʹ��Physics2D.OverlapCircleAll��������Ե�ǰ��Ϸ����transform.position��Ϊ���ģ�
            //�뾶ΪweaponRange * stats[weaponLevel].range��Բ�������ڵ�����Collider2D��
            //whatIsEnemy��һ��LayerMask������ָ��Ӧ�ü����Щ���ϵĵ��ˡ�
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            Debug.Log("***********************������:" + enemies.Length);
            if(enemies.Length > 0)
            {
                for(int i = 0; i < weaponnumber; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0,enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    //ʹ��Mathf.Atan2����ӵ�ǰ��Ϸ����Ŀ����˵Ļ��ȽǶȣ�Ȼ��ת��Ϊ������֮���ȥ90�ȿ�����Ϊ�˵���Ͷ�������ת����
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    //���ݼ�����ĽǶȣ�ʹ��Quaternion.AngleAxis����Ͷ�������ת��
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectile, Spawnplace.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

               //  SFXManager.instance.PlaySFXPitched(6);
            }
        }

    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        shotTime = stats[weaponLevel].timeBetweenAttacks;
        shotCounter = 0f;
        weaponnumber = stats[weaponLevel].amount;
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
