using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossex : MonoBehaviour
{
    public GameObject bossPrefab; // BossԤ��������  
    public Transform pos; // Boss����λ��  
    private bool ff;
    void Start()
    {
        ff = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag=="Player")
        {
            // ʵ����Boss  
            if (!ff)
            { 
                GameObject bossInstance = Instantiate(bossPrefab, pos.position, Quaternion.identity);
                ff = true;

            }
           
            
        }
    }
}
