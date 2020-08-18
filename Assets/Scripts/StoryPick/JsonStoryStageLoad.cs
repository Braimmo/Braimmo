using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;



public class JsonStoryStageLoad : MonoBehaviour
{
    public List<StageInformation> stageInfo;
 
    public int lastStage;
    //StoryModeManager storyModeManager;
    UserInformation userInformation;
    [SerializeField] StoryModeManager storyModeManager;
    public int AccountID;


    int ageID = 0;

    public void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        if (GameObject.Find("PassStageInfoBetweenScenes_dontDestroy") != null)
        {
             ageID = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<_StageInformation>().ageID;
        }
        else{
            Debug.Log("NO dontdestroy");
        }

        stageInfo = new List<StageInformation>();
        loadStageInfo();
        loadCurrentStageLeve();

        storyModeManager.lastStage = lastStage;
    }

    void loadCurrentStageLeve(){
        // string jsonData = File.ReadAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json");
        // userInformation = JsonConvert.DeserializeObject<UserInformation>(jsonData);
        // stageLevel = userInformation.stageID;
        //UserInformation에서 받아와야함~~
        lastStage = 3;
    }


    void loadStageInfo(){
        TextAsset textData = Resources.Load("Json_GameInfo/Age" + ageID.ToString() + "/StageInfo") as TextAsset;
        string ageJsonData = textData.text;
        stageInfo = JsonConvert.DeserializeObject<List<StageInformation>>(ageJsonData);
    }
}
