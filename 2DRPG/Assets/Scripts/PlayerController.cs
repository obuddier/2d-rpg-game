using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    

    private void Awake()
    {
        playerControls= new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()//better for player inputs
    {
        PlayerInput();
    }

    private void FixedUpdate() //better for physics
    {
        Move();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void PlayerInput()
    {
        movement= playerControls.Movement.Move.ReadValue<Vector2>();
        
    }
    private void Move()
    {
        rb.MovePosition(rb.position+movement*(moveSpeed*Time.fixedDeltaTime));
    }
}
