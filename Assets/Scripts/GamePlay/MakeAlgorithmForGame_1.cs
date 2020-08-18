using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MakeAlgorithmForGame_1 : MonoBehaviour
{
    public List<List<Condition_Specific>> conditionList;
    public CheckConditionData checkConditionData;
    public LoadAIFromJson loadAIFromJson;
    public List<Action_> data1;
    public int[] cases;

    void Awake()
    {
        cases = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        checkConditionData = this.transform.GetComponent<CheckConditionData>();
        loadAIFromJson = this.transform.GetComponent<LoadAIFromJson>();
    }

    void Start()
    {
        string theName = this.transform.GetChild(1).GetComponent<EachCharacterStat>().name;
        Debug.Log("MakeAlgorithm is implementing for " + theName);
        conditionList = new List<List<Condition_Specific>>();
        data1 = loadAIFromJson.LoadAIFromJson_(theName);
        
        for (int a = 0; a < 12; a++)
        {   //일단 새로운 리스트를 만들어주자.
            conditionList.Add(new List<Condition_Specific>());
        }
        for (int i = 0; i <data1.Count; i++)
        {
            if(data1[i].ConditionCombine.Count != 0) // ==0 은 거의 일어나지 않을 일. 왜냐하면 액션이라고 딸려 온 애는 거의 무조건 조건이 붙기 때문에.
            {
                for(int j = 0; j < data1[i].ConditionCombine.Count; j++) //이건 내생각에 and의 경우인 것 같은데 잘 모르겠음 아직까진. 2020-07-13
                {
                    print("actionID is " + data1[i].actionID + " conditioncombined: " + data1[i].ConditionCombine[j].conditionName);
                    conditionList[data1[i].actionID].Add(new Condition_Specific(data1[i].ConditionCombine[j].conditionName, data1[i].ConditionCombine[j].conditionValue, cases[data1[i].actionID - 1],data1[i].ConditionCombine[j].conditionPrefab,data1[i].ConditionCombine[j].conditionValueState));
                    //conditionList[data1[i].actionID].Add 라는 말은, conditionList는 사실 conditionList 가 아니고 actionList라는 것이다... ㅆㅃ
                    //각 액션 마다 conditionList가 존재해야한다는 것
                    //print("successfully finished data1[" + i + "]." + j);
                }
                cases[data1[i].actionID - 1] += 1;
            }
        }
    }
    public bool CheckCondition_(int action)
    {
        return checkConditionData.CheckCondition_Specific(action, conditionList);
    }

}
