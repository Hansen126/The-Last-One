using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverTextColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI textMeshProComponent;
    public Color hoverColor = Color.red;
    private Color originalColor;


    void Start()
    {
        textMeshProComponent = GetComponent<TextMeshProUGUI>();
        if (textMeshProComponent != null)
        {
            originalColor = textMeshProComponent.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textMeshProComponent != null)
        {
            textMeshProComponent.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textMeshProComponent != null)
        {
            textMeshProComponent.color = originalColor;
        }
    }

    void OnDisable()
    {
        if (textMeshProComponent != null)
        {
            textMeshProComponent.color = originalColor;
        }
    }
}
