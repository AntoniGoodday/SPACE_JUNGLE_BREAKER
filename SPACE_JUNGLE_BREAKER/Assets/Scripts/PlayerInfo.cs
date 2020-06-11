using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    /// <summary>
    /// This script basically has all the player-specific stats, so we can re-use this in case of different mechas/characters
    /// </summary>
    /// 

    //How high player jumps
    [SerializeField]
    float jumpHeight = 5;
    //How fast player moves
    [SerializeField]
    float speed = 2;
    //How fast player falls when holding space
    [SerializeField]
    float fallMultiplier = 2.5f;
    //How fast player falls when not holding space
    [SerializeField]
    float lowJumpMultiplier = 2f;


    [SerializeField]
    Camera playerCamera;

    //Which layers are considered ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded = false;

    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Speed { get => speed; set => speed = value; }
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public float FallMultiplier { get => fallMultiplier; set => fallMultiplier = value; }
    public float LowJumpMultiplier { get => lowJumpMultiplier; set => lowJumpMultiplier = value; }

    private void Update()
    {
        //Checks in a small radius if the player is touching the ground (and ground is indicated by the "Ground" mask) or nah
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
