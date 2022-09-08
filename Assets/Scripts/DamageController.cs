using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public Material flashMaterial;
    public float duration;
    public ParticleSystem hitPS;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Color originalColor;
    private Coroutine flashRoutine;
    private CameraShake cameraShake;

    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        cameraShake = camera.GetComponent<CameraShake>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    public void CamShake() {
        StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public void HitEffects()
    {
        hitPS.Play();
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        spriteRenderer.color = Color.white; // TEMPORARY
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }


}
