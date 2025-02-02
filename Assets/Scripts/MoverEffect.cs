using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Color hoverColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField]
    private float transitionDuration = 0.3f; // Duration in seconds

    private Image image;
    private Color originalColor;
    private Coroutine colorTransition;

    void Start()
    {
        image = GetComponent<Image>();

        if (image.color == null)
        {
            Debug.LogWarning("Invalid color on Image component of " + gameObject.name);
            return;
        }
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image == null) return;
        if (colorTransition != null)
            StopCoroutine(colorTransition);
        colorTransition = StartCoroutine(TransitionColor(hoverColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image == null) return;
        if (colorTransition != null)
            StopCoroutine(colorTransition);
        colorTransition = StartCoroutine(TransitionColor(originalColor));
    }

    private IEnumerator TransitionColor(Color targetColor)
    {
        Color startColor = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            image.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        image.color = targetColor;
    }
}