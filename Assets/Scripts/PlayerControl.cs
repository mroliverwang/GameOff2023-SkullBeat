using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    protected Rigidbody2D _rb;
    private PlayerInput _playerInput;

    private float _fallingSpeed;
    private float _fallDamage;
    private float _rayCastDistance;
    private float _rayCastDownDistance;


    private bool _isMoving;
    private bool _isOnSlippery;
    private bool _isOnBelt;
    private int _doubleJump;
    private Vector2 _initialPosition;




    [SerializeField]
    public float _speed;
    [SerializeField]
    public float _jumpPower;

    public float initialSpeed;
    public float initialJumpPower;

    private int horizontal;



    private void Awake()
    {
        Application.targetFrameRate = 75;

        _doubleJump = 2;
        _speed = 11f;       
        _jumpPower = 24f;
        initialSpeed = _speed;
        initialJumpPower = _jumpPower;


        _initialPosition = new Vector2(-6f, 1f);
        horizontal= 0;

        _rayCastDistance = 0.5f;
        _rayCastDownDistance = 1.5f;
        _fallingSpeed = 0;
        _fallDamage = -40f;

        _isMoving = false;
        _isOnSlippery = false;
        _isOnBelt = false;

        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(_rb.velocity.y < _fallingSpeed)
        {
            _fallingSpeed = _rb.velocity.y;
        }


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

        //player movement (normal)
        if (_isMoving && !_isOnSlippery && !_isOnBelt)
        {
            var velocityX = _speed * horizontal;
            _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
            _isMoving = false;
        }


        //moving on special surface
        if (_isMoving && (_isOnSlippery || _isOnBelt))
        {
            var velocityX = _speed/4 * horizontal;
            _rb.velocity = new Vector2(_rb.velocity.x + velocityX, _rb.velocity.y);
            _isMoving = false;
        }



        //jump
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



    private bool IsOnWall(int horizontal)
    {
        
        if (horizontal != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f),
                    new Vector2(horizontal, 0), _rayCastDistance, LayerMask.GetMask("Obstacle"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f),
                    new Vector2(horizontal, 0), _rayCastDistance, LayerMask.GetMask("Ground"));

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
        if(Physics2D.Raycast(transform.position, Vector2.down, _rayCastDownDistance,
                    LayerMask.GetMask("Ground")).transform != null)
        {
            _doubleJump = 2;

            
            if (_fallingSpeed < _fallDamage)
            {
                //lose earphones
                Debug.Log("EARPHONE DROPS");
                _fallingSpeed = 0;
            }
        }

        if(collision.gameObject.tag == "Slippery")
        {
            _isOnSlippery = true;

            if (_fallingSpeed < _fallDamage)
            {
                //lose earphones
                Debug.Log("EARPHONE DROPS");
                _fallingSpeed = 0;
            }
        }

        if (collision.gameObject.tag == "Belt")
        {
            _isOnBelt = true;

            if (_fallingSpeed < _fallDamage)
            {
                //lose earphones
                Debug.Log("EARPHONE DROPS");
                _fallingSpeed = 0;
            }
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slippery")
        {
            _isOnSlippery = false;
        }

        if (collision.gameObject.tag == "Belt")
        {
            _isOnBelt = false;
        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
