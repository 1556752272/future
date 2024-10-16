using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;

[Pool]
public class MagicBullet5 : Magicbulletdad
{
    [SerializeField] private int bounceCount = 1; // 反弹次数  
    [SerializeField] private Rigidbody2D rgb;
    //[SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private new Collider2D collider;
    private float movePower;
    private int attack;
    public Vector2 moveDir;
    //[SerializeField] private float searchRadius = 50f; // 搜索半径 
    public string name;
    public void Init(int attacks)
    {
        Invoke("DestoryOnInit", 0.5f);
        attack = attacks;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Monster")
        {

            collision.gameObject.GetComponent<Monster_Controller>().GetHit(attack);
        }

    }

    private void DestoryOnInit()
    {
        StartCoroutine(Destory());
    }

    IEnumerator Destory()
    {
        yield return new WaitForSeconds(0.1f);
        DoDestroy();
    }
    private void Destoryitem()
    {
        DoDestroy();
    }
    private void DoDestroy()
    {
        this.JKGameObjectPushPool();
    }
    private void MyOnCollision()
    {
        
    }
}
