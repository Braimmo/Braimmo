using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace AgePick{
    public class saveStageInfo : MonoBehaviour
    {
        public StageInformation stageInfo;
        public int AccountID;

        void Awake()
        {
            AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        }
        //public static saveStageInfo instance;

        // void Awake(){
        //     if(instance == null)    
        //         instance = this;
        //     else if(instance != this){
        //         Destroy(gameObject);
        //     }
           
        // }

        public void setAgeInfo(JsonAgeStageFormat age){
            stageInfo = new StageInformation();
            stageInfo.ageID = age.AgeID;
            stageInfo.experience = 1000;  //Json에서 받아오게 해야된다. 
            stageInfo.money = 1000; //Json에서 받아오게 해야된다. 
            stageInfo.gem = 2; //Json에서 받아오게 해야된다. 
            stageInfo.ageName = age.AgeName;
            stageInfo.ageInfo = age.AgeInfo;
            stageInfo.emblemID = age.EmblemSprite;
            stageInfo.emblemInfo = age.EmblemInfo;
            stageInfo.emblemName = age.EmblemName;
            Debug.Log("setAgeInfo: "+stageInfo.ageName);

            string jsonData = JsonConvert.SerializeObject(stageInfo);
            jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
            // File.WriteAllText(Application.dataPath + "/Scripts/GamePlay/gameStageInfo.json", jsonData);
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/gameStageInfo.json", jsonData);
        }
        
        public void setStageInfo(StageInformation pickedStage){
            stageInfo = new StageInformation();
            // AgePick에서 저장한 정보 불러오기
            TextAsset textData = Resources.Load("Json_AccountInfo/" + AccountID + "/gameStageInfo") as TextAsset;
            string ageJsonData = textData.text;
            stageInfo = JsonConvert.DeserializeObject<StageInformation>(ageJsonData);
            
            int stageId = pickedStage.stageLevel - 1;
            // StoryMode에서 선택한 스테이지 누적해서 Json으로 저장하기
            stageInfo.stageID = stageId;
            stageInfo.stageLevel = pickedStage.stageLevel;
            stageInfo.stageInfo = pickedStage.stageInfo;
            stageInfo.awards = new List<JsonStageAwardFormat>(pickedStage.awards); 
            // 선택한 stage에 따라 enemy도 저장
            
            textData = Resources.Load("Json_GameInfo/Age " + stageId + "/StageInfo") as TextAsset;
            string allStageInfo = textData.text;
            List<StageInformation> stages = JsonConvert.DeserializeObject<List<StageInformation>>(allStageInfo);

            stageInfo.enemyNumber = stages[stageId].enemyNumber;
            stageInfo.enemyIds = new List<int>(stages[stageId].enemyIds);
            stageInfo.charNumber = stages[stageId].charNumber;
            stageInfo.enemyStatMultiplier = 1.0f;        //stage level에 따라 변동되는 constant
            

            string jsonData = JsonConvert.SerializeObject(stageInfo);
            jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/gameStageInfo.json", jsonData);
        }

        public void setCharacterInfo(int[] selectedCharacterIDs){
            stageInfo = new StageInformation();

            // StoryPick에서 저장한 정보 불러오기
            TextAsset textData = Resources.Load("Json_GameInfo/gameStageInfo") as TextAsset;
            string jsonData = textData.text;
            stageInfo = JsonConvert.DeserializeObject<StageInformation>(jsonData);
            
            stageInfo.selectedCharacterIDs = new List<int>();
            for(int i = 0; i < stageInfo.charNumber; i++){
                stageInfo.selectedCharacterIDs.Add(selectedCharacterIDs[i]);
            }

            jsonData = JsonConvert.SerializeObject(stageInfo);
            jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
            File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/gameStageInfo.json", jsonData);
        }
    }
}