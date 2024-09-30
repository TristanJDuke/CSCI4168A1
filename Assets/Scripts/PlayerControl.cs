using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed; //left public for possible slowing by enemies
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    //movement variable
    private Vector3 _moveDirection;    //directional vector
    
    //falling mechanics
    private Vector3 _velocity;
    
    //ground and gravity
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    
    //jumping
    [SerializeField] private float jumpHeight;
    
    //wall jump mechanic
    [SerializeField] private bool canWallJump = false;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallMask;
    
    //References
    private CharacterController _characterController;
    private Animator anim;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>(); //attach controller to char
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move(); //encapuslation or what have you
    }

    private void Move()
    {
        var transformPosition = transform.position;
        isGrounded = Physics.CheckSphere(transformPosition, groundCheckDistance, groundMask);
        isTouchingWall = Physics.CheckSphere(transformPosition, wallCheckDistance, wallMask);
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
            
        }
        
        float moveZ = Input.GetAxis("Vertical"); //take input
        float moveX = Input.GetAxis("Horizontal"); // left/right for strafing

        _moveDirection = new Vector3(moveX, 0, moveZ); //Tank controls??
        _moveDirection = transform.TransformDirection(_moveDirection);
        
        if ((isGrounded) || (isTouchingWall && canWallJump))
        {
            if (_moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //walking state
                Walk();

            } else if (_moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Running state
                Run();
            
            } else if(_moveDirection == Vector3.zero)
            {
                //Idle State
                Idle();
            }
            _moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        } 
        else
        {
            _moveDirection *= moveSpeed; //in air movement
        }
        
        _characterController.Move(_moveDirection * Time.deltaTime);
        
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed",0,.1f,Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed",0.5f,.1f,Time.deltaTime);

    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed",0.7f,.1f,Time.deltaTime);

    }

    private void Jump()
    {
        _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("didJump");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            canWallJump = true;
            Destroy(other.gameObject);
        }
    }
}
