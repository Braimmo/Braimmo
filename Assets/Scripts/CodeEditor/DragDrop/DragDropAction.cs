using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DragDropAction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPointerUpHandler
{
    public RectTransform rectTransform;         public RectTransform upperLayerRectTransform;
    CanvasGroup canvasGroup;                    public Vector2 prevPos;
    private Canvas canvas;                      public GameObject parentPrefab;
    public GameObject originalParent;           private PointerEventData eventData;
    private LineRenderer lineRenderer;          public Vector2 endPos;
    public Vector2 startPos;                    private string titleString;
    private UIManager uiManager;                public bool onScreen = false;
    private GameObject actionBackground;        private GameObject conditionBackground;
    private string actionName;                  public Text codeSlotTitle;
    private Conditions conditionManager;        private Actions actionManager;
    public List<GameObject> connectedOnes;      public int tempParentNum;
    public string condDescription;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        lineRenderer = GetComponent<LineRenderer>();
        parentPrefab = GameObject.Find("DragDrop");
        originalParent = this.gameObject.transform.parent.gameObject;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        actionBackground = GameObject.Find("ActionBackground");
        conditionBackground = GameObject.Find("ConditionBackground");
        codeSlotTitle = conditionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        conditionManager = uiManager.GetComponent<Conditions>();
        actionManager = uiManager.GetComponent<Actions>();
        connectedOnes = new List<GameObject>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            _OnBeginDrag();
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            if(uiManager.boxEnabled == false)
            {
                if(this.gameObject.transform.parent.gameObject.name != "DragDrop")
                {
                    prevPos = rectTransform.anchoredPosition;
                    onScreen = false;
                }
            }
        }
    }
    public void _OnBeginDrag()
    {
        tempParentNum = uiManager.GetComponent<Actions>().actionList[int.Parse(gameObject.transform.GetChild(1).name) - 1].actionParent;
        if(tempParentNum == 1)
            titleString = "이동";
        else if(tempParentNum == 2)
            titleString = "공격";
        else if(tempParentNum == 3)
            titleString = "아이템";
        upperLayerRectTransform = GameObject.Find(titleString).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            if(onScreen == false)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

                string tempPointerName = "null";
                string tempPointerTag = "null";
                if(eventData.pointerEnter != null)
                {
                    tempPointerName = eventData.pointerEnter.name;
                    tempPointerTag = eventData.pointerEnter.tag;
                }
                
                if(tempPointerName == "BackgroundImage" || tempPointerTag == "Label" || tempPointerTag == "AddedAction" || tempPointerTag == "AddedCondition")// if it's in the itemSlot panel
                {
                    this.gameObject.transform.SetParent(parentPrefab.transform);
                }
                else if(tempPointerName != "BackgroundImage")// if it's in the itemSlot panel
                {
                    this.gameObject.transform.SetParent(originalParent.transform);
                    lineRenderer.SetPosition(0, new Vector3(0,0,0));
                    lineRenderer.SetPosition(1, new Vector3(0,0,0));
                }
            }
            else
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            if(this.gameObject.transform.parent.gameObject.name != "DragDrop")
            {   
                moveToOriginal();
                this.transform.GetChild(0).tag = "Action";
            }
            else
            {
                this.transform.GetChild(0).tag = "AddedAction";
                onScreen = true;
                this.transform.SetAsFirstSibling();
                GameObject.Find("BackgroundImage").transform.SetAsFirstSibling(); //move BackgroundImage to first child

                //set position x and y and set it to used and set the game object to inactive
                actionManager.actionList[int.Parse(this.transform.GetChild(1).name)-1].actionUsed = 1;
                actionManager.actionList[int.Parse(this.transform.GetChild(1).name)-1].actionPositionX = this.rectTransform.localPosition.x;
                actionManager.actionList[int.Parse(this.transform.GetChild(1).name)-1].actionPositionY = this.rectTransform.localPosition.y;
                    //변경사항 발동
                uiManager.somethingChanged = true;
                    //변경사항 발동                
                if(tempParentNum == 1) //movement
                    modifySelected(ref actionManager.movementOffList, ref actionManager.movementOnList, this.transform.GetChild(1).name);
                else if(tempParentNum == 2) //Attack
                    modifySelected(ref actionManager.attackOffList, ref actionManager.attackOnList, this.transform.GetChild(1).name);
                else if(tempParentNum == 3) //Item
                    modifySelected(ref actionManager.itemOffList, ref actionManager.itemOnList, this.transform.GetChild(1).name);
                
                if(GameObject.Find("ChatBox_CodeEditor"))
                {
                    GameObject.Find(titleString).GetComponent<Tuto_DragDropLabel>()._OnPointerClick();
                }
                else
                {
                    GameObject.Find(titleString).GetComponent<DragDropLabel>()._OnPointerClick();
                }

            }
        }
    }
    public void modifySelected(ref List<GameObject> listToRemove, ref List<GameObject> listToAdd, string actID)
    {
        for(int i = 0; i < listToRemove.Count; i++)
        {
            if(listToRemove[i].name == actID)
            {
                listToRemove[i].SetActive(false);
                listToAdd.Add(listToRemove[i]);
                listToRemove.RemoveAt(i);
            }
        }
    }
    private float downTime;
    private bool isDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false && this.gameObject.transform.parent.gameObject.name == "DragDrop")
        {
                this.isDown = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false && this.gameObject.transform.parent.gameObject.name == "DragDrop")
        {
            if(rectTransform.GetChild(0).tag == "AddedAction")
            {
                this.isDown = false;
                this.downTime = 0;
                destroyDescription();
            }
        }
    }
    void Update()
    {
        if(this.gameObject.transform.parent.gameObject.name == "DragDrop")
        {
            lineRenderer.sortingOrder = 1;
            startPos = new Vector2(rectTransform.position.x, rectTransform.position.y);
            lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y,100));
            endPos = new Vector2(upperLayerRectTransform.position.x, upperLayerRectTransform.position.y);
            lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y,100));
        }
        else
        {
            lineRenderer.sortingOrder = 1;
            startPos = new Vector2(0,0);
            lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y,100));
            endPos = new Vector2(0,0);
            lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y,100));
        }

        if(isDown == true)
        {
            this.downTime += Time.deltaTime;
            if(downTime > 0.5f)
            {
                createDescription();
                _popup.transform.position = new Vector2(this.rectTransform.position.x,this.rectTransform.position.y+30);
            }
            else
            {
                popupCreated = false;
            }
        } 
    }
    private bool descriptionInitiated = false;
    public GameObject popupPrefab;
    private GameObject _popup;
    private bool popupCreated = false;
    void createDescription()
    {
        condDescription = actionManager.actionList[int.Parse(this.transform.GetChild(1).name)-1].actionDescription;
        if(descriptionInitiated == false)
        {
            _popup = (GameObject)Instantiate(popupPrefab, new Vector2(this.rectTransform.position.x,this.rectTransform.position.y+30), Quaternion.identity);
            _popup.transform.SetParent(GameObject.Find("Canvas").transform);
            _popup.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = condDescription;
            _popup.transform.localScale = new Vector2(1,1); // change scale to fit size
            descriptionInitiated = true;
            popupCreated = true;
        }
    }
    void destroyDescription()
    {
        if(descriptionInitiated == true)
        {
            Destroy(_popup);
            descriptionInitiated = false;
        }
    }
    public void moveToOriginal()
    {
        rectTransform.anchoredPosition = prevPos;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false && popupCreated == false)
        {
            if(rectTransform.GetChild(0).tag == "AddedAction")
            {
                GameObject.Find("UIManager").GetComponent<UIManager>().AnimateSelected(this.gameObject);
                actionName = this.name;
                if(codeSlotTitle.text == actionName || codeSlotTitle.text == "DEFAULT")
                    GameObject.Find("UIManager").GetComponent<UIManager>().OpenCodeSlot();
                codeSlotTitle.text = actionName;
                actionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = this.name;
                _OnPointerClick();
            }
        }
    }
    public void _OnPointerClick()
    {
        
        string spriteName =conditionBackground.transform.GetComponent<Image>().sprite.name;
        print(spriteName);
        string tempDescription = uiManager.GetComponent<Actions>().actionList[int.Parse(gameObject.transform.GetChild(1).name) - 1].actionDescription;
        if(rectTransform.GetChild(0).tag == "AddedAction")
        {
            actionBackground.SetActive(false);
            conditionBackground.SetActive(true);

            if(spriteName == "Panel1")
            {
                uiManager.ConditionHpButton();
            }
            else if(spriteName == "Panel2")
            {
                uiManager.ConditionStateButton();
            }
            else if(spriteName == "Panel3")
            {
                uiManager.ConditionRadiusButton();
            }
            else if(spriteName == "Panel4")
            {
                uiManager.ConditionActionButton();
            }
        }  
    }
    void Start()
    {
        //Material mat = GetComponent<LineRenderer>().material;
        //Tween offsetTween = mat.DOOffset(new Vector2(1,1),2).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
    }

}
