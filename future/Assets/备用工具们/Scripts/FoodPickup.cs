using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodPickup : MonoBehaviour
{
    public GameObject childCanvas;
    public TMP_Text foodtext;
    public float interactionRange = 10.0f; // �������Ʒ�Ļ�������
    private PlayerController player; // ��Ҷ��������
    public bool power, speed, magnet;
    /*�����������*/
    //public KeyCode interactKey = KeyCode.E; // ��������
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= interactionRange)
        {
            //Debug.Log("��ǰ����Ϊ" + Vector3.Distance(PlayerController.instance.transform.position, transform.position));
            if (power && FoodController.instance.firstpower)
            {
                foodtext.text = "����������";
            }
            if (speed && FoodController.instance.firstspeed)
            {
                foodtext.text = "��ǿ�ٶ�";
            }
            if (magnet&&FoodController.instance.firstmagnet)
            {
                foodtext.text = "����ʰȡ��Χ";
            }

            childCanvas.gameObject.SetActive(true);

            
        }
        else
        {
            // ��Ҳ��ڻ�����Χ�ڣ�������ʾ����
            childCanvas.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //����������������һ��ʱ������
            if (power)
            {
                FoodController.instance.powertimeup();
                FoodController.instance.firstpower = true;
                //Debug.Log("ʳ��1");
            }
            if (speed)
            {
                PlayerController.instance.speedattributeTimer = PlayerController.instance.buffDuration;
                FoodController.instance.firstspeed = true;
                //Debug.Log("ʳ��2");
            }
            if (magnet)
            {
                PlayerController.instance.magnetattributeTimer = PlayerController.instance.buffDuration;
                FoodController.instance.firstmagnet = true;
                //Debug.Log("ʳ��3");
            }
            
            FoodController.instance.existingItemPositions.Remove(this.transform.position);
            Destroy(this.gameObject);
            SFXManager.instance.PlaySFXPitched(1);
        }
        
        
    }
}
