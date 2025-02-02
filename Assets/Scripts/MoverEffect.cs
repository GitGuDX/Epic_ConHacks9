using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField]
    private Color hoverColor = new Color(1f, 0f, 0f, 1f); // Red color (R, G, B, A)
    private Image image;
    private Color originalColor;

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
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image == null) return;
        image.color = originalColor;
    }
}