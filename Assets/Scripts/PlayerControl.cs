using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    protected Rigidbody2D _rb;
    public PlayerInput _playerInput;

    private float _fallingSpeed;
    private float _fallDamage;
    public float _rayCastDistance;
    public float _rayCastDownDistance;

    public AudioSource audioSource;

    public GameObject earphone;

    [SerializeField]
    private bool _isMoving;
    [SerializeField]
    private bool _isOnSlippery;
    private bool _isOnBelt;
    private int _doubleJump;
    private Vector2 _initialPosition;

    public bool isOnHeadPhone;


    [SerializeField]
    public float _speed;
    [SerializeField]
    public float _jumpPower;

    public float initialSpeed;
    public float initialJumpPower;

    private int horizontal;

    public GameObject gameMenu;

    private void Awake()
    {
        Application.targetFrameRate = 75;

        _doubleJump = 2;
        _speed = 11f;
        _jumpPower = 24f;
        initialSpeed = _speed;
        initialJumpPower = _jumpPower;


        _initialPosition = new Vector2(-6f, 1f);
        horizontal = 0;

        _rayCastDistance = 1.5f;
        _rayCastDownDistance = 1.5f;
        _fallingSpeed = 0;
        _fallDamage = -50f;

        _isMoving = false;
        _isOnSlippery = false;
        _isOnBelt = false;

        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();

        isOnHeadPhone = false;

        audioSource.volume = 0.3f;

        GetComponent<MusicEffectOnPlayer>().isActive = false;

        //gameMenu = GameObject.Find("CanvasGameMenu");
    }


    private void Update()
    {
        if (_rb.velocity.y < _fallingSpeed)
        {
            _fallingSpeed = _rb.velocity.y;
        }

        if (_fallingSpeed < -100)
        {
            PlayerDead();
        }


        horizontal = Mathf.RoundToInt(_playerInput.Player.Move.ReadValue<Vector2>().x);

        if (!IsOnWall(horizontal))
        {
            _isMoving = true;

        }

        if (_playerInput.Player.ESCMenu.triggered)
        {
            gameMenu.SetActive(true);
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }



        if (_playerInput.Player.Restart.triggered)
        {
            if (!gameMenu.activeSelf)
            {
                PlayerDead();
            }

        }


        if (isOnHeadPhone)
        {
            if (!earphone.activeSelf)
            {
                earphone.SetActive(true);
                //audioSource.Play();
                audioSource.volume = 0.8f;
            }
        }

    }

    void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("walk", false);



        //player movement (normal)
        if (_isMoving && !_isOnSlippery && !_isOnBelt)
        {
            var velocityX = _speed * horizontal;
            _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
            _isMoving = false;


            if (horizontal > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                earphone.GetComponent<SpriteRenderer>().flipX = false;
                earphone.transform.localPosition = new Vector3(-0.83f, earphone.transform.localPosition.y, earphone.transform.localPosition.z);
                GetComponent<Animator>().SetBool("walk", true);
            }
            if (horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                earphone.GetComponent<SpriteRenderer>().flipX = true;
                earphone.transform.localPosition = new Vector3(0.83f, earphone.transform.localPosition.y, earphone.transform.localPosition.z);
                GetComponent<Animator>().SetBool("walk", true);
            }

        }


        //moving on special surface
        if (_isMoving && (_isOnSlippery || _isOnBelt))
        {
            var velocityX = _speed / 4 * horizontal;
            _rb.velocity = new Vector2(_rb.velocity.x + velocityX, _rb.velocity.y);
            _isMoving = false;
            GetComponent<Animator>().SetBool("walk", true);

            if (horizontal > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                earphone.GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetBool("walk", true);
            }
            if (horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                earphone.GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetBool("walk", true);
            }
        }



        //jump
        if (_playerInput.Player.Jump.triggered && _doubleJump > 0)
        {
            GetComponent<Animator>().SetBool("jump", true);
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
        GetComponent<Animator>().SetBool("death", true);
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //transform.position = _initialPosition;
        //RestartLevel();
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
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f),
                    new Vector2(horizontal, 0), _rayCastDistance, LayerMask.GetMask("Obstacle"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f),
                    new Vector2(horizontal, 0), _rayCastDistance, LayerMask.GetMask("Ground"));

            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.2f),
                    new Vector2(transform.position.x, transform.position.y - 0.2f) + new Vector2(horizontal * _rayCastDistance, 0), Color.black);

            if (hit.transform != null || hit2.transform != null)
            {
                return true;
            }
        }
        return false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, _rayCastDownDistance,
                    LayerMask.GetMask("Ground")).transform != null)
        {
            _doubleJump = 2;

            GetComponent<Animator>().SetBool("jump", false);

            if (_fallingSpeed < _fallDamage)
            {
                //lose earphones
                Debug.Log("EARPHONE DROPS");
                audioSource.volume = 0.25f;
                earphone.GetComponent<Earphone>().fly();
                GetComponent<MusicEffectOnPlayer>().isActive = false;
                _fallingSpeed = 0;
            }
        }

        if (collision.gameObject.tag == "Slippery")
        {
            _isOnSlippery = true;

            if (_fallingSpeed < _fallDamage)
            {
                //lose earphones
                Debug.Log("EARPHONE DROPS");
                audioSource.volume = 0.25f;
                earphone.GetComponent<Earphone>().fly();
                GetComponent<MusicEffectOnPlayer>().isActive = false;
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
                audioSource.volume = 0.25f;
                earphone.GetComponent<Earphone>().fly();
                GetComponent<MusicEffectOnPlayer>().isActive = false;
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
        Attempts.attempt++;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, _rayCastDownDistance,
                    LayerMask.GetMask("Ground")).transform != null)
        {
            //_doubleJump = 2;
        }
    }
}
