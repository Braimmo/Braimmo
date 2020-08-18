using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;

public class stageExplainPanel : MonoBehaviour
{
    [SerializeField] Text stageName;

    private int _level;

    //StoryModeManager storyModeManager; 
    public List<StageInformation> stageInfo;
    [SerializeField] GameObject stageExplainPanelPrefab;
    [SerializeField] GameObject StoryUIManager;
    StoryModeManager storyModeManager;
    AsyncOperation operation;

    public int AccountID;

    public Award[] awards;

    public void Awake()
    {   
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        storyModeManager = StoryUIManager.GetComponent<StoryModeManager>();
        GameObject codeAward = transform.Find("code award").gameObject;
        awards = codeAward.GetComponentsInChildren<Award>();
        
    }

    public void initializeStageInfo()
    {
        // if(awardPanelPrefab != null)
        //     awards = awardPanelPrefab.GetComponentsInChildren<Award>();
    }

    public void setStageInfo(int level)
    {
        _level = level;
        stageName.text = "STAGE " + level.ToString();
        print("stageInfo.awards: " + stageInfo[level - 1].awards.Count);
        int i = 0;
        for (i = 0; i < awards.Length && i < stageInfo[level - 1].awards.Count; i++)
        {
            awards[i].awardImage.sprite = Resources.Load<Sprite>("StoryModeDB/stage" + level + "/" + stageInfo[level - 1].awards[i].AwardImage);
            awards[i].awardName.text = stageInfo[level - 1].awards[i].AwardName;
            // award.awardName.text = stageInfo[level-1].awards[i].AwardName;
            // award.awardInfo = stageInfo[level-1].awards[i].AwardInfo;
        }
        for (; i < awards.Length; i++)
        {
            awards[i].awardImage.color = new Color32(0, 0, 0, 1);
            awards[i].awardName.text = null;
        }
    }

    public void clickGameStart()
    {
        // UserInformation userInfo = new UserInformation();
        // string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json");
        // userInfo = JsonConvert.DeserializeObject<UserInformation>(jsonData);
        // userInfo.stageID += 1;
        // print("stage id: " + userInfo.stageID);
        // jsonData = JsonConvert.SerializeObject(userInfo);
        // jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        // File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/userInformation.json", jsonData);
        // AssetDatabase.Refresh();
        print("game start");
        // StartCoroutine(LoadAsynchronously("CharacterPick"));
        storyModeManager.gameStart(_level, stageInfo[_level - 1]);
    }

    public void allow(){
        operation.allowSceneActivation = true;
    }
     IEnumerator LoadAsynchronously(string sceneName){
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while(!operation.isDone){
            Debug.Log(operation.progress);
            yield return null;
        }
    }


    public void clickRightButton()
    {
        if (_level < storyModeManager.lastStage)
        {
            setStageInfo(_level + 1);
        }
    }

    public void clickLeftButton()
    {
        if (_level > 1)
        {
            setStageInfo(_level - 1);
        }
    }
}
