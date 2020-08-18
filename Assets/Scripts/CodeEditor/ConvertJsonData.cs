using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
public class ConvertJsonData : MonoBehaviour
{
    public Actions actionManager;   public Conditions conditionManager;
    public List<Condition_CodeModifier> conditionParentCondition = new List<Condition_CodeModifier>();
    public List<Condition_CodeModifier> conditionParentAction = new List<Condition_CodeModifier>();
    public List<Condition_CodeModifier> conditionUsed = new List<Condition_CodeModifier>();
    public List<Action_CodeModifier> actionUsed = new List<Action_CodeModifier>();
    public List<Condition_forGame> conditionList_forGame = new List<Condition_forGame>();
    public List<List<Condition_forGame>> conditionList_Total = new List<List<Condition_forGame>>();
    public List<Finalizer> finalizeString = new List<Finalizer>();
    public int AccountID = 0;//계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ

    void Awake()
    {
        actionManager = gameObject.GetComponent<Actions>();
        conditionManager = gameObject.GetComponent<Conditions>();
    }
    public void SortUsed()
    {
        //initialize
        conditionParentCondition = new List<Condition_CodeModifier>();
        conditionParentAction = new List<Condition_CodeModifier>();
        conditionUsed = new List<Condition_CodeModifier>();
        actionUsed = new List<Action_CodeModifier>();
        conditionList_forGame = new List<Condition_forGame>();
        conditionList_Total = new List<List<Condition_forGame>>();
        finalizeString = new List<Finalizer>();

        for(int i = 0; i < conditionManager.conditionList.Count; i ++) // 총 conditionlist를 다 돌면서 체크
        {
            if(conditionManager.conditionList[i].conditionParent != 0) // 만약 parent가 달려 있을 경우
            {
                conditionUsed.Add(conditionManager.conditionList[i]); //달려있는 애 이름인 conditionUsed에 저장
            }
        }
        for(int i = 0; i < actionManager.actionList.Count; i ++) //condition도 했으니까 action도 해주자
        {
            if(actionManager.actionList[i].actionUsed != 0)
            {
                actionUsed.Add(actionManager.actionList[i]);
            }
        }
        SortConditionParent();
    }
    public class Condition_forGame
    {
        public string conditionName;   
        public float conditionValue;
        public string conditionPrefab;
        public string conditionValueState;
        public Condition_forGame(string conditionName, float conditionValue, string conditionPrefab, string conditionValueState)
        {
            this.conditionName = conditionName;
            this.conditionValue = conditionValue;
            this.conditionPrefab = conditionPrefab;
            this.conditionValueState = conditionValueState;
        }
    }
    public class Finalizer
    {
        public int actionID {get;set;}
        public List<Condition_forGame> ConditionCombine {get;set;}
    }

    public void SortConditionParent()
    {
        for(int i = 0; i < conditionUsed.Count; i ++) //conditionUsed도 한번에 쫙 돌리면서
        {
            if(conditionUsed[i].conditionParent > 1000) //만약 condition의 parent가 condition인 경우
            {
                conditionParentCondition.Add(conditionUsed[i]); //conditionParentCondition에다가 저장
            }
            else
            {
                conditionParentAction.Add(conditionUsed[i]); //아닐 경우, 즉 condition의 parent가 action인 경우 conditionParentAction에 저장
            }
        }
        //print("conditionparentACTION.count = " + conditionParentAction.Count);
        //print("conditionparentCONDITION.count = " + conditionParentCondition.Count);
        //여기까진 문제 없음

        for(int i = 0; i < conditionParentAction.Count; i++) //condiitonParentAction이 가장 먼저 시작하는 놈이니까, 얘만 보면서 시작해보자.
        {
            //print("New Branch Starts Here");
            conditionList_forGame = new List<Condition_forGame>();
            conditionList_Total = new List<List<Condition_forGame>>();
            conditionList_forGame.Add(new Condition_forGame(conditionParentAction[i].conditionName,conditionParentAction[i].conditionValue, conditionParentAction[i].conditionPrefab,conditionParentAction[i].conditionValueState));
            int checkEqualFirst = conditionParentAction[i].conditionUniqueID; 
            int tempCount = conditionList_forGame.Count; //1
            do
            {
                tempCount = conditionList_forGame.Count; //1
                for(int j = 0; j < conditionParentCondition.Count; j++)
                {
                    if(checkEqualFirst == conditionParentCondition[j].conditionParent)
                    {
                        print("conditionParentAction[i+addedTo].conditionUniqueID = " + checkEqualFirst);
                        print("conditionParentCondition[j].conditionParent = " + conditionParentCondition[j].conditionParent);
                        conditionList_forGame.Add(new Condition_forGame(conditionParentCondition[j].conditionName,conditionParentCondition[j].conditionValue, conditionParentAction[i].conditionPrefab,conditionParentAction[i].conditionValueState));
                        checkEqualFirst = conditionParentCondition[j].conditionUniqueID;
                        print("checkequalfirst = " + checkEqualFirst);
                        conditionParentCondition.RemoveAt(j);
                    }
                }
                //print("conditionList_forGame Count is " + conditionList_forGame.Count);
            } while (tempCount != conditionList_forGame.Count);
            //conditionList_Total.Add(conditionList_forGame);
            Finalizer finalizer = new Finalizer
            {
                actionID = conditionParentAction[i].conditionParent,
                ConditionCombine = conditionList_forGame
            };
            finalizeString.Add(finalizer);
        }
        print("conditionList_Total Count is " + conditionList_Total.Count);
        WriteJson();
    }
    string jdata;
    public void WriteJson()
    {
        string tempID = this.transform.GetComponent<Actions>().passedID;
        string jdata = JsonConvert.SerializeObject(finalizeString);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);

        tempID theTempID = GameObject.Find("charID_DontDestroy").GetComponent<tempID>();
        if(theTempID != null)
        {//만약 이게 있으면? temp에서 넘어온 아이다.
            File.WriteAllText(Application.dataPath + "/Resources/" + theTempID.theID + tempID + "/AlgorithmForGame.json",jdata);
        }
        else
        {//만약 이게 없으면?
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ AccountID.ToString() + "/" + tempID + "/AlgorithmForGame.json",jdata);
        }
        print("Converted Json Created!!");
    }



}
