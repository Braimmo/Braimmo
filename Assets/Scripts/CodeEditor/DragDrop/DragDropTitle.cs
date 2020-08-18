using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragDropTitle : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private UIManager uiManager;
    private CanvasGroup canvasGroup;
    public GameObject TargetPanel;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = GameObject.Find("charID_DontDestroy").transform.GetComponent<fromCharToCode>().name;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            Debug.Log("OnPointerDown");
            
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TargetPanel.SetActive(true);
    }
}
