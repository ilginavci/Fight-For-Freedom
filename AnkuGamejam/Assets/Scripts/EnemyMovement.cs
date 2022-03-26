using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyMovement : Movement
{
    [SerializeField] PlayerCombat player;
    public float attakRadius;
    public float avoidRadius;
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerCombat>();
    }
    protected override void Update()
    {
        if (!combat.isAlive) { return; }
        base.Update();
        if(!canMove)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist <= attakRadius && dist > avoidRadius && canMove)
        {
            GoToPlayer();
        }
        else if(dist <= avoidRadius && canMove && player.isAlive)
        {
            combat.Attack();
        }
        AnimationCheck();
    }
    void AnimationCheck()
    {
        if(agent.velocity.magnitude> 0)
        {
            anim.SetBool("Run",true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    void GoToPlayer()
    {
        Vector3 movePos = Vector3.MoveTowards(player.transform.position, transform.position, avoidRadius);
        agent.SetDestination(movePos);
    }
}
