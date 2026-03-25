using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerController playerController;
    private Animator myAnimator;
    
    private ActiveWeapon activeWeapon;
    [SerializeField] private float attackCoolDown=0.1f;
    private bool attackButtonDown,isAttacking=false;
    
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController=GetComponentInParent<PlayerController>();  // GetComponentInParent: ilk parentta bulamazsa �ncekine gidiyor
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
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }
    private void Attack()
    {
        if (attackButtonDown&&!isAttacking)
        {
            StartCoroutine(AttackCoolDownRoutine());
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab,slashAnimSpawnPoint.position,Quaternion.identity); //quaternion identity oldu�u gibi rotasyon gibi bi�i
            slashAnim.transform.parent = transform.parent;
        }
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()   
    {
        attackButtonDown = false;
    }
    private IEnumerator AttackCoolDownRoutine()
    {
        isAttacking=true;
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking=false;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if(playerController.FacingLeft)
        {slashAnim.GetComponent<SpriteRenderer>().flipX = true;}
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(playerController.FacingLeft)
        {slashAnim.GetComponent<SpriteRenderer>().flipX = true;}
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle =Mathf.Atan2(mousePos.y, mousePos.x)*Mathf.Rad2Deg;
        
        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
