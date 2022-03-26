using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyCombat : Combat
{
    [SerializeField] float radius;
    
    public override void CheckEnemy(Vector3 hitPos)
    {
        Collider[] player = Physics.OverlapSphere(hitPos, 1f, LayerMask.GetMask("Player"));
        if(player.Length >0)
        {
            player[0].GetComponent<Combat>().GetDamage(hitPos, transform, damage); //1 player
        }
    }
    public override void Attack()
    {
        base.Attack();
    }

}
