using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : RayCastGun
{
    public Transform orientation;

    float hInput;
    float vInput;
    public float moveSpeed;
    Vector3 moveDirection;
    public float groundDrag;
    public float playerHeight;
    public LayerMask IsGround;
    bool grounded;
    Rigidbody rb;
    public KeyCode JumpKey = KeyCode.Space;

    public float JumpForce;
    public float JumpCoolDown;
    public float airMultiplier;
    bool IsreadytoJump=true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void FixedUpdate()
    {
        Movement();
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, IsGround);
        myInput();
        
        if (grounded)
        {
            Debug.Log("Ground");
            rb.drag = groundDrag;
        }
        else
        {
           
            rb.drag = 0;
        }
    }

    private void myInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(JumpKey) && IsreadytoJump && grounded)
        {
            IsreadytoJump = false;

            Jump();

            Invoke(nameof(ResetJump), JumpCoolDown);
        }
    }

    private void Movement()
    {
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        if (grounded)
        {
            
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
         

        else if (!grounded) {
            
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
       
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        IsreadytoJump = true;
    }

    //Next Scene
    

}
