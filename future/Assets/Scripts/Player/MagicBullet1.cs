using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;

[Pool]
public class MagicBullet1 : Magicbulletdad
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
        Invoke("DestoryOnInit", 20);
        attack = attacks;
        bounceCount = 2;
        //findenemy();

    }
    public void findenemy()
    {
        //Collider[] monsters = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Monster"));

        //if (monsters.Length > 0)
        //{
        //    // 找到最近的怪物  
        //    Collider nearestMonster = monsters[0];
        //    float nearestDistance = Vector3.Distance(transform.position, nearestMonster.transform.position);

        //    for (int i = 1; i < monsters.Length; i++)
        //    {
        //        float distance = Vector3.Distance(transform.position, monsters[i].transform.position);
        //        if (distance < nearestDistance)
        //        {
        //            nearestMonster = monsters[i];
        //            nearestDistance = distance;
        //        }
        //    }
        //    dir=nearestMonster.transform.position-Player_Controller.Instance.transform.position;
        //    // 朝最近的怪物发射子弹  
        //    rgb.AddForce(dir.normalized * movePower);
        //    //trailRenderer.emitting = true;
        //    collider.enabled = true;//很奇怪

        //    Invoke("DestoryOnInit", 20);
        //}
        //else
        //{//没有找到目标销毁自身
        //    CancelInvoke("DestoryOnInit");
        //    Destoryitem();
        //}
    }//已弃用

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CancelInvoke("DestoryOnInit");
        //StartCoroutine(Destory());
        // TODO:攻击AI碰到后子弹消失
        if (collision.gameObject.tag == "Monster")
        {

            // 子弹反弹次数用尽，销毁子弹或执行其他逻辑  
            //Destroy(gameObject);
            CancelInvoke("DestoryOnInit");
            StartCoroutine(Destory());
            collision.gameObject.GetComponent<Monster_Controller>().GetHit(attack);
        }
        else if (collision.gameObject.tag == "Player")
        {

            // 子弹反弹次数用尽，销毁子弹或执行其他逻辑  
            //Destroy(gameObject);
            Player_Controller.Instance.GetHit(1);
            CancelInvoke("DestoryOnInit");
            StartCoroutine(Destory());

        }
        // TODO:碰到环境判断反弹次数来决定是否消失或者别的
        else if(collision.gameObject.tag == "Environment")
        {
            bounceCount--;
            if (bounceCount < 0)
            {
                // 子弹反弹次数用尽，销毁子弹或执行其他逻辑  
                //Destroy(gameObject);
                CancelInvoke("DestoryOnInit");
                StartCoroutine(Destory());
                return;
            }
            else//开始反弹找方向,这里试试看
            {
                MyOnCollision();
            }
        }

    }

    private void DestoryOnInit()
    {
        StartCoroutine(Destory());
    }

    IEnumerator Destory()
    {
        //collider.enabled = false;
        rgb.velocity = Vector3.zero;
        //trailRenderer.emitting = false;
        yield return new WaitForSeconds(0.1f);
        // 销毁自身
        DoDestroy();
    }
    private void Destoryitem()
    {
        //collider.enabled = false;
        // 销毁自身
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
