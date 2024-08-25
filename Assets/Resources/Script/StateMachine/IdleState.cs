using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float time = 0f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("idle");
    }
    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if(time > 0.5f)
        {
            bot.ChangeState(new RunningState());
        }

        bot.range.RemoveNullTarget();
        if (bot.range.botInRange.Count > 0 && !bot.isAttack)
        {
            bot.ChangeState(new AttackSate());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
