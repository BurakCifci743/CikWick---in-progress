using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed = 20f;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;

    [SerializeField] private bool _canJump;
    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;
    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;
    

    private StateController _stateController;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;


    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }
    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
            Debug.Log("Player Sliding!!");
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
            Debug.Log("Player Moving");
        }
        // zıplama tuşuna basıldı mı ? - karakter zıplamaya uygun mu ? - şuanda yerde misin ? _-_ üçü birden true döndüğü durumda karakter zıplayacak.
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded()) 
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }
    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }


    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when !IsGrounded() => PlayerState.Jump,
            _ when movementDirection == Vector3.zero && IsGrounded() && !isSliding() => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && IsGrounded() && !isSliding() => PlayerState.Move,
            _ when movementDirection != Vector3.zero && IsGrounded() && isSliding() => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && IsGrounded() && isSliding() => PlayerState.SlideIdle,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
       
    }
    private void SetPlayerDrag()
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
         };
    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, limitedVelocity.y, limitedVelocity.z);
        }
    }



    private void SetPlayerJumping()
    {

        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse); // impulse - ani kontrol
    }
    private void ResetJump()
    {
        _canJump = true;
    }
    private bool IsGrounded() // karakterin ayağından zemine doğru ışın gönderip , karakterin yerden mesafesini ölçeceğiz amaç isGrounded true döndürmek. false olduğu sürece zıplamasına izin vermeyeceğiz.
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer); // player height işlemi bu oyun özelinde deneme-yanılma hesabı
    }
    private bool isSliding()
    {
        return _isSliding;
    }

}