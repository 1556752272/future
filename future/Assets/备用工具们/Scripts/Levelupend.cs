using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Levelupend : MonoBehaviour
{
    public float fadeDuration = 1f; // ����ʱ��  
    public float timer = 0f;

    private void Start()
    {
        // �����Ҫ������ʼ���������Ե��� StartFadeOut()  
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fadeDuration)
        {
            this.gameObject.SetActive(false);
            timer = 0f;
        }
    }

  
    
}
