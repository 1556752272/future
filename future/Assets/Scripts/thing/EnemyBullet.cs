using JKFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int bounceCount = 1; // 反弹次数  
    [SerializeField] private Rigidbody2D rgb;
    //[SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private new Collider2D collider;
    private float movePower;
    private int attack;
    public Vector2 moveDir;
    public string name;
    public void Init(int attacks)
    {
        Invoke("DestoryOnInit", 20);
        attack = attacks;
        bounceCount = 2;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CancelInvoke("DestoryOnInit");
        //StartCoroutine(Destory());
        // TODO:攻击AI碰到后子弹消失
        
        if (collision.gameObject.tag == "Player")
        {

            Player_Controller.Instance.GetHit(attack);
            CancelInvoke("DestoryOnInit");
            StartCoroutine(Destory());

        }
        else 
        {
            CancelInvoke("DestoryOnInit");
            StartCoroutine(Destory());
        }

    }

    private void DestoryOnInit()
    {
        StartCoroutine(Destory());
    }

    IEnumerator Destory()
    {
        rgb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 销毁自身
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
        //int index = LayerMask.GetMask("Environment");
        //RaycastHit2D hit=Physics2D.Raycast(transform.position, moveDir,5f, index);
        //if (hit)
        //{
        //    Vector3 collisionPoint = hit.point;
        //    Vector3 vect = hit.normal;
        //    Vector3 reflect=Vector3.Reflect(moveDir, vect);
        //    moveDir = reflect;  

        //}

    }
}
