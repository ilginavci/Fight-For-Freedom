using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Cinemachine;
public class PlayerMovement : Movement
{
    bool isRunning = false;
    float speed;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] float defaultSpeed;
    [SerializeField] float mouseSensivity;
    [SerializeField] float xRotation=0;
    [SerializeField] Transform cameraAim;
    #region Gravity
    [SerializeField] float jumpHeight;
    protected Vector3 velocity = Vector3.zero;
    [SerializeField] Transform groundCheck;
    #endregion Gravity
    protected override void Start()
    {
        base.Start();
        Cursor.lockState =CursorLockMode.Locked;
        Cursor.visible = true;
        speed = defaultSpeed;
    }
    protected override void Update()
    {
        if (!combat.isAlive) { return; }
        anim.SetBool("isGrounded", isGrounded);
        base.Update();
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        if(canMove)
        {
            MoveCharacter();
        }
        Look(mouseX, mouseY);
        isGrounded = Physics.OverlapSphere(groundCheck.position, 0.1f, LayerMask.GetMask("Floor")).Length == 0 ? false : true;
        if (!isGrounded)
        {
            velocity.y += -9.81f * 2* Time.deltaTime;
        }
        Jump();
        Speed();
    }
    void Speed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && canMove)
        {
            speed = defaultSpeed * 1.5f;
            isRunning = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
            isRunning = false;
        }
        float currentFov = cam.m_Lens.FieldOfView;
        if (isRunning)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(currentFov, 60, 0.1f);
        }
        else
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(currentFov, 55, 0.1f);

        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * -9.81f);
            anim.SetTrigger("Jump");
        }
        charController.Move(velocity * Time.deltaTime);
    }
    void Look(float mouseX, float mouseY)
    {
        if (!canMove) return;
        if (Input.GetMouseButton(1))
        {
            Vector3 temp = cameraAim.localRotation.eulerAngles;
            temp.x -= mouseY;
            temp.y += mouseX;
            cameraAim.localRotation = Quaternion.Euler(temp);
            return;
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30, 30);
        cameraAim.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void MoveCharacter()
    {
        Vector3 movementVector= Vector2.zero;
        Vector3 direction= Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            movementVector += transform.forward;
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector -= transform.forward;
            direction -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector -= transform.right;
            direction -= Vector3.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector += transform.right;
            direction += Vector3.right;
        }
     
        if(direction.magnitude >0 )
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);

        }
        Quaternion lookRot = Quaternion.LookRotation(direction);
        transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, lookRot, 0.2f);
        charController.Move(movementVector * speed * Time.deltaTime);
    }
}
