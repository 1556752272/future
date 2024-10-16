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
        // 检查碰撞的对象是否是特定的标签或类型  
        if (collision.CompareTag("Player"))
        {
            // 在这里处理碰撞逻辑，例如：播放动画、减少生命值等  
            Debug.Log("121321312");
            collision.gameObject.transform.position=placeTo.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log("121321312");
    }

    // 当另一个碰撞器退出触发器时调用（可选）  
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log("121321312");
    }
}
