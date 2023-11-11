using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;

    
    private bool _isMoving;
    private bool _isOnSlippery;
    private int _doubleJump;
    private Vector2 _initialPosition;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpPower;

    private int horizontal;



    private void Awake()
    {
        //Application.targetFrameRate = 60;

        _doubleJump = 2;
        _speed = 11f;       
        _jumpPower = 24f;
        _initialPosition = new Vector2(-6f, 1f);
        horizontal= 0;

        _isMoving = false;
        _isOnSlippery = false;

        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        horizontal = Mathf.RoundToInt(_playerInput.Player.Move.ReadValue<Vector2>().x);

        if (!IsOnWall(horizontal))
        {
            _isMoving = true;

        }


        if (_playerInput.Player.Restart.triggered)
        {
            RestartLevel();
        }




    }

    void FixedUpdate()
    {


        if (_isMoving && !_isOnSlippery)
        {
            var velocityX = _speed * horizontal;
            _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
            _isMoving = false;
        }

        
        if (_playerInput.Player.Jump.triggered && _doubleJump > 0)
        {
            var vertical = Mathf.RoundToInt(_playerInput.Player.Jump.ReadValue<float>());
            var velocityY = _jumpPower * vertical;
            _rb.velocity = new Vector2(_rb.velocity.x, velocityY);
            _doubleJump--;
        }
    }


    public void PlayerDead()
    {
        //ResetLevel();
        //animation
        //transform.position = _initialPosition;
        RestartLevel();
    }


    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }


    private bool IsGrounded()
    {


        return true;
    }


    private bool IsOnWall(int horizontal)
    {
        
        if (horizontal != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f),
                    new Vector2(horizontal, 0), 0.5f, LayerMask.GetMask("Obstacle"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f),
                    new Vector2(horizontal, 0), 0.5f, LayerMask.GetMask("Ground"));

            /*Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f),
                    new Vector2(horizontal, 0), Color.black);*/
            
            if (hit.transform != null || hit2.transform != null)
            {
                return true;
            }
        }
        return false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, 1.5f,
                    LayerMask.GetMask("Ground")).transform != null)
        {
            _doubleJump = 2;
        }

        if(collision.gameObject.tag == "Slippery")
        {
            _isOnSlippery = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slippery")
        {
            _isOnSlippery = false;
        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
