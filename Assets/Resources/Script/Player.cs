using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Player : Character
{
    Vector3 nextPoint;
    public LayerMask groundLayer;
    public CounterTime counter = new CounterTime();

    void Start()
    {
        
    }

    void Update()
    {
        nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * 7f;
        //Debug.Log(CheckGround(nextPoint));
        if(CheckGround(nextPoint) && JoystickControl.direct.magnitude > 0f)
        {
            counter.Cancel();
            transform.position = nextPoint;
            transform.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else if (!isAttack)
        {
            counter.Execute();
            ChangeAnim("idle");
            range.RemoveNullTarget();
            if (range.botInRange.Count > 0)
            {
                AttackTarget();
            }
        }else
        {
            counter.Execute();
        }
       
    }

    public void AttackTarget()
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnim("attack");
        counter.Start(OnAttack, 0.5f);
    }
    private void ChangeIsAttack()
    {
        isAttack = false;
    }

    private bool CheckGround(Vector3 point)
    {
        RaycastHit hit;
        return Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer);
    }
   
    public override void OnInit()
    {
        this.enabled = true;
        skin.PlayerEquipItems();
        isDeath = false;
        gameObject.tag = "Bot";
        ChangeAnim("idle");
        indicator.InitTarget(Color.black, 1, "Player");
        base.OnInit();
    }
    public override void OnDeath()
    {
        counter.Cancel();
        GameController.Instance.EndGame();
        this.enabled = false;
        UIManager.Instance.OpenAwardUI(level);
        base.OnDeath();
    }

    public void OnDespawn()
    {
        counter.Cancel();
    }

}
