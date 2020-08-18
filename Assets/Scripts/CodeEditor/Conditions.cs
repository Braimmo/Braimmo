using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using TMPro;
public class Condition_CodeModifier
{
    public int conditionID;             public int conditionUniqueID;
    public string conditionName;        public string conditionDescription;
    public int conditionParent;
    public float conditionPositionX;    public float conditionPositionY;
    public float goldValue;             public float conditionValue;
    public string conditionPrefab;      public string conditionValueState;
    public int usingCharID;
    public Condition_CodeModifier(int conditionID, int conditionUniqueID, string conditionName, string conditionDescription, int conditionParent, float conditionPositionX, float conditionPositionY, float goldValue, float conditionValue, string conditionPrefab, string conditionValueState, int usingCharID)
    {
        this.conditionID = conditionID;
        this.conditionUniqueID = conditionUniqueID;
        this.conditionName = conditionName;
        this.conditionDescription = conditionDescription;
        this.conditionParent = conditionParent;
        this.conditionPositionX = conditionPositionX;
        this.conditionPositionY = conditionPositionY;
        this.goldValue = goldValue;
        this.conditionValue = conditionValue;
        this.conditionPrefab = conditionPrefab;
        this.conditionValueState = conditionValueState;
        this.usingCharID = usingCharID;
        //conditionParent 1 = Movement,    conditionParent 2 = Attack,    conditionParent 3 = Item, 0 = OFF
    }    
    public Condition_CodeModifier()
    {
        
    }
}
public class Conditions : MonoBehaviour
{
    public Text tx; public int passedID; public string passedName;
    public GameObject conditionPrefab;      public GameObject parentPrefab;
    public List<Condition_CodeModifier>conditionListSorted = new List<Condition_CodeModifier>();
    public List<Condition_CodeModifier>conditionList = new List<Condition_CodeModifier>();
    public List<GameObject> hpOnList = new List<GameObject>();      public int hpOnListNum = 0;          // 체력 ON 관련
    public List<GameObject> stateOnList = new List<GameObject>();   public int stateOnListNum = 0;  // 상태 ON 관련
    public List<GameObject> radiusOnList = new List<GameObject>();  public int radiusOnListNum = 0;  // 반경 ON 관련
    public List<GameObject> actionCheckerOnList = new List<GameObject>();  public int actionCheckerOnListNum = 0;  // 반경 ON 관련
    public List<GameObject> hpOffList = new List<GameObject>();     public int hpOffListNum = 0;        // 체력 OFF 관련
    public List<GameObject> stateOffList = new List<GameObject>();  public int stateOffListNum = 0;  // 상태 OFF 관련
    public List<GameObject> radiusOffList = new List<GameObject>(); public int radiusOffListNum = 0; // 반경 OFF 관련
    public List<GameObject> actionCheckerOffList = new List<GameObject>(); public int actionCheckerOffListNum = 0; // 액션온 OFF 관련

    public int AccountID;
    public void _save()
    {
        string jdata = JsonConvert.SerializeObject(conditionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);

        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면? temp에서 넘어온 아이다.
            File.WriteAllText(Application.dataPath + "/Resources/" + theTempID.theID + "ConditionData.json",jdata);
        }
        else
        {//만약 이게 없으면?
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ AccountID.ToString() +"/ConditionData.json",jdata);
        }
        Debug.Log("Condition is saved");
        AssetDatabase.Refresh();
    }
    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        passedID = GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().characterID;
        passedName = GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().name;
        TextAsset JData;

        //만약 codeeditor_IDpasser에서 온 경우일 경우 이걸 해야된당...
        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면?
            JData = Resources.Load(theTempID.theID + "ConditionData") as TextAsset;
            //무슨 캐릭터인지 정의를 내려줘야한다!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        else
        {//만약 이게 없으면?
            JData = Resources.Load("Json_AccountInfo/"+ AccountID.ToString() + "/ConditionData") as TextAsset; //끝
        }
        string jdata = JData.text;
        //string jdata = File.ReadAllText(Application.dataPath + "/Resources/CodeEditor/ConditionData.json");
        //byte[] bytes = System.Convert.FromBase64String(jdata);
        //string reformat = System.Text.Encoding.UTF8.GetString(bytes); // this is the actual string
        //conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(reformat);
        conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(jdata);
        loadOnScene();
    }

    private void loadOnScene()
    {
        conditionListSorted = new List<Condition_CodeModifier>(conditionList);

        conditionListSorted.Sort(delegate(Condition_CodeModifier a, Condition_CodeModifier b) {return (a.conditionID*1000 + a.conditionValue).CompareTo(b.conditionID*1000 + b.conditionValue);});
        int totalConditionsSorted = conditionListSorted.Count;
        for(int tempConditionID = 0; tempConditionID < totalConditionsSorted; tempConditionID++)
        {
            int tempCategory = conditionListSorted[tempConditionID].conditionID/100; //1=체력,2=상태,3=반경
            if(conditionListSorted[tempConditionID].usingCharID == passedID || conditionListSorted[tempConditionID].usingCharID == -1) //아예 안쓴거거나 내가 쓴거라면. 2020-08-13 수정
            {
                if(tempCategory == 1) //체력
                {
                    if(conditionListSorted[tempConditionID].conditionParent == 0) //아직 안쓴거라면
                    {
                        createCondition(tempConditionID, ref hpOffList, ref hpOffListNum);
                    }
                    else //이미 달려있는거라면
                    {
                        createCondition(tempConditionID, ref hpOnList, ref hpOnListNum);
                    }
                }
                else if(tempCategory == 2) //상태
                {
                    if(conditionListSorted[tempConditionID].conditionParent == 0)
                    {
                        createCondition(tempConditionID, ref stateOffList, ref stateOffListNum);
                    }
                    else
                    {
                        createCondition(tempConditionID, ref stateOnList, ref stateOnListNum);
                    }
                }
                else if(tempCategory == 3) // 반경
                {
                    if(conditionListSorted[tempConditionID].conditionParent == 0)
                    {
                        createCondition(tempConditionID, ref radiusOffList, ref radiusOffListNum);
                    }
                    else
                    {
                        createCondition(tempConditionID, ref radiusOnList, ref radiusOnListNum);
                    }
                }
                else if(tempCategory == 4)
                {
                    if(conditionListSorted[tempConditionID].conditionParent == 0)
                    {
                        createCondition(tempConditionID, ref actionCheckerOffList, ref actionCheckerOffListNum);
                    }
                    else
                    {
                        createCondition(tempConditionID, ref actionCheckerOnList, ref actionCheckerOnListNum);
                    }
                }
            }
        }
    }
    public void createCondition(int tempConditionID, ref List<GameObject> selectedList, ref int selectedListNum)
    {
        GameObject _condition = (GameObject)Instantiate(conditionPrefab, new Vector2(0,0), Quaternion.identity);
        Condition_CodeModifier tempCondCode = conditionListSorted[tempConditionID]; 

        _condition.name = tempCondCode.conditionUniqueID.ToString()+tempCondCode.conditionName;
        _condition.transform.SetParent(parentPrefab.transform); //put it inside the content prefab
        _condition.transform.localScale = new Vector2(1,1); // change scale to fit size
        _condition.transform.GetChild(0).GetComponentInChildren<Text>().name = (tempCondCode.conditionID / 100).ToString();
        //_condition.transform.GetChild(0).GetComponentInChildren<Text>().text = conditionListSorted[tempConditionID].conditionName + " " + conditionListSorted[tempConditionID].conditionValue;
        _condition.transform.GetChild(0).GetComponentInChildren<Text>().text = tempCondCode.conditionDescription;
        _condition.transform.GetChild(0).GetChild(0).name = tempCondCode.conditionName;
        _condition.transform.GetChild(0).GetChild(1).name = tempCondCode.conditionUniqueID.ToString();
        _condition.transform.GetChild(0).GetChild(3).name = tempCondCode.conditionName + tempCondCode.conditionUniqueID.ToString();
        _condition.transform.GetChild(0).GetChild(3).GetChild(1).name = tempCondCode.conditionUniqueID.ToString();
        print(tempCondCode.conditionPrefab);
        _condition.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/"+tempCondCode.conditionPrefab);
        
        if(tempCondCode.conditionValue != 0)
        {   //숫자 집어넣기
            _condition.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = tempCondCode.conditionValue.ToString();
        }
        else
        {
            _condition.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
        
        if(tempCondCode.conditionValueState == "On")
        {
            _condition.transform.GetChild(0).GetChild(3).GetChild(3).transform.localPosition=new Vector2(_condition.transform.GetChild(0).GetChild(3).GetChild(3).transform.localPosition.x - 10, _condition.transform.GetChild(0).GetChild(3).GetChild(3).transform.localPosition.y); 
        }
        else
        {
            _condition.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<RawImage>().texture = Resources.Load<Texture>("CodeEditor/Arrow_"+tempCondCode.conditionValueState);
        }
        selectedList.Add(_condition);
        selectedList[selectedListNum].SetActive(false);
        selectedListNum++;
    }

}
