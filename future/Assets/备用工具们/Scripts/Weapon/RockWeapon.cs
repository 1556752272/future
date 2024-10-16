using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWeapon : Weapon
{
    public static RockWeapon instance;
    private void Awake()
    {
        instance = this;
    }
    public float rotateSpeed;
    public Transform Spawnplace;
    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    private float weaponnumber;
    public RockDamagers damager;

    //public GameObject stonePrefab; // ʯͷPrefab
    //public Transform ellipseCenter; // ��Բ�����ĵ�
    //public float rotationSpeed = 1.0f; // ��ת�ٶ�
    //public float generationInterval = 2.0f; // ���ɼ��
    //public float ellipseRadiusX = 5.0f; // ��ԲX��뾶
    //public float ellipseRadiusY = 3.0f; // ��ԲY��뾶

    //private float generationTimer;
    private float radius = 5.0f; // �������ɫ�ľ���  
    public float angleSpacing = 30.0f; // ����֮��ĽǶȼ��  
   
    private List<GameObject> weapons = new List<GameObject>();
    void Start()//������Ը�����������Ƿǳ����޸�
    {
        rotateSpeed = 120f;
        SetStats();
        damager.attackBullet = false;
        damager.makeEnemyStone = false;
        // UIController.instance.levelUpButtons[0].UpdataButtonDisplay(this);
    }


    void Update()
    {
        //holder.rotation = Quaternion.Euler(0f,0f,holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime* damager.timeBetweenDamage));
        //fireballToSpawn.rotation = Quaternion.Euler(0f, 0f, fireballToSpawn.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        //rotateSpeed����תȦ�ٶ�
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation,holder).gameObject.SetActive(true);

            //for (int i = 0; i < weaponnumber; i++)
            //{
            //StartCoroutine(GenerateWeapons());

            //}
            for (int i = 0; i < weaponnumber; i++)
            {
                angleSpacing = 360.0f / weaponnumber;
                float angle = i * angleSpacing * Mathf.Deg2Rad; // ���Ƕ�ת��Ϊ����  
                Vector3 position = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                Quaternion rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 80); // ����Y�����ϣ�Χ��Z����ת  
                Instantiate(fireballToSpawn, position, rotation, holder);
                //weapons.Add(weapon); // ��������ӵ��б��У��������������ѡ��  
            }
            // SFXManager.instance.PlaySFXPitched(8);
        }
        
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
    }
    IEnumerator GenerateWeapons()
    {
        
            for (int i = 0; i < weaponnumber; i++)
            {
                angleSpacing = 360.0f / weaponnumber;
                float angle = i * angleSpacing * Mathf.Deg2Rad; // ���Ƕ�ת��Ϊ����  
                Vector3 position = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                Quaternion rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg+80); // ����Y�����ϣ�Χ��Z����ת  
                Instantiate(fireballToSpawn, position, rotation,holder);
                //weapons.Add(weapon); // ��������ӵ��б��У��������������ѡ��  
            }

            yield return new WaitForSeconds(0.5f); // �ȴ����ɼ��  

    }
    public void SetStats()
    {
        if (weaponLevel == 2)
        {
            damager.addskill();
        }
        if (weaponLevel == 3)
        {
            rotateSpeed *= 1.25f;
        }
        if (weaponLevel == 5)
        {
            damager.makeStone();
        }
        if (weaponLevel == 6)
        {
            PlayerController.instance.assignWeapons.Remove(this);
        }
        damager.damageAmount = stats[weaponLevel].damage;
        damager.attackTime = stats[weaponLevel].speed;
        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;
        weaponnumber = stats[weaponLevel].amount;
        spawnCounter = 0f;
    }
    //void GenerateStone()
    //{
    //    // ����ǶȺ��ٶ�
    //    float randomAngle = Random.Range(0, 360);
    //    float randomSpeed = Random.Range(0.5f, 1.5f) * rotationSpeed;

    //    // ��Բ�ϵĵ�
    //    float stoneX = ellipseRadiusX * Mathf.Cos(Mathf.Deg2Rad * randomAngle);
    //    float stoneY = ellipseRadiusY * Mathf.Sin(Mathf.Deg2Rad * randomAngle);

    //    // ʵ����ʯͷ
    //    GameObject stone = Instantiate(stonePrefab, ellipseCenter.position + new Vector3(stoneX, stoneY, 0), Quaternion.identity);
    //    stone.GetComponent<Rigidbody2D>().velocity = new Vector2(stoneX * randomSpeed, stoneY * randomSpeed);
    //}

    //void RotateEllipse(Transform ellipseCenter, float deltaTime)
    //{
    //    // ��ת��Բ���ĵ�
    //    ellipseCenter.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * deltaTime);
    //}
}
