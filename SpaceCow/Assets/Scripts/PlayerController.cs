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
    public CapsuleCollider2D Collider;
    public CapsuleCollider2D LoseCollider;

    public Camera Camera;
    public SpriteRenderer Render;
    public GameObject screens;
    public GameObject winscreen;
    public GameObject losescreen;
    public GameObject winscreenimage;
    public GameObject losescreenimage;
    public GameObject background;

    public GameObject platformcatus;
    public GameObject platformclouds;
    public GameObject platformstars;
    public AudioSource jumpSound;


    public float CameraSpeed = .1f;

    private Animator anim;
    private bool gameStarted = false;
    private float Nextjumpdelay = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Finish")
        {
            screens.SetActive(true);
            winscreen.SetActive(true);
            winscreenimage.SetActive(true);

            platformcatus.SetActive(false);
            platformclouds.SetActive(false);
            platformstars.SetActive(false);
            background.SetActive(false);

            gameObject.SetActive(false);
        }
        if (col.tag == "Death")
        {
            screens.SetActive(true);
            losescreen.SetActive(true);

            platformcatus.SetActive(false);
            platformclouds.SetActive(false);
            platformstars.SetActive(false);
            background.SetActive(false);

            losescreenimage.SetActive(true);
        }
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
               

        bool jumpclicked = Input.GetKey(KeyCode.Space);

        if (Input.GetMouseButton(0))
        {
            Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 Screen =  Camera.main.WorldToScreenPoint(gameObject.transform.position);
            if (Input.mousePosition.x < Screen.x)
            {
                Direction.x -= Speed;
            }
            else
            {
                Direction.x += Speed;
            }

            jumpclicked = true;
        }

        if (Direction.x >= Speed * .5f)
        {
            Render.flipX = true;
        }
        if (Direction.x <= Speed * .5f)
        {
            Render.flipX = false;
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Finding Layers
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        //Shooting raycast downwards to see if on ground
        bool grounded = Physics2D.Raycast(FeetLocation.position, Vector2.down, .05f, layerMask);

        Nextjumpdelay -= Time.deltaTime;

        // jump
        if (jumpclicked && grounded && Nextjumpdelay <= 0 && RigidBody.velocity.y <= 0)
        {
            Direction += Vector3.up * JumpSpeed;
            gameStarted = true;
            Nextjumpdelay = .2f;
            jumpSound.Play();
        }

        //Sees weather or not player is on ground
        //GRAVITY
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

        anim.SetBool("isRunning", bIsrunning && !bisjumping);
        anim.SetBool("isJumping", bisjumping);
       // Debug.Log(bIsrunning + "Running");
       // Debug.Log(bisjumping + "Jumping");
       // Debug.Log(grounded + "grounded");

        Direction.y = Direction.y + RigidBody.velocity.y;
        RigidBody.velocity = Direction;

        //Enable when on ground, disable when not SO WE CAN JUMP THRU CLOUDS
        Collider.enabled = grounded;

        if (gameStarted)
        {
            Vector3 cameraposition = Camera.transform.position;
            //makes camera move upwards
            cameraposition.y = cameraposition.y + CameraSpeed;
            Camera.transform.position = cameraposition;
        }
    }

    private bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera);
        if (GeometryUtility.TestPlanesAABB(planes, Collider.bounds))
            return true;
        else
            return false;
    }
}