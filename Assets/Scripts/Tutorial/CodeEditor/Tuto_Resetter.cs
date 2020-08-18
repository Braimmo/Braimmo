using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;


public class Tuto_Resetter : MonoBehaviour
{
    string theName;
    public List<Condition_CodeModifier> conditionList;
    public List<Action_CodeModifier> actionList;

    void Awake()
    {
        TextAsset JData = Resources.Load("CodeEditor/" + "Alice" + "/ConditionData") as TextAsset;
        string jdata = JData.text;
        conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(jdata);

        JData = Resources.Load("CodeEditor/" + "Alice" + "/ActionData") as TextAsset;
        jdata = JData.text;
        actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(jdata);
    }
    public void Startz()
    {
        theName = "Alice";
        print(HomeSceneMenuControl.data.tutorialStage + "fiojewapfjewofjoewaifjoewaifjoewaifjoewaifjio");
        if (HomeSceneMenuControl.data.tutorialStage == 6)
        { 
            conditionList = new List<Condition_CodeModifier>();
            conditionList.Add(new Condition_CodeModifier(101, 1001, "HPMT", "내 체력 25% 이상ㅋ", 0, 0f, 0f, 888.8f, 25f, "Condition1", "Up", -1));
        }
        if (HomeSceneMenuControl.data.tutorialStage == 15)
        {
            conditionList.Add(new Condition_CodeModifier(302, 1002, "EnemyLT", "공격 반경 안 적이 0명 이하", 0, 0f, 0f, 888.8f, 0f, "Condition6", "Down", -1));
        }
          if (HomeSceneMenuControl.data.tutorialStage >15)
        {
          conditionList.Add(new Condition_CodeModifier(101, 1003, "HPMT", "내 체력 20% 이상", 0, 0f, 0f, 888.8f, 20f, "Condition1", "Up", -1));
          conditionList.Add(new Condition_CodeModifier(201, 1004, "DPSMT", "한번에 받는 데미지 5% 이상", 0, 0f, 0f, 888.8f, 5f, "Condition2", "Up", -1));
          conditionList.Add(new Condition_CodeModifier(403, 1005, "OAmoveForward", "타겟에게 전진 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On", -1));

        }
        /*  conditionList.Add(new Condition_CodeModifier(101, 1001, "HPMT", "내 체력 50% 이상", 0, 0f, 0f, 888.8f, 50f, "Condition1", "Up"));
          conditionList.Add(new Condition_CodeModifier(101, 1002, "HPMT", "내 체력 70% 이상", 0, 0f, 0f, 888.8f, 70f, "Condition1", "Up"));
          conditionList.Add(new Condition_CodeModifier(102, 1004, "HPLT", "내 체력 70% 이하", 0, 0f, 0f, 888.8f, 70f, "Condition1", "Down"));
          conditionList.Add(new Condition_CodeModifier(102, 1005, "HPLT", "내 체력 50% 이하", 0, 0f, 0f, 888.8f, 50f, "Condition1", "Down"));
          conditionList.Add(new Condition_CodeModifier(201, 1006, "DPSMT", "한번에 받는 데미지 5% 이상", 0, 0f, 0f, 888.8f, 5f, "Condition2", "Up"));
          conditionList.Add(new Condition_CodeModifier(202, 1007, "DPSLT", "한번에 받는 데미지 5% 이하", 0, 0f, 0f, 888.8f, 5f, "Condition2", "Down"));
          conditionList.Add(new Condition_CodeModifier(203, 1008, "isStunned", "내 상태 기절", 0, 0f, 0f, 888.8f, 0f, "Condition3", "On"));
          conditionList.Add(new Condition_CodeModifier(204, 1009, "BuffedAtk", "공격력 버프 유지 중", 0, 0f, 0f, 888.8f, 0f, "Condition5", "On"));
          conditionList.Add(new Condition_CodeModifier(205, 1010, "DebuffedDef", "방어력 디버프 유지 중", 0, 0f, 0f, 888.8f, 0f, "Condition4", "On"));
          conditionList.Add(new Condition_CodeModifier(206, 1011, "BuffedDef", "방어력 버프 유지 중", 0, 0f, 0f, 888.8f, 0f, "Condition4", "On"));
          conditionList.Add(new Condition_CodeModifier(207, 1012, "DebuffedAtk", "공격력 디버프 유지 중", 0, 0f, 0f, 888.8f, 0f, "Condition5", "On"));
          conditionList.Add(new Condition_CodeModifier(101, 1013, "HPMT", "내 체력 45% 이상", 0, 0f, 0f, 888.8f, 45f, "Condition1", "Up"));
          conditionList.Add(new Condition_CodeModifier(102, 1014, "HPLT", "내 체력 85% 이하", 0, 0f, 0f, 888.8f, 85f, "Condition1", "Down"));
          conditionList.Add(new Condition_CodeModifier(101, 1015, "HPMT", "내 체력 20% 이상", 0, 0f, 0f, 888.8f, 20f, "Condition1", "Up"));
          conditionList.Add(new Condition_CodeModifier(102, 1016, "HPLT", "내 체력 60% 이하", 0, 0f, 0f, 888.8f, 60f, "Condition1", "Down"));
          conditionList.Add(new Condition_CodeModifier(301, 1017, "EnemyMT", "공격 반경 안 적이 3명 이상", 0, 0f, 0f, 888.8f, 3f, "Condition6", "Up"));
          conditionList.Add(new Condition_CodeModifier(302, 1018, "EnemyLT", "공격 반경 안 적이 3명 이하", 0, 0f, 0f, 888.8f, 3f, "Condition6", "Down"));
          conditionList.Add(new Condition_CodeModifier(301, 1020, "EnemyMT", "공격 반경 안 적이 1명 이상", 0, 0f, 0f, 888.8f, 1f, "Condition6", "Up"));
          conditionList.Add(new Condition_CodeModifier(401, 1021, "OAuseWeapon", "무기 사용 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(402, 1022, "OAuseHealthPotion", "체력 포션 사용 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(403, 1023, "OAmoveForward", "타겟에게 전진 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(404, 1024, "OAmoveBackward", "타겟에게서 후진 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(405, 1025, "OAmoveLeft", "타겟의 좌측으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(406, 1026, "OAmoveRight", "타겟의 우측으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(407, 1027, "OAmoveNorth", "맵의 위쪽으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(408, 1028, "OAmoveSouth", "맵의 아래쪽으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(409, 1029, "OAmoveWest", "맵의 왼쪽으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(410, 1030, "OAmoveEast", "맵의 오른쪽으로 이동 중", 0, 0f, 0f, 888.8f, 0f, "Condition1", "On"));
          conditionList.Add(new Condition_CodeModifier(101, 1031, "HPMT", "내 체력 10% 이상", 0, 0f, 0f, 888.8f, 10f, "Condition1", "Up"));
          conditionList.Add(new Condition_CodeModifier(102, 1032, "HPLT", "내 체력 40% 이하", 0, 0f, 0f, 888.8f, 40f, "Condition1", "Down"));
          conditionList.Add(new Condition_CodeModifier(102, 1034, "HPLT", "내 체력 30% 이하", 0, 0f, 0f, 888.8f, 30f, "Condition1", "Down"));
          */

        string jdata = JsonConvert.SerializeObject(conditionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/CodeEditor/" + theName + "/ConditionData.json", jdata); //ID 추가해주기

        if (HomeSceneMenuControl.data.tutorialStage == 6)
        {
            actionList = new List<Action_CodeModifier>();
            actionList.Add(new Action_CodeModifier(1, "useWeapon", "무기 사용", 2, 2, 0f, 0f));
        }
        if (HomeSceneMenuControl.data.tutorialStage == 15)
        {
            actionList.Add(new Action_CodeModifier(2,   "useHealthPotion",  "체력 포션 사용",       3, 2, 0f, 0f));
            actionList.Add(new Action_CodeModifier(3,   "moveForward",      "타겟에게 전진",        1, 2, 0f, 0f));
        }
        /*   actionList.Add(new Action_CodeModifier(2, "useHealthPotion", "체력 포션 사용", 3, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(4, "moveBackward", "타겟에게서 후진", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(5, "moveLeft", "타겟의 좌측으로 이동", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(6, "moveRight", "타겟의 우측으로 이동", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(7, "moveNorth", "맵의 위쪽으로 이동", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(8, "moveSouth", "맵의 아래쪽으로 이동", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(9, "moveWest", "맵의 왼쪽으로 이동", 1, 2, 0f, 0f));
           actionList.Add(new Action_CodeModifier(10, "moveEast", "맵의 오른쪽으로 이동", 1, 2, 0f, 0f));
           */
        jdata = JsonConvert.SerializeObject(actionList);
        //bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ActionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/CodeEditor/" + theName + "/ActionData.json", jdata); //ID 추가해주기
        AssetDatabase.Refresh();

    }
}
