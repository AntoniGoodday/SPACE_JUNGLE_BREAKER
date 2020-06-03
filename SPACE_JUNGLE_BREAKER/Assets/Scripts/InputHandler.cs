using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    GameObject currentlyControlled;
    [SerializeField]
    Rigidbody rb;

    PlayerInfo playerInfo;

    float jumpHeight;

    float speed;

    Camera cam;

    Command jump;

    Vector3 playerMovement;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public GameObject CurrentlyControlled { get => currentlyControlled; set => currentlyControlled = value; }

    private void Start()
    {
        jump = new PerformJump();
        rb = currentlyControlled.GetComponent<Rigidbody>();
        playerInfo = currentlyControlled.GetComponent<PlayerInfo>();

        jumpHeight = playerInfo.JumpHeight;
        speed = playerInfo.Speed;
        cam = playerInfo.PlayerCamera;

        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && playerInfo.IsGrounded == true)
        {
            //REMOVE AFTER FINDING GOOD JUMP HEIGHT
            jumpHeight = playerInfo.JumpHeight;

            jump.Execute(rb, jumpHeight);
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Debug.Log(hor);

        playerMovement = new Vector3(hor, 0, ver).normalized;

        
        

    }

    private void FixedUpdate()
    {
        //currentlyControlled.transform.LookAt(currentlyControlled.transform.position + new Vector3(playerMovement.x, 0, playerMovement.z));

        

        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        

        if (playerMovement.magnitude > 0.1f)
        {

            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentlyControlled.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir * speed, ForceMode.Force);
        }
        else
        {
            
        }


    }
}
