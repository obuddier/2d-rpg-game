using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController=GetComponentInParent<PlayerController>();  // GetComponentInParent: ilk parentta bulamazsa öncekine gidiyor
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
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
        myAnimator.SetTrigger("Attack");

        slashAnim = Instantiate(slashAnimPrefab,slashAnimSpawnPoint.position,Quaternion.identity); //quaternion identity olduðu gibi rotasyon gibi biþi
        slashAnim.transform.parent = this.transform.parent;

    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle =Mathf.Atan2(mousePos.y, mousePos.x)*Mathf.Rad2Deg;   
        

        if(mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }

        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }

}
