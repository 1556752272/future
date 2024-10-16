using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch1kill : Weapon
{
    //public float rotateSpeed;
    public Transform Spawnplace;
    public float timeBetweenSpawn;
    private float spawnCounter;
    public Skill1 damager;
    public Skill2 heal;
    public Skill1 fireRain; // ����Prefab
    public float minSize = 0.5f; // ��������С�ߴ�
    public float maxSize = 2.0f; // ���������ߴ�
    public float minDuration = 2.0f; // ��������С����ʱ��
    public float maxDuration = 5.0f; // ������������ʱ��
    private float rainDuration = 5.0f;
    private float raincounter;
    public bool fireskill = false;
    public Transform leftR;
    public Transform rightR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireskill) {
            StartCoroutine(SpawnRaindropsFor(rainDuration));
            fireskill = false;
        }
    }
    IEnumerator SpawnRaindropsFor(float duration)
    {
        float startTime = Time.time;
        SFXManager.instance.PlaySFXPitched(5);
        while (Time.time < startTime + duration)
        {
            // ���������ε�λ��
            float randomX = Random.Range(leftR.position.x,rightR.position.x);
            float randomY = leftR.position.y;

            // ʵ�������
            Skill1 s= Instantiate(fireRain, new Vector2(randomX, randomY), Quaternion.identity);
            
            s.lifeTime = Random.Range(minDuration, maxDuration);
            float num=Random.Range(minSize, maxDuration);
            s.transform.localScale = new Vector3(num, num, 1);
            // �ȴ�һ��ʱ�䣬Ȼ���������
            yield return new WaitForSeconds(0.07f);
        }
    }
    public void chansheng()
    {
        Instantiate(damager, Spawnplace.position, Quaternion.identity,this.gameObject.transform).
                    gameObject.SetActive(true);
    }
    public void changeheal()
    {
        Instantiate(heal, Spawnplace.position, Quaternion.identity).
                    gameObject.SetActive(true);
    }
    public void changeFireRain()
    {
        fireskill = true;
        raincounter = rainDuration;
    }
}
