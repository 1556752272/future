using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastMovedoor : MonoBehaviour
{
    public GameObject placeTo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        // �����ײ�Ķ����Ƿ����ض��ı�ǩ������  
        if (collision.CompareTag("Player"))
        {
            // �����ﴦ����ײ�߼������磺���Ŷ�������������ֵ��  
            Debug.Log("121321312");
            collision.gameObject.transform.position=placeTo.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log("121321312");
    }

    // ����һ����ײ���˳�������ʱ���ã���ѡ��  
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log("121321312");
    }
}
