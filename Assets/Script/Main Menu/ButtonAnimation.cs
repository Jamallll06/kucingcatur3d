using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Scale")]
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float clickScale = 0.9f;
    [SerializeField] private float speed = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip clickSound;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ClickAnimation());

        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(
                clickSound,
                Camera.main.transform.position,
                1f);
        }
    }

    private IEnumerator ClickAnimation()
    {
        targetScale = originalScale * clickScale;

        yield return new WaitForSeconds(0.08f);

        targetScale = originalScale * hoverScale;
    }
}