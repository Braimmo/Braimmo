using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AgePick;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;

public class passDataBetweenScene : MonoBehaviour
{
    //age -> story -> char -> inGame으로 넘기는 data 저장
    public static passDataBetweenScene passData;
    public _StageInformation stageInfo;
    public int AccountID = 0; //계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    void Awake()
    {
        if (passData == null)
            passData = this;
        else if (passData != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        //stageInfo = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<StageInformation>();
        stageInfo = this.gameObject.GetComponent<_StageInformation>();
    }

    //Age씬에서 픽한 정보 _StageInformation에 저장
    public void setAgeInfo(JsonAgeStageFormat age)
    {
        stageInfo.ageID = age.AgeID;
        stageInfo.ageName = age.AgeName;
        stageInfo.ageInfo = age.AgeInfo;
        stageInfo.emblemID = age.EmblemSprite;
        stageInfo.emblemInfo = age.EmblemInfo;
        stageInfo.emblemName = age.EmblemName;
        Debug.Log("stageInfo.emblemName: " + stageInfo.emblemName);
    }


    //Story씬에서 픽한 정보 _StageInformation에 저장
    public void setStoryInfo(StageInformation pickedStage)
    {
        print("??");
        int stageId = pickedStage.stageLevel - 1;
        // StoryMode에서 선택한 스테이지 누적해서 Json으로 저장하기
        stageInfo.stageID = stageId;
        stageInfo.stageLevel = pickedStage.stageLevel;
        stageInfo.stageInfo = pickedStage.stageInfo;
        stageInfo.money = pickedStage.money;
        stageInfo.gem = pickedStage.gem;
        stageInfo.experience = 1000;  //Json에서 받아오게 해야된다. 
        stageInfo.awards = new List<JsonStageAwardFormat>(pickedStage.awards);
        // 선택한 stage에 따라 enemy도 저장

        stageInfo.enemyNumber = pickedStage.enemyNumber;
        stageInfo.enemyIds = new List<int>(pickedStage.enemyIds);
        // stageInfo.enemyIds = new List<int>();
        // stageInfo.enemyIds.Add(0);
        // stageInfo.enemyIds.Add(1);
        // stageInfo.enemyIds.Add(2);

        stageInfo.charNumber = pickedStage.charNumber;
        stageInfo.enemyStatMultiplier = 1.0f;        //stage level에 따라 변동되는 constant
        print("짱느리네");
    }

    //Char씬에서 픽한 정보 _StageInformation에 저장
    public void setCharacterInfo(int[] selectedCharacterIDs)
    {
        // StoryPick에서 저장한 정보 불러오기
        stageInfo.selectedCharacterIDs = new List<int>();
        for (int i = 0; i < stageInfo.charNumber; i++)
        {
            stageInfo.selectedCharacterIDs.Add(selectedCharacterIDs[i]);
        }
    }

    //age, story, char씬에서 픽한 내용 인게임에 적용시키도록 Resources>GamePlay>gameStageInfo json으로 저장
    public void savePickedStageInfoAsJson()
    {
        StageInformation stageInfoJson = new StageInformation();
        stageInfoJson.ageID = stageInfo.ageID;
        stageInfoJson.experience = stageInfo.experience;
        stageInfoJson.money = stageInfo.money;
        stageInfoJson.gem = stageInfo.gem;
        stageInfoJson.ageName = stageInfo.ageName;
        stageInfoJson.ageInfo = stageInfo.ageInfo;
        stageInfoJson.emblemID = stageInfo.emblemID;
        stageInfoJson.emblemInfo = stageInfo.emblemInfo;
        stageInfoJson.emblemName = stageInfo.emblemName;
        stageInfoJson.stageID = stageInfo.stageID;
        stageInfoJson.stageLevel = stageInfo.stageLevel;
        stageInfoJson.stageInfo = stageInfo.stageInfo;
        stageInfoJson.awards = new List<JsonStageAwardFormat>(stageInfo.awards);
        stageInfoJson.enemyNumber = stageInfo.enemyNumber;
        stageInfoJson.enemyIds = stageInfo.enemyIds;
        stageInfoJson.charNumber = stageInfo.charNumber;
        stageInfoJson.enemyStatMultiplier = stageInfo.enemyStatMultiplier;
        stageInfoJson.selectedCharacterIDs = new List<int>(stageInfo.selectedCharacterIDs);
        
        string jsonData = JsonConvert.SerializeObject(stageInfoJson, Formatting.Indented);
        //jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/gameStageInfo.json", jsonData);
        AssetDatabase.Refresh();
    }

}

