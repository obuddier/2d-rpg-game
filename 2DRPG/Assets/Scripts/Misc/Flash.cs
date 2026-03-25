using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float whiteFlashDuration = 0.2f;

    public float GetRestoreMatTime()
    {
        return whiteFlashDuration;
    }
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat=spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(whiteFlashDuration);
        spriteRenderer.material = defaultMat;
    }
}
