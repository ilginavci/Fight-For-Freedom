using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public CharacterController charController; //For player
    public NavMeshAgent agent; //For enemies
    public bool isGrounded;
    public Animator anim;
    public bool canMove = true;
    public float nextMoveTime = 0;
    protected Rigidbody rb;
    protected Combat combat;
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        combat = GetComponent<Combat>();
    }
    protected virtual void Update()
    {

        if (nextMoveTime <= Time.timeSinceLevelLoad)
        {
            canMove = true;
        }
    }

}
