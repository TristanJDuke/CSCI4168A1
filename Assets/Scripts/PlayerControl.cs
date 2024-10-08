using TMPro;
using UnityEngine.SceneManagement;

using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //player controller vars
    public float moveSpeed; 
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
    private bool canWallJump = false;
    private bool isTouchingWall;
    private float wallCheckDistance = 1;
    [SerializeField] private LayerMask wallMask;
    
    //References
    private CharacterController _characterController;
    private Animator _anim;
    private GameLogic _gameLogic;
    public TextMeshPro hintText;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>(); 
        _gameLogic = FindObjectOfType<GameLogic>();

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
        //set inputs to directions
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        //set the x z and direction
        _moveDirection = new Vector3(moveX, 0, moveZ);
        _moveDirection = transform.TransformDirection(_moveDirection);
        
        //kinda messy but I like how the OR is used so I can have this work for before and after unlocking walljump
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
                //guess
                Jump();
            }
        } 
        else
        {
            _moveDirection *= moveSpeed; //in air movement
        }
        //actually implementing motion
        _characterController.Move(_moveDirection * Time.deltaTime);
        
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
    //the following functions were mainly for the animator
    private void Idle()
    {
        _anim.SetFloat("Speed",0,.1f,Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        _anim.SetFloat("Speed",0.5f,.1f,Time.deltaTime);

    }

    private void Run()
    {
        moveSpeed = runSpeed;
        _anim.SetFloat("Speed",0.7f,.1f,Time.deltaTime);

    }
    //jumping math and walljump animations covered
    private void Jump()
    {
        _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        if (isTouchingWall && canWallJump)
        {
            _anim.SetTrigger("didWallJump");
        }
        else
        {
            _anim.SetTrigger("didJump");
        }
    }
    //used for touching the important stuff
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            canWallJump = true;
            Destroy(other.gameObject);
            Destroy(hintText); //I made this in the event that you need miss going left
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            _gameLogic.LevelCompletion(SceneManager.GetActiveScene().buildIndex); //distinguishing between items
        }
    }
}
