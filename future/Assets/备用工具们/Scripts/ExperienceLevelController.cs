using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{   //���о����ȡ�����������
    public static ExperienceLevelController instance;

    void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1,levelCount = 100;//�ȼ�����100

    [HideInInspector]public List<Weapon> weaponsToUpgrade;
    public Levelupend levelUpText;
    
    void Start()
    {
        
    }

    void Update()
    {
        while(expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f ));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])
        {
            StartCoroutine(WaitForFiveSeconds());
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel],currentLevel);
    }
    IEnumerator WaitForFiveSeconds()
        {
            yield return new WaitForSeconds(0.8f);
            
        }
    public void SpawnExp(Vector3 position,int expValue)//����ֵ����
    {
       Instantiate(pickup, position, Quaternion.identity,this.transform).expValue = expValue;
    }

    void LevelUp()
    {
        //�ȼ�������Ч
        levelUpText.gameObject.SetActive(true);
        
        currentExperience -= expLevels[currentLevel];
        
        currentLevel++;

        if(currentLevel >= expLevels.Count)//��ֹ�ȼ�ͻ������
        {
            currentLevel = expLevels.Count - 1;
        }

        //PlayerController.instance.activeWeapon.LevelUp();

        if (PlayerController.instance.assignWeapons.Count <=0)//���û�����������������
        {
            return;
        }


        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //UIController.instance.levelUpButtons[1].UpdataButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.levelUpButtons[0].UpdataButtonDisplay(PlayerController.instance.assignWeapons[0]);

        //UIController.instance.levelUpButtons[1].UpdataButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButtons[2].UpdataButtonDisplay(PlayerController.instance.unassignedWeapons[1]);
        
        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();

        availableWeapons.AddRange(PlayerController.instance.assignWeapons);//������������ӵ��б���
        for(int i = 0; i < 3; i++) { 
            if(availableWeapons.Count > 0)//���Ѿ�ӵ�е����������3�������ṩ����
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }
        //***********
        //if (PlayerController.instance.assignWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        //{
        //    availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);//������������ϵ�һ��
        //}

        //for (int i = weaponsToUpgrade.Count; i < 3; i++)//�����3������3������ѡ�
        //{
        //    if (availableWeapons.Count > 0)
        //    {
        //        int selected = Random.Range(0, availableWeapons.Count);
        //        weaponsToUpgrade.Add(availableWeapons[selected]);
        //        availableWeapons.RemoveAt(selected);
        //    }
        //}

        for (int i = 0; i < weaponsToUpgrade.Count; i++)//�����⼸����ť������ı��ȵ�
        {
            UIController.instance.levelUpButtons[i].UpdataButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        //PlayerStatController.instance.UpdateDisplay();//�츳ѡ��
    }
}
