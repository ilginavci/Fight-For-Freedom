using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class Combat : MonoBehaviour
{
    public float healt;
    public float damage;
    public bool isAlive=true;
    public Movement movement;
    [SerializeField] float attackCoolDown;
    [SerializeField] RagdollController ragdoll;
    [SerializeField] protected Animator anim;
    public virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movement = gameObject.GetComponent<Movement>();
    }
    public virtual void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetInteger("random", (anim.GetInteger("random") + 1) % 5);
        movement.nextMoveTime = Time.timeSinceLevelLoad + attackCoolDown;
        movement.canMove = false;
        //transform.DOMove(transform.forward / 2 + transform.position, .2f);
    }
    public abstract void CheckEnemy(Vector3 hitPos);
    public virtual void GetDamage(Vector3 hit, Transform enemyTransform, float damage)
    {
        if (!isAlive) return;
        Vector3 look = enemyTransform.position;
        look.y = transform.position.y;
        transform.DOLookAt(look, 0.1f);
        healt -= damage;
        if(healt <= 0)
        {
            isAlive = false;
            StartCoroutine(DieProcess(hit));
        }
        else
        {
            StartCoroutine(DamageProcess(hit));
        }
    }
    IEnumerator DamageProcess(Vector3 hit)
    {
        movement.nextMoveTime = Time.timeSinceLevelLoad + 1.5f;
        movement.canMove = false;
        yield return new WaitForSeconds(.1f);
        transform.DOMove(-transform.forward/2 + transform.position, 0.3f);
        anim.SetInteger("DamageNumber", Random.Range(0, 2));
        anim.SetTrigger("GetDamage");
        yield return new WaitForSeconds(1f);
    }
    IEnumerator DieProcess(Vector3 hit)
    {
        movement.canMove = false;
        yield return new WaitForSeconds(.1f);
        transform.DOMove(-transform.forward + transform.position, 0.3f);
        RagdollOn(hit);
    }
    protected virtual void RagdollOn(Vector3 hit)
    {
        ragdoll.RagdollsOn();
        ragdoll.MakeForceBones(hit);
    }
}
