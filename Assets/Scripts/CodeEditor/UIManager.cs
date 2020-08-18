using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro; //Text Mesh Pro용으로 필요함.
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public RectTransform codeList;              public GameObject deletePrefab;
    private GameObject parentPrefab;            public int kyleOpenClose = 1; // 1 is closedForm, 0 is openedForm
    public bool boxEnabled = false;             private GameObject[] listOfAddedAction;
    private GameObject[] listOfAddedCondition;  public GameObject beingPressed; //현재 내가 눌러놓은 아이 <-- dotween으로 scale 바뀌는놈
    private Conditions conditionManager;        public GameObject previousAnimatedOne = null;
    private GameObject actionBackground;        private GameObject conditionBackground;
    private Actions actionManager;              public int previousID;
    public bool canIConnectNow = false;         public bool somethingChanged = false;
    public GameObject TargetPanel;              public float cameraScale;
    public float codeSlotX;
    void Awake()
    {
        Screen.SetResolution(1334,750, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        codeList.DOAnchorPos(new Vector2(1000,0), 0f); //Beginning
        actionBackground = GameObject.Find("ActionBackground");
        conditionBackground = GameObject.Find("ConditionBackground");
        cameraScale = GameObject.Find("Main Camera").GetComponent<CameraResolution>().cameraScale;
        codeSlotX = GameObject.Find("Main Camera").GetComponent<CameraResolution>().codeSlotX;
    }
    void Start()
    {
        initializeAction();
        initializeCondition();
        initializeRectTransform();
        canIConnectNow = true;

        
    }
    public void BlackImageClick()
    {
        TargetPanel.SetActive(false);
    }
    public void initializeAction()
    {
        actionManager = gameObject.GetComponent<Actions>();
        GameObject parentPrefab = GameObject.Find("DragDrop");

        for(int i = 0; i < actionManager.movementOnList.Count; i++)
        {
            GameObject tempOb = actionManager.movementOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeAction(tempOb,i,parentPrefab);
        }

        for(int i = 0; i < actionManager.attackOnList.Count; i++)
        {
            GameObject tempOb = actionManager.attackOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeAction(tempOb,i,parentPrefab);
        }

        for(int i = 0; i < actionManager.itemOnList.Count; i++)
        {
            GameObject tempOb = actionManager.itemOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeAction(tempOb,i,parentPrefab);
        }
    }
    void _initializeAction(GameObject tempOb, int i, GameObject parentPrefab)
    {
        tempOb.transform.GetChild(0).tag = "AddedAction";
        tempOb.GetComponent<DragDropAction>().prevPos = tempOb.GetComponent<RectTransform>().anchoredPosition;
        tempOb.GetComponent<DragDropAction>().onScreen = true;
        tempOb.transform.SetParent(parentPrefab.transform);
        tempOb.transform.SetAsFirstSibling();
        GameObject.Find("BackgroundImage").transform.SetAsFirstSibling();

        tempOb.GetComponent<RectTransform>().anchoredPosition = new Vector2(actionManager.actionList[int.Parse(tempOb.transform.GetChild(1).name)-1].actionPositionX, actionManager.actionList[int.Parse(tempOb.transform.GetChild(1).name)-1].actionPositionY);
        tempOb.GetComponent<DragDropAction>()._OnBeginDrag();
    }
    public void initializeCondition()
    {
        conditionManager = gameObject.GetComponent<Conditions>();
        Debug.Log("set conditionManager"); 
        GameObject parentPrefab = GameObject.Find("DragDrop");
        for(int i = 0; i < conditionManager.hpOnList.Count; i++)
        {
            GameObject tempOb = conditionManager.hpOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeCondition(tempOb, i, parentPrefab);
        }
        for(int i = 0; i < conditionManager.stateOnList.Count; i++)
        {
            GameObject tempOb = conditionManager.stateOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeCondition(tempOb, i, parentPrefab);
        }
        for(int i = 0; i < conditionManager.radiusOnList.Count; i++)
        {
            GameObject tempOb = conditionManager.radiusOnList[i].transform.GetChild(0).GetChild(3).gameObject;
            _initializeCondition(tempOb, i, parentPrefab);
        }
    }
    void _initializeCondition(GameObject tempOb, int i, GameObject parentPrefab)
    {
        tempOb.transform.GetChild(0).tag = "AddedCondition";
        tempOb.GetComponent<DragDropCondition>().prevPos = tempOb.GetComponent<RectTransform>().anchoredPosition;
        tempOb.GetComponent<DragDropCondition>().onScreen = true;
        tempOb.transform.SetParent(parentPrefab.transform);
        tempOb.transform.SetAsFirstSibling();
        GameObject.Find("BackgroundImage").transform.SetAsFirstSibling();
        tempOb.GetComponent<DragDropCondition>()._OnBeginDrag();
        tempOb.GetComponent<DragDropCondition>().alreadyLinked = 1;
        tempOb.transform.GetChild(1).GetComponent<Text>().text = conditionManager.conditionList[int.Parse(tempOb.transform.GetChild(1).name)-1001].conditionParent.ToString();
        string parentNumText = tempOb.transform.GetChild(1).GetComponent<Text>().text;
        tempOb.GetComponent<DragDropCondition>().itsParentNumber = parentNumText;
        float condPosX = conditionManager.conditionList[int.Parse(tempOb.transform.GetChild(1).name)-1001].conditionPositionX;
        float condPosY = conditionManager.conditionList[int.Parse(tempOb.transform.GetChild(1).name)-1001].conditionPositionY;
        tempOb.GetComponent<RectTransform>().anchoredPosition = new Vector2(condPosX, condPosY);
    }
    public void initializeRectTransform()
    {
        conditionManager = gameObject.GetComponent<Conditions>();
        GameObject parentPrefab = GameObject.Find("DragDrop");

        for(int i = 0; i < conditionManager.hpOnList.Count; i++)
        {
            GameObject tempOb = GameObject.Find(conditionManager.hpOnList[i].transform.GetChild(0).GetChild(0).name + conditionManager.hpOnList[i].transform.GetChild(0).GetChild(1).name);
            _initializeRectTransform(tempOb, i, parentPrefab);
        }

        for(int i = 0; i < conditionManager.stateOnList.Count; i++)
        {
            GameObject tempOb = GameObject.Find(conditionManager.stateOnList[i].transform.GetChild(0).GetChild(0).name + conditionManager.stateOnList[i].transform.GetChild(0).GetChild(1).name);
            _initializeRectTransform(tempOb, i, parentPrefab);
        }

        for(int i = 0; i < conditionManager.radiusOnList.Count; i++)
        {
            GameObject tempOb = GameObject.Find(conditionManager.radiusOnList[i].transform.GetChild(0).GetChild(0).name + conditionManager.radiusOnList[i].transform.GetChild(0).GetChild(1).name);
            _initializeRectTransform(tempOb, i, parentPrefab);
        }
    }
    void _initializeRectTransform(GameObject tempOb, int i, GameObject parentPrefab)
    {
            string parentNumText = tempOb.transform.GetChild(1).GetComponent<Text>().text;
            print("_initializeRectTransform parentNumText = " + parentNumText);
            tempOb.GetComponent<DragDropCondition>().upperLayerRectTransform = GameObject.Find(parentNumText).transform.parent.GetComponent<RectTransform>();
            if(int.Parse(parentNumText) < 1000)
            {
                GameObject.Find(parentNumText).transform.parent.GetComponent<DragDropAction>().connectedOnes.Add(tempOb); //connected ones <-- 이건 모든 or연결 되어 있는 애들이 싹다 여기에 들어가게 됌
            }
            else
            {
                GameObject.Find(parentNumText).transform.parent.GetComponent<DragDropCondition>().connectedOnes = new List<GameObject>();
                GameObject.Find(parentNumText).transform.parent.GetComponent<DragDropCondition>().connectedOnes.Add(tempOb);
            }
    }
    public void OpenCodeSlot()
    {
        if(kyleOpenClose == 1)
        {
            //codeSlotX or 462?
            codeList.DOAnchorPos(new Vector2(codeSlotX,0), 0.7f); //Shows Target Priority //460 or 450
            kyleOpenClose = 0;
        }
        else if(kyleOpenClose == 0)
        {
            codeList.DOAnchorPos(new Vector2(1000,0), 0.7f); //Closes List Panel
            LeanTween.cancelAll();
            previousAnimatedOne.transform.localScale = new Vector2(1,1);
            kyleOpenClose = 1;
        }
    }
    public void CloseCodeSlot()
    {
        if(previousAnimatedOne != null)
        {
            codeList.DOAnchorPos(new Vector2(1000,0), 0.7f); //Closes List Panel
            LeanTween.cancelAll();
            previousAnimatedOne.transform.localScale = new Vector2(1,1);
            kyleOpenClose = 1;
        }
    }
    public void ConditionHpButton()
    {
        _ConditionSetActive(true,false,false,false);
        GameObject.Find("ConditionBackground").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/"+"Panel1");
    }
    public void ConditionStateButton()
    {
        _ConditionSetActive(false,true,false,false);
        GameObject.Find("ConditionBackground").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/"+"Panel2");
    }
    public void ConditionRadiusButton()
    {
        _ConditionSetActive(false,false,true,false);
        GameObject.Find("ConditionBackground").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/"+"Panel3");
    }
    public void ConditionActionButton()
    {
        _ConditionSetActive(false,false,false,true);
        GameObject.Find("ConditionBackground").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/"+"Panel4");
    }
    void _ConditionSetActive(bool forHP, bool forState, bool forRadius, bool forActionChecker)
    {
        foreach(GameObject eachAction in conditionManager.hpOffList)
            eachAction.SetActive(forHP);
        foreach(GameObject eachAction in conditionManager.stateOffList)
            eachAction.SetActive(forState);
        foreach(GameObject eachAction in conditionManager.radiusOffList)
            eachAction.SetActive(forRadius);
        foreach(GameObject eachAction in conditionManager.actionCheckerOffList)
            eachAction.SetActive(forActionChecker);
    }
    public void InventoryButton()
    {
        listOfAddedAction = GameObject.FindGameObjectsWithTag("AddedAction");
        listOfAddedCondition = GameObject.FindGameObjectsWithTag("AddedCondition");
        CloseCodeSlot();
        if(boxEnabled == false)
        {
            for(int i = 0; i < listOfAddedAction.Length; i++)
            {
                parentPrefab = listOfAddedAction[i].transform.parent.gameObject;
                GameObject deleteButton = (GameObject)Instantiate(deletePrefab, new Vector2(listOfAddedAction[i].transform.position.x+3f,listOfAddedAction[i].transform.position.y+3f), Quaternion.identity);
                deleteButton.transform.SetParent(parentPrefab.transform); //put it inside the content prefab
                deleteButton.transform.localScale = new Vector2(0,0); // change scale to fit size    
                LeanTween.scale(deleteButton,new Vector2(1f,1f),0.3f).setDelay(0.05f);
            }
            for(int i = 0; i < listOfAddedCondition.Length; i++)
            {
                parentPrefab = listOfAddedCondition[i].transform.parent.gameObject;
                GameObject deleteButton = (GameObject)Instantiate(deletePrefab, new Vector2(listOfAddedCondition[i].transform.position.x+3f,listOfAddedCondition[i].transform.position.y+3f), Quaternion.identity);
                deleteButton.transform.SetParent(parentPrefab.transform); //put it inside the content prefab
                deleteButton.transform.localScale = new Vector2(0,0); // change scale to fit size    
                LeanTween.scale(deleteButton,new Vector2(1f,1f), 0.3f).setDelay(0.05f);
            } 
            boxEnabled = !boxEnabled;
            GameObject.Find("InventoryImage").GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+"OpenedBox");
        }
        else
        {
            for(int i = 0; i < listOfAddedAction.Length; i++)
                listOfAddedAction[i] = null;

            for(int i = 0; i < listOfAddedCondition.Length; i++)
                listOfAddedCondition[i] = null;

            GameObject[] destroyDeleteButton = GameObject.FindGameObjectsWithTag("DeleteButton");
            print("go to Delete Button");

            for(int i = 0; i < destroyDeleteButton.Length; i++)
                Destroy(destroyDeleteButton[i]);

            boxEnabled = !boxEnabled;
            actionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "DEFAULT";
            conditionBackground.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "DEFAULT";
            GameObject.Find("InventoryImage").GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+"ClosedBox");
        }
    }
    public void deleteButton()
    {
        print("delete button pressed");
            //변경사항 발동
        somethingChanged = true;
            //변경사항 발동                
        GameObject toDestroy = EventSystem.current.currentSelectedGameObject;   //X버튼
        GameObject parentToDestroy = toDestroy.transform.parent.gameObject;     //동그란 버튼
        print(parentToDestroy.name);

        DragDropAction actionScript = parentToDestroy.GetComponent<DragDropAction>();
        DragDropCondition conditionScript = parentToDestroy.GetComponent<DragDropCondition>();
        GameObject[] referenceIDList = GameObject.FindGameObjectsWithTag("ReferenceID");
        if(conditionScript==null && actionScript.connectedOnes.Count > 0) //Action이면서 줄줄이 달려있을 경우
        {
            print("Action이면서 줄줄이 달려있을 경우");
            while(actionScript.connectedOnes.Count > 0)
                deleteButtonProcess(parentToDestroy, actionScript.connectedOnes[0], "Condition"); //무조건 0 밖에 없을거임 Condition은 한개밖에 안달리기 때문에

            parentToDestroy.transform.GetChild(0).tag = "Action";
            Destroy(toDestroy);
            parentToDestroy.transform.SetParent(actionScript.originalParent.transform);
            actionScript.moveToOriginal();
            actionScript.startPos = new Vector2(0,0);
            actionScript.endPos = new Vector2(0,0);
            actionScript.onScreen = false;
            
            actionManager = GameObject.Find("UIManager").GetComponent<Actions>();
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionPositionX = 0f;
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionPositionY = 0f;
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionUsed = 2;

            int tempParentNum = actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionParent;
            if(tempParentNum == 1) //movement
                actionScript.modifySelected(ref actionManager.movementOnList, ref actionManager.movementOffList, parentToDestroy.transform.GetChild(1).name);
            else if(tempParentNum == 2) //Attack
                actionScript.modifySelected(ref actionManager.attackOnList, ref actionManager.attackOffList, parentToDestroy.transform.GetChild(1).name);
            else if(tempParentNum == 3) //Item
                actionScript.modifySelected(ref actionManager.itemOnList, ref actionManager.itemOffList, parentToDestroy.transform.GetChild(1).name);
            
        }
        else if(actionScript==null && conditionScript.connectedOnes.Count > 0) //Condition이면서 줄줄이 달려있을 경우
        {
            deleteButtonProcess(parentToDestroy, conditionScript.connectedOnes[0], "Condition"); //무조건 0 밖에 없을거임 Condition은 한개밖에 안달리기 때문에
            parentToDestroy.transform.GetChild(0).tag = "Condition";
            Destroy(toDestroy);
            parentToDestroy.transform.SetParent(conditionScript.originalParent.transform);
            string tempText = (parentToDestroy.transform.GetChild(1).GetComponent<Text>().text);
            if(int.Parse(tempText) > 1000)
                GameObject.Find(tempText).transform.parent.gameObject.GetComponent<DragDropCondition>().connectedOnes = new List<GameObject>();
            else
                GameObject.Find(tempText).transform.parent.GetComponent<DragDropAction>().connectedOnes.Remove(parentToDestroy);
            conditionScript.moveToOriginal();
            conditionScript.alreadyLinked = 0;
            conditionScript.startPos = new Vector2(0,0);
            conditionScript.endPos = new Vector2(0,0);
            conditionScript.onScreen = false;

            conditionManager = GameObject.Find("UIManager").GetComponent<Conditions>();
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionPositionX = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionPositionY = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionParent = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].usingCharID = -1;

            string condName = parentToDestroy.transform.GetChild(1).name + conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionName;
            int tempParentNum  = conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionID/100;
            if(tempParentNum == 1) //hp
                conditionScript.modifySelected(ref conditionManager.hpOnList, ref conditionManager.hpOffList, condName);
            else if(tempParentNum == 2) //state
                conditionScript.modifySelected(ref conditionManager.stateOnList, ref conditionManager.stateOffList, condName);
            else if(tempParentNum == 3) //radius
                conditionScript.modifySelected(ref conditionManager.radiusOnList, ref conditionManager.radiusOffList, condName);
        }
        else if(conditionScript==null && actionScript.connectedOnes.Count==0) //Action이면서 달려있는 애들이 없는 경우
        {
            parentToDestroy.transform.GetChild(0).tag = "Action";
            Destroy(toDestroy);
            parentToDestroy.transform.SetParent(actionScript.originalParent.transform); //put it inside the content prefab
            actionScript.moveToOriginal();
            actionScript.startPos = new Vector2(0,0);
            actionScript.endPos = new Vector2(0,0);
            actionScript.onScreen = false;
            Actions actionManager = GameObject.Find("UIManager").GetComponent<Actions>();
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionPositionX = 0f;
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionPositionY = 0f;
            actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionUsed = 2;

            int tempParentNum = actionManager.actionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1].actionParent;
            if(tempParentNum == 1) //movement
                actionScript.modifySelected(ref actionManager.movementOnList, ref actionManager.movementOffList, parentToDestroy.transform.GetChild(1).name);
            else if(tempParentNum == 2) //Attack
                actionScript.modifySelected(ref actionManager.attackOnList, ref actionManager.attackOffList, parentToDestroy.transform.GetChild(1).name);
            else if(tempParentNum == 3) //Item
                actionScript.modifySelected(ref actionManager.itemOnList, ref actionManager.itemOffList, parentToDestroy.transform.GetChild(1).name);
        }
        else //Condition이면서 달려있는 애들이 없을 경우
        {
            parentToDestroy.transform.GetChild(0).tag = "Condition";
            Destroy(toDestroy);
            parentToDestroy.transform.SetParent(conditionScript.originalParent.transform);
            string tempText = (parentToDestroy.transform.GetChild(1).GetComponent<Text>().text);
            if(int.Parse(tempText) > 1000)
                GameObject.Find(tempText).transform.parent.gameObject.GetComponent<DragDropCondition>().connectedOnes = new List<GameObject>();
            else
                GameObject.Find(tempText).transform.parent.GetComponent<DragDropAction>().connectedOnes.Remove(parentToDestroy);
            conditionScript.moveToOriginal();
            conditionScript.alreadyLinked = 0;
            conditionScript.startPos = new Vector2(0,0);
            conditionScript.endPos = new Vector2(0,0);
            conditionScript.onScreen = false;
            conditionManager = GameObject.Find("UIManager").GetComponent<Conditions>();
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionPositionX = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionPositionY = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionParent = 0;
            conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].usingCharID = -1;
            string condName = parentToDestroy.transform.GetChild(1).name + conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionName;
            print("condName = " + condName);
            int tempParentNum  = conditionManager.conditionList[int.Parse(parentToDestroy.transform.GetChild(1).name)-1001].conditionID/100;
            if(tempParentNum == 1) //hp
                conditionScript.modifySelected(ref conditionManager.hpOnList, ref conditionManager.hpOffList, condName);
            else if(tempParentNum == 2) //state
                conditionScript.modifySelected(ref conditionManager.stateOnList, ref conditionManager.stateOffList, condName);
            else if(tempParentNum == 3) //radius
                conditionScript.modifySelected(ref conditionManager.radiusOnList, ref conditionManager.radiusOffList, condName);

        }
    }
    public void deleteButtonProcess(GameObject parentReference, GameObject toDelete, string tagName) //toDelete은 조건 동그라미, parentReerence 는 
    {
        //여기서 toDelete가 DPSMT같은 아이. X버튼 지우려면 따로 child 타서 들어가야됌
        print("DeleteButtonProcess Starts from Here");
        if(toDelete.GetComponent<DragDropCondition>().connectedOnes.Count != 0)
            deleteButtonProcess(toDelete ,toDelete.GetComponent<DragDropCondition>().connectedOnes[0],"Condition");
        DragDropCondition conditionScript = toDelete.GetComponent<DragDropCondition>();
        toDelete.transform.GetChild(0).tag = "Condition";
        Destroy(toDelete.transform.GetChild(2).gameObject);
        toDelete.transform.SetParent(conditionScript.originalParent.transform);
        conditionScript.moveToOriginal();
        conditionScript.alreadyLinked = 0;
        conditionScript.startPos = new Vector2(0,0);
        conditionScript.endPos = new Vector2(0,0);
        conditionScript.onScreen = false;
            conditionManager = GameObject.Find("UIManager").GetComponent<Conditions>();
            conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].conditionPositionX = 0;
            conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].conditionPositionY = 0;
            conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].conditionParent = 0;
            conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].usingCharID = -1;
            string condName = toDelete.transform.GetChild(1).name + conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].conditionName;
            int tempParentNum  = conditionManager.conditionList[int.Parse(toDelete.transform.GetChild(1).name)-1001].conditionID/100;
            if(tempParentNum == 1) //hp
                conditionScript.modifySelected(ref conditionManager.hpOnList, ref conditionManager.hpOffList, condName);
            else if(tempParentNum == 2) //state
                conditionScript.modifySelected(ref conditionManager.stateOnList, ref conditionManager.stateOffList, condName);
            else if(tempParentNum == 3) //radius
                conditionScript.modifySelected(ref conditionManager.radiusOnList, ref conditionManager.radiusOffList, condName);
        if(int.Parse(parentReference.transform.GetChild(1).name) > 1000)
            parentReference.GetComponent<DragDropCondition>().connectedOnes = new List<GameObject>();
        else
            parentReference.GetComponent<DragDropAction>().connectedOnes.Remove(toDelete);
    }
    public void AnimateSelected(GameObject toAnimate)
    {
        float scaleValue = 1.2f;
        if(previousAnimatedOne != null)
        {
            LeanTween.cancelAll();
            previousAnimatedOne.transform.localScale = new Vector2(1,1);
        }    
        LeanTween.scale(toAnimate,new Vector2 (scaleValue,scaleValue),0.5f).setLoopPingPong();
        previousID = LeanTween.scale(toAnimate,new Vector2 (scaleValue,scaleValue),0.5f).setLoopPingPong().id;
        previousAnimatedOne = toAnimate;
    }
    public bool goBackPopUpOpened = false;
    public GameObject goBackPopUp;
    public void GoBack()
    {
        if(boxEnabled == false)
        {
            if(somethingChanged == false)
            {
                SaveUnchanged();
            }
            else
            {
                print("GoBack Clicked");
                if(goBackPopUpOpened == false)
                {
                    goBackPopUp.SetActive(true);
                }
                else
                {
                    goBackPopUpOpened = false;
                    goBackPopUp.SetActive(false);
                }
            }
        }
    }
    public void SaveUnchanged()
    {
        AssetDatabase.Refresh();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Inventory_asset", LoadSceneMode.Single);
    }
    public void QuitExitting()
    {
        goBackPopUpOpened = false;
        goBackPopUp.SetActive(false);
    }

    public void SaveClicked()
    {
        somethingChanged = false;
    }
}
