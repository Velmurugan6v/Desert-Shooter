using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    //Player Move Variables
    [SerializeField] float _moveSpeed;
    [SerializeField] CharacterController controller;
    Vector3 moveDirection;

    //Jump Variables
    [SerializeField] Transform _grounCheck;
    [SerializeField] float _jumpValue;
    [SerializeField] LayerMask _grounLayer;
    [SerializeField] bool _isGround;

    //Add Gravity
    [SerializeField] float _gravity;
    private Vector2 _velocity;

    //Player's Health
    [SerializeField] int _health = 100;

    //Third Person Controller
    [Header("TPP SetUp")]
    public bool _ttp;
    public Animator _playerAnimator;


    public int Health 
    { 
        get 
        { 
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        _isGround = Physics.CheckSphere(_grounCheck.transform.position, 0.2f, _grounLayer);

        //Get PlayerInput
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //PlayerInput Multiple with Transform for Movedirection
        moveDirection = transform.right * x + transform.forward * z;

        moveDirection.Normalize();

        controller.Move(moveDirection * _moveSpeed * Time.deltaTime);

        //When Press Space Button to Jump
        if(Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            Jump();
        }

        //Add Gravity to Player
        _velocity.y += _gravity * Time.deltaTime;

        if(_velocity.y<=-2 && _isGround)
        {
            _velocity.y = -2;
        }

        controller.Move(_velocity);



        if (_ttp)
        {
            _isGround = Physics.CheckSphere(_grounCheck.transform.position, 0.2f, _grounLayer);

            //Get PlayerInput
            float hori = Input.GetAxisRaw("Horizontal");
            float forward = Input.GetAxisRaw("Vertical");

            //PlayerInput Multiple with Transform for Movedirection
            moveDirection = transform.right * hori + transform.forward * forward;

            moveDirection.Normalize();

            controller.Move(moveDirection * _moveSpeed * Time.deltaTime);

            if (moveDirection.x!=0 || moveDirection.z!=0)
            {
                _playerAnimator.SetBool("IsWalk", true);
            }
            else
            {
                _playerAnimator.SetBool("IsWalk", false);
            }

            //When Press Space Button to Jump
            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                Jump();
            }

            //Add Gravity to Player
            _velocity.y += _gravity * Time.deltaTime;

            if (_velocity.y <= -2 && _isGround)
            {
                _velocity.y = -2;
            }

            controller.Move(_velocity);
        }

    }

    //jump Method
    void Jump()
    {
        _velocity.y = Mathf.Sqrt(_gravity * -2 * _jumpValue * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_grounCheck.transform.position, 0.2f);
    }

    public void PlayerDeath()
    {
        if (PlayerMovement.instance._ttp)
        {
            _playerAnimator.SetTrigger("Die");
            PlayerMovement.instance.enabled = false;
        }
        //VFXManager.instance.PlayFX();
        PlayerMovement.instance.enabled = false;
    }
    //Health Damage Funtion
    /*public void Damage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            print("Player Die");
            PlayerMovement.instance.enabled = false;
        }
    }*/
}
