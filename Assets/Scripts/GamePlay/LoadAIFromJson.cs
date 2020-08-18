using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class LoadAIFromJson : MonoBehaviour
{
    public int AccountID = 0; //계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    public List<Action_> LoadAIFromJson_(string theName)
    {
        //적이면 Resources/Json_GameInfo에서 로드해야되고 아군이면 Resources/Json_AccountInfo에서 로드해야된다!
        string tempLocation = "";
        if(this.transform.tag == "Enemy")
        {
            tempLocation = "Json_GameInfo/";
        }
        else if(this.transform.tag == "Player")
        {
            tempLocation = "Json_AccountInfo/" + AccountID.ToString() + "/";
        }
        print(tempLocation + theName + " is being loaded from LoadAIFromJson_");
        //TextAsset JData = Resources.Load("CodeEditor/" + theName + "/AlgorithmForGame") as TextAsset;
        TextAsset JData = Resources.Load(tempLocation + theName + "/AlgorithmForGame") as TextAsset;
        string jsonData = JData.text;
        print(jsonData);
  
        List<Action_> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Action_>>(jsonData);
        return data;
    }

}
public class Condition_Specific
{
    public int branchNum;
    public string conditionName;
    public float conditionValue;
    public string conditionPrefab;
    public string conditionValueState;

    public Condition_Specific(string conditionName, float conditionValue, int branchNum,string conditionPrefab, string conditionValueState)
    {
        this.conditionName = conditionName;
        this.conditionValue = conditionValue;
        this.branchNum = branchNum;
        this.conditionPrefab = conditionPrefab;
        this.conditionValueState = conditionValueState;
    }
}
[System.Serializable]

public class Condition_General
{
    public string conditionName;
    public float conditionValue;
    public string conditionPrefab;
    public string conditionValueState;

    public Condition_General(string conditionName, float conditionValue, string conditionPrefab, string conditionValueState)
    {
        this.conditionName = conditionName;
        this.conditionValue = conditionValue;
        this.conditionPrefab = conditionPrefab;
        this.conditionValueState = conditionValueState;
    }
}
[System.Serializable]
public class Action_General
{
    public int actionID { get; set; }
    public List<Condition_> ConditionCombine { get; set; }
}

[System.Serializable]
public class Condition_
{
    public string conditionName;
    public float conditionValue;
    public string conditionPrefab;
    public string conditionValueState;

    public Condition_(string conditionName, float conditionValue, string conditionPrefab, string conditionValueState)
    {
        this.conditionName = conditionName;
        this.conditionValue = conditionValue;
        this.conditionPrefab = conditionPrefab;
        this.conditionValueState = conditionValueState;
    }
}
[System.Serializable]
public class Action_
{
    public int actionID { get; set; }
    public List<Condition_> ConditionCombine { get; set; }
}

