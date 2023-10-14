using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private int _extraJumps;

    private float _moveInput;
    private bool _facingRight = true;
    private bool _isGrounded;
    private int _currentExtraJumps;

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);

        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);

        if (_facingRight == false && _moveInput > 0)
        {
            Flip();
        }
        else if (_facingRight == true && _moveInput < 0)
        {
            Flip();
        }

        if (_isGrounded == true)
        {
            _currentExtraJumps = _extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _currentExtraJumps > 0)
        {
            _rb.velocity = Vector2.up * _jumpForce;
            _currentExtraJumps--;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _currentExtraJumps == 0 && _isGrounded == true)
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
