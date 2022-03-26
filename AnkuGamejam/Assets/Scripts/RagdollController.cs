using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField]GameObject rigRoot;
    [SerializeField] Animator anim;
    [SerializeField] Collider main;
    Collider[] rigColliders;
    Rigidbody[] rigRigids;
    void Start()
    {
        GetRagdollsElements();
        RagdollOff();
    }
    void RagdollOff()
    {
        for (int i = 0; i < rigRigids.Length; i++)
        {

            rigColliders[i].enabled = true;
            rigRigids[i].isKinematic = true;
        }
    }
    public void RagdollsOn()
    {
        anim.enabled = false;
        main.enabled = false;
        for (int i = 0; i < rigRigids.Length; i++)
        {
            rigColliders[i].enabled = true;
            rigRigids[i].isKinematic = false;
        }
    }
    void GetRagdollsElements()
    {
        rigColliders = rigRoot.GetComponentsInChildren<Collider>();
        rigRigids = rigRoot.GetComponentsInChildren<Rigidbody>();
    }
    public void MakeForceBones(Vector3 pos)
    {
        for (int i = 0; i < rigRigids.Length; i++)
        {
            Vector3 direction = rigRigids[i].transform.position - pos;
            direction = direction.normalized;
            rigRigids[i].AddForceAtPosition(direction * 15 * (1f - (rigRigids[i].transform.position - pos).magnitude), pos, ForceMode.Impulse);
        }
    }
}
