using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CharacterPick
{
    public class LoadCharInfo : MonoBehaviour
    {
        public List<CharacterInformation_item> characters;
        public List<int> enemies;
        public _StageInformation _stageInfo;
        public StageInformation stageInfo;
        public int characterNum;
        public int enemyNum;
        public int AccountID = 0; // 계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
        public passDataBetweenScene passData;

        void Awake()
        {
            if (GameObject.Find("PassStageInfoBetweenScenes_dontDestroy") != null)
            {
                print("loadCharInfo find pass");
                passData = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>();
                _stageInfo = passData.GetComponent<_StageInformation>();
            }
            else
            {
                Debug.Log("No dontdestroy");
                TextAsset textData = Resources.Load("Json_GameInfo/Age0/StageInfo") as TextAsset;
                string charJsonData = textData.text;
                List<StageInformation> stages = JsonConvert.DeserializeObject<List<StageInformation>>(charJsonData);
                stageInfo = stages[0];
            }
            loadCharsJson();
            loadEnemyJson();
        }

        public void loadCharsJson()
        {
            TextAsset textData = Resources.Load("Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo") as TextAsset;
            string charJsonData = textData.text;
            characters = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(charJsonData);

            characterNum = characters.Count;
        }

        public void loadEnemyJson()
        {
            enemyNum = _stageInfo.enemyNumber;
            enemies = new List<int>(_stageInfo.enemyIds);
        }

    }
}