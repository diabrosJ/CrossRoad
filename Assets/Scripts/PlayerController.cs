using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody =  GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 velocity = direction * moveSpeed;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            curMovementInput = context.ReadValue<Vector2>();
            SetTargetPosition();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isMoving = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    void SetTargetPosition()
    {
        float targetX = transform.position.x;

        if (curMovementInput.x > 0)
        {
            targetX = Mathf.Min(transform.position.x + 1.5f, 1.5f);
        }
        else if (curMovementInput.x < 0)
        {
            targetX = Mathf.Max(transform.position.x - 1.5f, -1.5f);
        }
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        isMoving = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }


}
