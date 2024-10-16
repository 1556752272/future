using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Follow : Monster_StateBase
{
    public override void Enter()
    {
        // 修改移动状态
        SetMoveState(true);
        // 播放动画
        //PlayerAnimation("Run");
    }

    public override void LateUpdate()
    {
        // 如果我距离玩家非常进，应该去攻击玩家
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 3.0f)//这里要设置一个小小的攻击范围
        {
            stateMachine.ChangeState<Monster_Attack>((int)MonsterStateType.Attack);
            return;
        }
        // 否则一直去追玩家
      //  navMeshAgent.SetDestination(player.transform.position);
    }
}