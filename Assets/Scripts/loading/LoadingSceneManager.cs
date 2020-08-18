using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;

public class LoadingSceneManager : MonoBehaviour
{
    UserInformation data;
    void Start()
    {
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        Debug.Log("Userinfo = " + data.tutorialStage);
    }
    public void goHomeScene()
    {
        //resetting the code editor
        tutoCodeResetter();


        /*
        if (data.tutorialStage == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayScene");

        }*/
     
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }

    public List<Condition_CodeModifier>conditionList = new List<Condition_CodeModifier>();
    public List<Action_CodeModifier>actionList = new List<Action_CodeModifier>();
    public void tutoCodeResetter()
    {
        conditionList = new List<Condition_CodeModifier>();
        conditionList.Add(new Condition_CodeModifier(101, 1001, "HPMT",             "내 체력 25% 이상",             0, 0f, 0f, 888.8f, 25f,"Condition1","Up", -1));

        string jdata = JsonConvert.SerializeObject(conditionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/CodeEditor/" + "Alice" + "/ConditionData.json",jdata); //ID 추가해주기

        actionList = new List<Action_CodeModifier>();
        actionList.Add(new Action_CodeModifier(1,   "useWeapon",        "무기 사용",            2, 2, 0f, 0f));

        jdata = JsonConvert.SerializeObject(actionList);
        //bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ActionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/CodeEditor/" + "Alice" + "/ActionData.json",jdata); //ID 추가해주기
        AssetDatabase.Refresh();

    }

}
