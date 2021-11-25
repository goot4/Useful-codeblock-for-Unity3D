/* Fundamental movement controller using InputSystem Package.
*  Used for a Top-down View game.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveInput;
    private Rigidbody _rigidbody;

    private PlayerState _playerState;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveInput * _playerState.moveSpeed + Vector3.up * _rigidbody.velocity.y;
    }
    public void OnMove(InputValue value)
    {
        var vector = value.Get<Vector2>();
        _moveInput = new Vector3(vector.x, 0, vector.y);
        if (_moveInput.magnitude > 0)
        {
            transform.forward = _moveInput;
        }
    }
}
