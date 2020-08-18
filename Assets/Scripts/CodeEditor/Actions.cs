using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
public class Action_CodeModifier
{
    public int actionID;                public string actionName;
    public string actionDescription;    public int actionParent;
    public int actionUsed;              public float actionPositionX;
    public float actionPositionY;
    public Action_CodeModifier(int actionID, string actionName, string actionDescription, int actionParent, int actionUsed, float actionPositionX, float actionPositionY)
    {
        this.actionID = actionID;
        this.actionName = actionName;
        this.actionDescription = actionDescription;
        this.actionParent = actionParent;
        this.actionUsed = actionUsed;
        this.actionPositionX = actionPositionX;
        this.actionPositionY = actionPositionY;
        //actionParent  1 = Movement,    actionParent 2 = Attack,    actionParent 3 = Item
        //actionUsed    1 = YES,    2 = NO 
    }    
}
public class Actions : MonoBehaviour
{
    public Text tx; public string passedID;
    public GameObject actionPrefab;     public GameObject parentPrefab;
    public List<GameObject> movementOnList = new List<GameObject>(); public int movementOnListNum = 0;
    public List<GameObject> attackOnList = new List<GameObject>();  public int attackOnListNum = 0;
    public List<GameObject> itemOnList = new List<GameObject>(); public int itemOnListNum = 0;
    public List<GameObject> movementOffList = new List<GameObject>(); public int movementOffListNum = 0;
    public List<GameObject> attackOffList = new List<GameObject>(); public int attackOffListNum = 0;
    public List<GameObject> itemOffList = new List<GameObject>(); public int itemOffListNum = 0;
    public List<Action_CodeModifier>actionList = new List<Action_CodeModifier>();
    public int AccountID = 0; //계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    public void _save()
    {
        string jdata = JsonConvert.SerializeObject(actionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ActionData.json",format);

        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면? temp에서 넘어온 아이다.
            File.WriteAllText(Application.dataPath + "/Resources/" + theTempID.theID + passedID + "/ActionData.json",jdata);
        }
        else
        {//만약 이게 없으면?
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ AccountID.ToString() + "/" + passedID +"/ActionData.json",jdata);
        }
        Debug.Log("Action is saved");
        AssetDatabase.Refresh();
    }
    void Awake()
    {
        passedID = GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().name;
        TextAsset JData;

        //만약 codeeditor_IDpasser에서 온 경우일 경우 이걸 해야된당...
        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면?
            JData = Resources.Load(theTempID.theID + passedID + "/ActionData") as TextAsset;
        }
        else
        {//만약 이게 없으면?
            JData = Resources.Load("Json_AccountInfo/"+ AccountID.ToString() + "/" + passedID + "/ActionData") as TextAsset; //끝
        }
        string jdata = JData.text;

        //string jdata = File.ReadAllText(Application.dataPath + "/Resources/CodeEditor/ActionData.json");
        //byte[] bytes = System.Convert.FromBase64String(jdata);
        //string reformat = System.Text.Encoding.UTF8.GetString(bytes); // this is the actual string
        //actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(reformat);
        actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(jdata);
        loadOnScene();
    }
    private void loadOnScene()
    {
        int totalActions = actionList.Count;
        for(int tempActionID = 0; tempActionID < totalActions; tempActionID++)
        {
            if(actionList[tempActionID].actionParent == 1) //Movement
            {
                if(actionList[tempActionID].actionUsed == 1) //If it's already attached
                    createAction(tempActionID, ref movementOnList, ref movementOnListNum, 1);
                else if(actionList[tempActionID].actionUsed == 2) //If it's not been attached
                    createAction(tempActionID, ref movementOffList,ref movementOffListNum, 2);
            }
            else if(actionList[tempActionID].actionParent == 2) //Attack
            {
                if(actionList[tempActionID].actionUsed == 1) //If it's already attached
                    createAction(tempActionID, ref attackOnList, ref attackOnListNum, 1);
                else if(actionList[tempActionID].actionUsed == 2) //If it's not been attached
                    createAction(tempActionID, ref attackOffList,ref attackOffListNum, 2);
            }
            else if(actionList[tempActionID].actionParent == 3) //Item
            {
                if(actionList[tempActionID].actionUsed == 1) //If it's already attached
                    createAction(tempActionID, ref itemOnList, ref itemOnListNum, 1);
                else if(actionList[tempActionID].actionUsed == 2) //If it's not been attached
                    createAction(tempActionID, ref itemOffList,ref itemOffListNum, 2);
            }
        }
    }
    void Start()
    {
        //GameObject.Find("UIManager").GetComponent<UIManager>().initializeAction();
    }
    public void createAction(int tempActionID, ref List<GameObject> selectedList, ref int selectedListNum, int used)
    {
        GameObject _action = (GameObject)Instantiate(actionPrefab, new Vector2(0,0), Quaternion.identity);
        _action.transform.SetParent(parentPrefab.transform); //put it inside the content prefab
        _action.name = actionList[tempActionID].actionID.ToString();
        _action.transform.localScale = new Vector2(1,1); // change scale to fit size
        //_action.transform.GetChild(0).GetComponentInChildren<Text>().text = actionList[tempActionID].actionName;
        _action.transform.GetChild(0).GetComponentInChildren<Text>().text = actionList[tempActionID].actionDescription;
        _action.transform.GetChild(0).GetChild(3).name = actionList[tempActionID].actionName;
        _action.transform.GetChild(0).GetChild(3).GetChild(1).name = actionList[tempActionID].actionID.ToString();
        _action.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+"Action"+actionList[tempActionID].actionID.ToString());
        selectedList.Add(_action);
        selectedList[selectedListNum].SetActive(false);
        selectedListNum++;
    }
}
