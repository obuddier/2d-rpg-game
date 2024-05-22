using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator animator;
    private void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
