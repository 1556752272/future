using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{   //update�����ƶ�λ��
    public float moveSpeed;//����ֱ�߷����ٶ�

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
