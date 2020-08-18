using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tuto_DragDropLabel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private RectTransform upperLayerRectTransform;
    private CanvasGroup canvasGroup;
    private LineRenderer lineRenderer;
    private Vector2 endPos;
    private Vector2 startPos;
    public GameObject upperLayer;
    public Text labelText;
    public Text codeSlotTitle;
    private string labelName;
    private UIManager uiManager;
    private Actions actionManager;
    private Canvas canvas;
    private GameObject actionBackground;
    private GameObject conditionBackground;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        lineRenderer = GetComponent<LineRenderer>();
        upperLayerRectTransform = upperLayer.GetComponent<RectTransform>();
        lineRenderer.positionCount = 2;
        labelName = labelText.text;
        actionManager = GameObject.Find("UIManager").GetComponent<Actions>();
        actionBackground = GameObject.Find("ActionBackground");
        conditionBackground = GameObject.Find("ConditionBackground");
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (uiManager.boxEnabled == false)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (uiManager.boxEnabled == false)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (uiManager.boxEnabled == false)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (uiManager.boxEnabled == false)
        {

        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiManager.boxEnabled == false)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().AnimateSelected(this.gameObject);
            if (codeSlotTitle.text == labelName || codeSlotTitle.text == "DEFAULT")
            {
                GameObject.Find("UIManager").GetComponent<UIManager>().OpenCodeSlot();
            }

            actionBackground.SetActive(true);
            conditionBackground.SetActive(false);
            _OnPointerClick();
        }
    }

    public void _OnPointerClick()
    {
        print("onpointerclick of label");
        codeSlotTitle.text = labelName;
        conditionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = labelName;
        if (rectTransform.tag == "Label")
        {
            print("Label's _OnPointerClick");
            if (labelName == "이동")
            {
                if (HomeSceneMenuControl.data.tutorialStage == 15)
                {
                    HomeSceneMenuControl.data.tutorialStage++;
                }
                foreach (GameObject eachAction in actionManager.movementOffList)
                    eachAction.SetActive(true);
                foreach (GameObject eachAction in actionManager.attackOffList)
                    eachAction.SetActive(false);
                foreach (GameObject eachAction in actionManager.itemOffList)
                    eachAction.SetActive(false);
            }
            else if (labelName == "공격")
            {
                if (HomeSceneMenuControl.data.tutorialStage == 6)
                {
                    HomeSceneMenuControl.data.tutorialStage++;
                }
                foreach (GameObject eachAction in actionManager.movementOffList)
                    eachAction.SetActive(false);
                foreach (GameObject eachAction in actionManager.attackOffList)
                    eachAction.SetActive(true);
                foreach (GameObject eachAction in actionManager.itemOffList)
                    eachAction.SetActive(false);
            }
            else if (labelName == "아이템")
            {
                foreach (GameObject eachAction in actionManager.movementOffList)
                    eachAction.SetActive(false);
                foreach (GameObject eachAction in actionManager.attackOffList)
                    eachAction.SetActive(false);
                foreach (GameObject eachAction in actionManager.itemOffList)
                    eachAction.SetActive(true);
            }
        }
    }
    void Start()
    {
        Material mat = GetComponent<LineRenderer>().material;
        Tween offsetTween = mat.DOOffset(new Vector2(1, 1), 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    public void Update()
    {
        lineRenderer.sortingOrder = 1;
        startPos = new Vector2(rectTransform.position.x, rectTransform.position.y);
        lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, 100));
        endPos = new Vector2(upperLayerRectTransform.position.x, upperLayerRectTransform.position.y);
        lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y, 100));

    }
}
