using System.Collections;
using UnityEngine;
public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackDuration = 0.2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBack(Transform damageSource, float knockBachThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBachThrust*rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackCoroutine());
    }
    private IEnumerator KnockBackCoroutine()
    {
        yield return new WaitForSeconds(knockBackDuration);
        rb.velocity = Vector2.zero;
        GettingKnockedBack=false;
    }
}
