using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    float jumpHeight = 5;
    [SerializeField]
    float speed = 2;

    [SerializeField]
    Camera playerCamera;

    bool isGrounded = true;

    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Speed { get => speed; set => speed = value; }
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
