using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;  // Value for horizontal movement
    [SerializeField] private float _jumpForce = 25.0f;  // Value for vertical movement
    [SerializeField] [Range(0.0f,1.0f)] private float _dragForce = 0.8f;   //Value should be set in range of 0-1 (1 means no friction from 'ground', 0 means friction to high to move on 'ground') 
    
    private Rigidbody2D _rigidBody2D;   //This stores rigidbody in order to affect Player movement using physics 
    [SerializeField] private BoxCollider2D _groundCheck;  //This stores collider 'groundCheck' attached as component to the Player
    [SerializeField] private LayerMask _groundLayer; //This stores Layer related to what's considered 'Ground' Layer
    private float _inputHorizontal; //This stores input value for horizontal movement from Human Player
    private float _inputVertical; //This stores input value for horizontal movement from Human Player
    private bool _isGrounded;   //This is used for checking wheather Player is standing on 'ground' (object within "Ground" Layer)
    
    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (Math.Abs(_inputHorizontal) > 0)
        {
            _rigidBody2D.velocity = new Vector2(_inputHorizontal * _moveSpeed, _rigidBody2D.velocity.y);
        }
        
        if (Math.Abs(_inputVertical) > 0)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x,_inputVertical * _jumpForce);
        }

        //Check if standing on the ground and apply drag force (friction)
        if (CheckGround())
        {
            _rigidBody2D.velocity *= _dragForce;
        }
    }
    void Update()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _groundLayer).Length > 0;
    }
    
}
