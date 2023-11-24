using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _inputs;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Cannon _frontCannon;
    [SerializeField] private Cannon[] _rightCannons;
    [SerializeField] private Cannon[] _leftCannons;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _sideFireDelay;
    private float _fireTime;

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

        _inputs.Player.FireFoward.performed += OnFireFoward;
        _inputs.Player.FireRight.performed += OnFireRight;
        _inputs.Player.FireLeft.performed += OnFireLeft;

        _inputs.Enable();
    }
    private void OnFireFoward(InputAction.CallbackContext context)
    {
        if (_fireTime <= Time.time)
        {
            _frontCannon.Fire(_collider);
            _fireTime = Time.time + _fireRate;
        }
    }
    private void OnFireRight(InputAction.CallbackContext context)
    {
        if (_fireTime <= Time.time)
        {
            for (int i = 0; i < _rightCannons.Length; i++)
            {
                _rightCannons[i].Fire(_collider, _sideFireDelay * i);
            }
            _fireTime = Time.time + _fireRate;
        }
    }
    private void OnFireLeft(InputAction.CallbackContext context)
    {
        if (_fireTime <= Time.time)
        {
            for (int i = 0; i < _leftCannons.Length; i++)
            {
                _leftCannons[i].Fire(_collider, _sideFireDelay * i);
            }
            _fireTime = Time.time + _fireRate;
        }
    }
}
