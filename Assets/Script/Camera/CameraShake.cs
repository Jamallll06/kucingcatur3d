using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private Coroutine shakeCoroutine;
    private Vector3 defaultLocalPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        defaultLocalPosition = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(
            ShakeRoutine(duration, strength)
        );
    }

    private IEnumerator ShakeRoutine(
        float duration,
        float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-1f, 1f) * strength;
            float offsetY = Random.Range(-1f, 1f) * strength;

            transform.localPosition =
                defaultLocalPosition +
                new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }

        transform.localPosition = defaultLocalPosition;
        shakeCoroutine = null;
    }
}