using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    Combat targetCombat;
    [SerializeField] Transform hitPoint;
    private void Start()
    {
        targetCombat = GetComponentInParent<Combat>();
    }
    public void CheckEnemy()
    {
        targetCombat.CheckEnemy(hitPoint.position);
    }
}
