using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input handler which lets you control the player (or ant future objects who have a working character controller and playerinfo)
/// </summary>
public class InputHandler : MonoBehaviour
{
    [SerializeField]
    GameObject currentlyControlled;

    PlayerInfo playerInfo;

    CharacterController controller;

    float jumpHeight;

    float speed;

    float fallMultiplier;

    float lowJumpMultiplier;

    Camera cam;

    Command jump;

    Vector3 playerMovement;

    Vector3 velocity;


    public float gravity = -9.81f;
    //How smoothly the player turns
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Which object is currently controlled
    public GameObject CurrentlyControlled { get => currentlyControlled; set => currentlyControlled = value; }

    private void Start()
    {
        //For command pattern, ignore for now
        //jump = new PerformJump();

        playerInfo = currentlyControlled.GetComponent<PlayerInfo>();

        jumpHeight = playerInfo.JumpHeight;
        speed = playerInfo.Speed;
        fallMultiplier = playerInfo.FallMultiplier;
        cam = playerInfo.PlayerCamera;
        lowJumpMultiplier = playerInfo.LowJumpMultiplier;

        controller = currentlyControlled.GetComponent<CharacterController>();

    }

    private void Update()
    {
        //Check if grounded, and make it so velocity does not increase infinitely
        if (playerInfo.IsGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && playerInfo.IsGrounded == true)
        {
            //REMOVE AFTER FINDING GOOD JUMP HEIGHT, AS IT IS FOR REAL TIME JUMP HEIGHT CHANGES IN PLAYERINFO
            jumpHeight = playerInfo.JumpHeight;

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            //Command pattern stuff, ignore for now
            //jump.Execute(jumpHeight);
        }

        //Buttons axis for moving (so this also works for controller ... so far
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Normalizing so that moving diagonally would not be faster
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //When an axis is pressed, it will have a magnitude
        if(direction.magnitude >= 0.1f)
        {
            //Get rotation angle towards movement direction, offset by camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            //Do that smoothly
            float angle = Mathf.SmoothDampAngle(CurrentlyControlled.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            //Rotate
            currentlyControlled.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Make movement camera-dependant
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Move
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        //Add gravity over time
        velocity.y += gravity * Time.deltaTime;

        //Make the jumps more videogame-y, so that if you hold the jump button the gravity is weaker, mario style
        if (velocity.y < 0)
        {
            velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
        {
            velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Add gravity to movement
        controller.Move( new Vector3(0, velocity.y) * Time.deltaTime);


    }
}
