using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public bool FacingLeft
    {
        get => facingLeft;
    }

    [SerializeField] private float defaultMoveSpeed = 1f;
    private float moveSpeed;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private TrailRenderer dashTrail;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private bool facingLeft = false;
    private bool isDashing = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        moveSpeed = defaultMoveSpeed;
    }

    private void Update() //better for player inputs
    {
        AdjustPlayerFacingDirection();
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
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            //flip player
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (isDashing) return;
        isDashing = true;
        moveSpeed *= dashSpeed;
        dashTrail.emitting = true;
        StartCoroutine(EndDashCoroutine());
    }

    private IEnumerator EndDashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = defaultMoveSpeed;
        dashTrail.emitting = false;
        yield return new WaitForSeconds(dashTime*2.5f);
        isDashing = false;
    }
}