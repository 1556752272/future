using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool poison = false;
    public float maxhealth;
    public float health = 5f;
    
    public int igniteDamage = 5; // ��ȼʱÿ�����Ѫֵ  
    public float igniteInterval = 0.5f; // ��ȼ״̬�µ�Ѫ�ļ��ʱ�䣨�룩  
    public int deathDamage = 20; // ����ʱ����Χ������ɵ��˺�  
    public float detectionRadius = 3f; // �����Χ���˵İ뾶  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public  void TakeDamage(float damageToTake) { }
    virtual public void Dying() { }
    virtual public void TakeDamage(float damageToTake, bool shouldKnockback)
    {

    }
    virtual public void TakeDamage(float damageToTake, int fire)
    {

    }
    virtual public void TakeDamage(float damageToTake, bool shouldKnockback,string str)
    {

    }
    virtual public void makeStoned()
    {

    }
    virtual public void makemaxStoned()
    {
        
    }
    virtual public void makeFired(int num)
    {

    }
}
