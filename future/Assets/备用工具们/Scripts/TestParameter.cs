using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

//���Բ���
public class TestParameter : MonoBehaviour
{
    public static TestParameter instance;
    public bool isIndestructible = false;//�Ƿ��޵�״̬
    
    void Awake()
    {
        if (instance==null)
        {
            TestParameter.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�л��޵�״̬
    public void ToggleIndestructible()
    {
        isIndestructible = !isIndestructible;
    }
    //������Ϸ�ٶ�
    public Slider slider;
    public float timeScale = 1;
    public void AdjustTimeScale()
    {
        timeScale = 1 + slider.value * 10;
        Time.timeScale = timeScale;
    }
}
