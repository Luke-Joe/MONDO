using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public Material flashMaterial;
    public float duration;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Color originalColor;
    private Coroutine flashRoutine;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Flash() {
        if (flashRoutine != null) {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine() {
        spriteRenderer.material = flashMaterial;
        spriteRenderer.color = Color.white; // TEMPORARY
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

}
