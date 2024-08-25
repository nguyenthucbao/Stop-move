using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        bot.SetDestination(SeekTarget());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

    private Vector3 SeekTarget()
    {
        return new Vector3(Random.Range(-30f, 30f), 0f, Random.Range(-30f, 30f));

    }
}
