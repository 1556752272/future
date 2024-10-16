using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;
    [HideInInspector]
    public bool statsUpdated;
    public Sprite icon;
    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            Debug.Log(weaponLevel);
            weaponLevel++;
            statsUpdated = true;
        }
    }
}
[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration,acrossEnemyNums,slowEffects;
    public Sprite ChIcon;
             //�˺�������˺���������С����������ʱ�䣬��������������ʱ�� ����͸��������������Ч�������ٵ���Ч��
    public string upgradeText;
    public string nameLevelText;
    public string nameLevelLittleText;
}
