using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public float JumpSpeed = 1;
    public float Gravity = 9.8f;
    public float AirControlMultiplier = .5f;
    public float FallingMultiplier = .5f;
    public float JumpingMultiplier = .5f;
    public Transform FeetLocation;
    public Rigidbody2D RigidBody;
    public Camera Camera;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 Direction = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Direction.x -= Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Direction.x += Speed;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        bool grounded = Physics2D.Raycast(FeetLocation.position, Vector2.down, .05f, layerMask);

        // jump
        if (grounded && Input.GetKey(KeyCode.Space))
        {
            Direction += Vector3.up * JumpSpeed;
        }

        if (!grounded)
        {
            //current character velocity
            if (RigidBody.velocity.y < 0)
            {
                //Down
                Direction += Vector3.up * Gravity * FallingMultiplier;
            }
            else
            {
                //Up
                Direction += Vector3.up * Gravity * JumpingMultiplier;
            }
        }

        bool bIsrunning = Mathf.Abs(RigidBody.velocity.x) > .1;
        bool bisjumping = Mathf.Abs(RigidBody.velocity.y) > .5;

       // anim.SetBool("isRunning", bIsrunning && !bisjumping);
        //anim.SetBool("isJumping", bisjumping);
        Debug.Log(bIsrunning + "Running");
        Debug.Log(bisjumping + "Jumping");

        Direction.y = Direction.y + RigidBody.velocity.y;
        RigidBody.velocity = Direction;

        Vector3 cameraposition = Camera.transform.position;
        cameraposition.x = transform.position.x;
        Camera.transform.position = cameraposition;
    }
}