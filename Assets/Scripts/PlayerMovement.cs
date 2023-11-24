using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _damping;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInputActions _inputs;
    private Vector2 _movementInput;
    private float _velocity;

    private void OnEnable()
    {
        if (_inputs != null)
        {
            _inputs.Enable();
        }
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Awake()
    {
        _inputs = new PlayerInputActions();
        _inputs.Player.Move.performed += GetMoveInput;
        _inputs.Player.Move.canceled += GetMoveInput;
        _inputs.Enable();
    }

    private void FixedUpdate()
    {
        UpdateRotation();
        UpdateSpeed();
    }

    private void UpdateRotation()
    {
        if (_movementInput.x != 0)
        {
            _visualTransform.Rotate(-Vector3.forward * _movementInput.x * _rotationSpeed * (_velocity / _maxSpeed));
        }
    }

    private void UpdateSpeed()
    {
        if (_movementInput.y > 0 && _velocity < _maxSpeed)
        {
            _velocity += _moveSpeed * Time.deltaTime;
        }
        else if (_velocity > 0)
        {
            _velocity -= _damping * Time.deltaTime;
        }
        else
        {
            _velocity = 0;
        }
        _rigidbody.velocity = _visualTransform.up * _velocity;
    }

    private void GetMoveInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
}
