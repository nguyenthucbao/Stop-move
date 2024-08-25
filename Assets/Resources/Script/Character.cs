using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractCharacter
{
    public Animator animator;
    private string currentAnim;
    public CharacterRange range;
    public Bullet bulletPrefabs;
    public bool isAttack = false;
    public TargetIndicator indicator;
    public int level = 1;
    public bool isDeath = false;
    public InitSkin skin;

    public override void OnInit()
    {
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
    }

    public override void OnAttack()
    {
        Throw();
    }

    public override void OnDeath()
    {
        GameController.Instance.CharacterDead();
        isDeath = true;
        ChangeAnim("dead");
        gameObject.tag = "Untagged";
    }

    public void SetBodyScale()
    {
        transform.localScale = (1 + (level - 1) * 0.15f) * Vector3.one;
    }

    public void GainLevel()
    {
        if (!isDeath)
        {
            level++;
            SetBodyScale();
            indicator.InitTarget(level);
        }
    }

    public void Throw()
    {
        range.RemoveNullTarget();
        if (range.botInRange.Count > 0)
        {
            Bullet bullet = Instantiate(bulletPrefabs);
            bullet.transform.localScale = (1 + (level - 1) * 0.15f) * Vector3.one;

            bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

            bullet.seft = this;
            Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;

            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);

            transform.forward = new Vector3(direction.x, 0f, direction.z);
            //transform.forward = direction;
        }       
    }

    
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {            
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

}
