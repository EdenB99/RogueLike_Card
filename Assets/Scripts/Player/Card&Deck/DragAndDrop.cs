using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector3 originalScale;
    private bool isDragging;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = transform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        isDragging = true;
        originalPosition = rectTransform.localPosition;
        transform.localScale = originalScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
        GameObject target = eventData.pointerEnter;
        if (target != null)
        {
           if (target.GetComponent<PlayerPanel>() != null)
            {
                target.GetComponent<PlayerPanel>().OnDrop(eventData);
            } else if (target.GetComponent<EnemyPanel>() != null)
            {
                target.GetComponent<EnemyPanel>().OnDrop(eventData);
            } 
            else
            {
                PositionReSet();
            }
        }
        else PositionReSet();
    }
    public void PositionReSet()
    {
        rectTransform.localPosition = originalPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
        {
            transform.localScale = originalScale * 1.2f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            transform.localScale = originalScale;
        }
    }

   
}
