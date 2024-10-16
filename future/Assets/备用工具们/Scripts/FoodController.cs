using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FoodController : MonoBehaviour
{
    public static FoodController instance;
    public List<FoodPickup> food;
    public int foodnums = 20;//��ʼ�������20��ʳ��
    public float appear_time = 20.0f,appear_account=20.0f;
    public Transform maxplace, minplace;
    public bool firstpower, firstspeed, firstmagnet;
    //private List<FoodPickup> foodPool = new List<FoodPickup>();
    [HideInInspector]public List<Vector3> existingItemPositions = new List<Vector3>();//����ʳ���Ѿ�ӵ�е�λ��,����������ˢ��
    public float minDistance=20.0f;//ʳ��֮����С�ľ���

    [HideInInspector] public float powerattributeTimer; // �������������ʱ��
    public float powerbuff;
    private float buffedAttribute = 2.0f; // ����������ֵ
    private float buffedlowAttribute = 1.0f;
    private float buffDuration = 10.0f; // �������ʱ��
    public void powertimeup()//ʳ��ǿ��
    {
        powerattributeTimer +=buffDuration;
        powerbuff = buffedAttribute;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        powerbuff = buffedlowAttribute;
        for (int i = 0; i < foodnums; i++)
        {

            float n = Random.Range(0, 9);
            
            if (0 <= n && n < 3)
            {
                //Debug.Log("����ʳ��1");
                food1();
            }
            else
            if (3 <= n && n < 6)
            {
                //Debug.Log("����ʳ��2");
                food2();
            }
            else
            if (6 <= n && n < 9)
            {
                //Debug.Log("����ʳ��3");
                food3();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (powerattributeTimer > 0)
        {
            powerattributeTimer -= Time.deltaTime;
            if (powerattributeTimer <= 0)
            {
                powerbuff = buffedlowAttribute;
            }
        }




        appear_time -= Time.deltaTime;
        if (appear_time <= 0)//ˢ���Ѿ��Թ���ʳ��
        {
            appear_time = appear_account;
            for (int i = existingItemPositions.Count; i < foodnums; i++)
            {
                Debug.Log("���ɼ���ʳ�" + (foodnums - existingItemPositions.Count));
                float n = Random.Range(0, 1);
                if (0 <= n && n < 0.33)
                {
                    food1();
                }
                else
                if (0.33 <= n && n < 0.66)
                {
                    food2();
                }
                else
                if (0.66 <= n && n < 1)
                {
                    food3();
                }

            }
        }
    }
    public void food1()
    {
        Vector3 v = SpawnItem();
        Instantiate(food[0], v, Quaternion.identity, this.transform);

    }
    public void food2()
    {
        Vector3 v = SpawnItem();
        Instantiate(food[1], v, Quaternion.identity, this.transform);
    }
    public void food3()
    {
        Vector3 v = SpawnItem();
        Instantiate(food[2], v, Quaternion.identity, this.transform);
    }
    //public void ItemSpawner(float minDistance, float mapLength, float mapWidth)
    //{
    //    this.minDistance = minDistance;
    //    this.mapLength = mapLength;
    //    this.mapWidth = mapWidth;
    //}

    public Vector3 SpawnItem()//���ɺ����ʳ���λ��
    {
        Vector3 newPosition=Vector3.zero;
        bool isValidPosition = false;

        while (!isValidPosition)
        {
            newPosition = new Vector3(Random.Range(minplace.position.x, maxplace.position.x),
                Random.Range(minplace.position.y, maxplace.position.y ),0);//�������λ��
            isValidPosition = IsPositionValid(newPosition);
        }

        existingItemPositions.Add(newPosition);
        return newPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var existingPosition in existingItemPositions)
        {
            float distance = Vector3.Distance(position, existingPosition);
            if (distance < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
