using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCombat : Combat
{
    [SerializeField] Transform actionCam;
    [SerializeField] Transform actionCamTarget;
    public override void Start()
    {
        base.Start();
    }
    public override void Attack()
    {
        Vector3 temp = actionCamTarget.localRotation.eulerAngles;
        temp.x = Random.Range(-10, 20);
        temp.y = Random.Range(-60, 60);
        temp.z = Random.Range(-10, 10);
        actionCamTarget.localRotation = Quaternion.Euler(temp);
        actionCam.gameObject.SetActive(false);
        actionCam.gameObject.SetActive(true);
        Invoke("CloseActCam", 1f);
        base.Attack();
    }
    void CloseActCam()
    {
        actionCam.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isAlive) { return; }
        if (Input.GetMouseButtonDown(0) && movement.canMove && movement.isGrounded)
        {
            Attack();
        }
    }
    public override void CheckEnemy(Vector3 hitPos)
    {
        Collider[] enemies = Physics.OverlapSphere(hitPos, 1f, LayerMask.GetMask("Enemy"));
        foreach (var item in enemies)
        {
            item.GetComponent<Combat>().GetDamage(hitPos, transform,damage);
        }
    }
    protected override void RagdollOn(Vector3 hit)
    {
        movement.charController.enabled = false;
        base.RagdollOn(hit);
    }
}
