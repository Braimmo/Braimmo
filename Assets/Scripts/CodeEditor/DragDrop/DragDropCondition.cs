using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragDropCondition : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPointerUpHandler
{
    public RectTransform rectTransform;    public RectTransform upperLayerRectTransform;
    CanvasGroup canvasGroup;                public Vector2 prevPos;
    private Canvas canvas;                  public GameObject parentPrefab;
    public GameObject originalParent;       private PointerEventData eventData;
    private LineRenderer lineRenderer;      public Vector2 endPos;
    public Vector2 startPos;                private string titleString;
    private UIManager uiManager;            public bool onScreen = false;
    private GameObject actionBackground;    private GameObject conditionBackground;
    private string conditionName;           public Text codeSlotTitle;
    private Conditions conditionManager;    public List<GameObject> connectedOnes = new List<GameObject>();
    public int alreadyLinked = 0; //0 not used yet, 1 already linked.
    public string itsParentNumber;          public string condDescription;
    public bool isMoving;
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
        isMoving = false;
        print("ismoving is " + isMoving);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            _OnBeginDrag();
            //만약 눌러진 거에 child가 하나라도 있으면 드래그 아예 못하게 막아야함
            if(this.transform.GetChild(0).tag == "Condition" && int.Parse(GameObject.Find(titleString).transform.GetChild(1).name) > 1000 && GameObject.Find(titleString).GetComponent<DragDropCondition>().connectedOnes.Count >0)
            {

            }
            else
            {
                if(alreadyLinked == 0)
                {
                    upperLayerRectTransform = GameObject.Find(titleString).GetComponent<RectTransform>();
                }
                canvasGroup.alpha = 0.6f;
                canvasGroup.blocksRaycasts = false;
                Debug.Log(eventData.pointerEnter.name);
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
        titleString = codeSlotTitle.text; //현재 내가 클릭한 parent의 이름이 나타남.
        print("titleString from _OnBeginDrag is " + titleString);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            isMoving = true;
            if(this.transform.GetChild(0).tag == "Condition" && int.Parse(GameObject.Find(titleString).transform.GetChild(1).name) > 1000 && GameObject.Find(titleString).GetComponent<DragDropCondition>().connectedOnes.Count >0)
            {

            }
            else
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
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            isMoving = false;
            if(this.transform.GetChild(0).tag == "Condition" && int.Parse(GameObject.Find(titleString).transform.GetChild(1).name) > 1000 && GameObject.Find(titleString).GetComponent<DragDropCondition>().connectedOnes.Count >0)
            {
                //도대체 이게 뭐였지???
            }
            else
            {    
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                if(this.gameObject.transform.parent.gameObject.name != "DragDrop")
                {   
                    moveToOriginal();
                    this.transform.GetChild(0).tag = "Condition";
                    alreadyLinked = 0;
                    upperLayerRectTransform = this.GetComponent<RectTransform>();
                }
                else
                {
                    GameObject tempTitleObject = GameObject.Find("DEFAULT").gameObject;
                    int tempConditionID = int.Parse(this.transform.GetChild(1).name)-1001;
                    print("titleString is " + titleString);
                    if(titleString != "DEFAULT")
                        tempTitleObject = GameObject.Find(titleString);
                    else
                        tempTitleObject = GameObject.Find(conditionManager.conditionList[tempConditionID].conditionParent.ToString()).transform.parent.gameObject;
                    print("tempobjecttitle name " + tempTitleObject.name);
                    this.transform.GetChild(0).tag = "AddedCondition";
                    if(onScreen == false)
                        itsParentNumber = tempTitleObject.transform.GetChild(1).name;
                    onScreen = true;
                    this.transform.SetAsFirstSibling();
                    GameObject.Find("BackgroundImage").transform.SetAsFirstSibling(); //move BackgroundImage to first child
                    Debug.Log(titleString + "<-- title string, this.name -->" + this.name);
                    if(tempTitleObject.transform.GetChild(0).tag == "AddedAction" && this.name != titleString && alreadyLinked != 1)
                        tempTitleObject.GetComponent<DragDropAction>().connectedOnes.Add(this.gameObject); //connected ones <-- 이건 모든 or연결 되어 있는 애들이 싹다 여기에 들어가게 됌
                    else if(tempTitleObject.transform.GetChild(0).tag == "AddedCondition" && this.name != titleString && alreadyLinked != 1)
                    {
                        tempTitleObject.GetComponent<DragDropCondition>().connectedOnes = new List<GameObject>();
                        tempTitleObject.GetComponent<DragDropCondition>().connectedOnes.Add(this.gameObject);
                    }
                    alreadyLinked = 1;
                    this.transform.GetChild(1).GetComponent<Text>().text = itsParentNumber;
                    print(itsParentNumber + " = itsParentNum");

                    //set position x and y and set it to used and set the game object to inactive
                    conditionManager.conditionList[tempConditionID].conditionParent = int.Parse(itsParentNumber);
                    conditionManager.conditionList[tempConditionID].conditionPositionX = this.rectTransform.localPosition.x;
                    conditionManager.conditionList[tempConditionID].conditionPositionY = this.rectTransform.localPosition.y;

                    //무슨 아이디가 썼는지 추가해주기
                    conditionManager.conditionList[tempConditionID].usingCharID = GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().characterID;
                        //변경사항 발동
                    uiManager.somethingChanged = true;
                        //변경사항 발동
                    print(conditionManager.conditionList[tempConditionID].conditionName);
                    string condName = this.transform.GetChild(1).name + conditionManager.conditionList[tempConditionID].conditionName;
                    condDescription = conditionManager.conditionList[tempConditionID].conditionDescription;
                    int itsCategory = conditionManager.conditionList[tempConditionID].conditionID / 100; //1 = 체력, 2 = 상태, 3 = 반경
                    if(itsCategory == 1) //체력 HP
                        modifySelected(ref conditionManager.hpOffList, ref conditionManager.hpOnList, condName);
                    else if(itsCategory == 2) //상태 State
                        modifySelected(ref conditionManager.stateOffList, ref conditionManager.stateOnList, condName);
                    else if(itsCategory == 3) //반경 Radius
                        modifySelected(ref conditionManager.radiusOffList, ref conditionManager.radiusOnList, condName);
                    else if(itsCategory == 4) //액션 ActionChecker
                        modifySelected(ref conditionManager.actionCheckerOffList, ref conditionManager.actionCheckerOnList, condName);

                    if(int.Parse(tempTitleObject.transform.GetChild(1).name) > 1000)
                        tempTitleObject.GetComponent<DragDropCondition>()._OnPointerClick();
                    else if(int.Parse(tempTitleObject.transform.GetChild(1).name) > 0)
                        tempTitleObject.GetComponent<DragDropAction>()._OnPointerClick();
                    else
                    {

                    }
                }
            }
        }
    }
    public void modifySelected(ref List<GameObject> listToRemove, ref List<GameObject> listToAdd, string condName)
    {
        for(int i = 0; i < listToRemove.Count; i++)
        {
            if(listToRemove[i].name == condName)
            {
                listToRemove[i].SetActive(false);
                listToAdd.Add(listToRemove[i]);
                listToRemove.Remove(listToRemove[i]);
                print("modifySelected successful!");
            }
        }
    }
    private float downTime;
    private bool isDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            if(rectTransform.GetChild(0).tag == "AddedCondition")
            {
                this.isDown = true;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(uiManager.boxEnabled == false)
        {
            if(rectTransform.GetChild(0).tag == "AddedCondition")
            {
                this.isDown = false;
                this.downTime = 0;
                destroyDescription();
            }
        }
    }

    void Update()
    {
        if(GameObject.Find("UIManager").GetComponent<UIManager>().canIConnectNow == true)
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
        }
        if(isDown == true)
        {
            this.downTime += Time.deltaTime;
            if(downTime > 0.5f && isMoving == false)
            {
                createDescription();
                _popup.transform.position = new Vector2(this.rectTransform.position.x,this.rectTransform.position.y+30);
            }
            else if(downTime > 0.5f && isMoving == true)
            {
                destroyDescription();
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
        condDescription = conditionManager.conditionList[int.Parse(this.transform.GetChild(1).name)-1001].conditionDescription;
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
            if(rectTransform.GetChild(0).tag == "AddedCondition")
            {
                GameObject.Find("UIManager").GetComponent<UIManager>().AnimateSelected(this.gameObject);
                if(codeSlotTitle.text == this.name  || codeSlotTitle.text == "DEFAULT")
                    GameObject.Find("UIManager").GetComponent<UIManager>().OpenCodeSlot();
                codeSlotTitle.text = this.name;
                actionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = this.name;
                _OnPointerClick();
            }
        }
    }
    public void _OnPointerClick()
    {
        string tempDescription = uiManager.GetComponent<Conditions>().conditionList[int.Parse(gameObject.transform.GetChild(1).name) - 1001].conditionDescription;
        string spriteName = conditionBackground.transform.GetComponent<Image>().sprite.name;
        if(rectTransform.GetChild(0).tag == "AddedCondition")
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
