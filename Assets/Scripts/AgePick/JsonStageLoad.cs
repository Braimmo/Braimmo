using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;
using AgePick;

namespace AgePick{
    //이제 이거 accountInfo에 옮겨야 됨
      public class JsonStageLoad : MonoBehaviour
    {   
        private List<JsonAgeStageFormat> _jsonAgeStageFormat;
        public List<JsonAgeStageFormat> jsonAgeStageFormat;
        int ageNum = 8;
        public int ageLevel;
        public void Awake(){
            ageLevel = 1;
            _jsonAgeStageFormat = new List<JsonAgeStageFormat>();
            jsonAgeStageFormat = new List<JsonAgeStageFormat>();
            //saveJsonAgeStage();
            loadJsonAgeStage();
        }

        void loadJsonAgeStage(){
            string jsonData = File.ReadAllText(Application.dataPath + "/Scripts/AgePick/jsonAgeInfo.json");
            jsonAgeStageFormat = JsonConvert.DeserializeObject<List<JsonAgeStageFormat>>(jsonData);
        }

        void saveJsonAgeStage(){
            for(int i = 0; i < 2; i++){
                JsonAgeStageFormat newAge = new JsonAgeStageFormat();
                newAge.AgeID = i+0;
                newAge.AgeSprite = 0;
                newAge.AgeName = "이상한 나라의 앨리스";
                newAge.AgeInfo = "챕터 1은 이상한 나라의 앨리스입니다.\n붉은 여왕의 분노에 맞서 블러드 루비를 탈환하세요 .\n ";
                newAge.EmblemSprite = 0;
                newAge.EmblemName = "블러드 루비";
                newAge.EmblemInfo = "공격력: 313up\n방어력: 212up\n크리티컬확률: 100up";
                newAge.CharSprite = 0;
                newAge.CharName = "붉은여왕";
                _jsonAgeStageFormat.Add(newAge);

                newAge = new JsonAgeStageFormat();
                newAge.AgeID = i+1;
                newAge.AgeSprite = 1;
                newAge.AgeName = "난중일기";
                newAge.AgeInfo = "챕터 2는 난중일기입니다. 이순신 장군을 도와 명량해전을 승리로 이끄세요.\n";
                newAge.EmblemSprite = 1;
                newAge.EmblemName = "도깨비 호패";
                newAge.EmblemInfo = "공격력: 212\n방어력: 400up\n크리티컬확률: 300up";
                newAge.CharSprite = 1;
                newAge.CharName = "도깨비 호패";
                _jsonAgeStageFormat.Add(newAge);

                newAge = new JsonAgeStageFormat();
                newAge.AgeID = i+2;
                newAge.AgeSprite = 2;
                newAge.AgeName = "천일야화";
                newAge.AgeInfo = "챕터 3은 천일야화입니다.\n 신밧드가 되어 신비로운 동굴에 감춰진 황금을 찾으세요!\n";
                newAge.EmblemSprite = 2;
                newAge.EmblemName = "골드 엠블럼";
                newAge.EmblemInfo = "공격력: 113up\n방어력: 60\n크리티컬확률: 600";
                newAge.CharSprite = 2;
                newAge.CharName = "신밧드";
                _jsonAgeStageFormat.Add(newAge);

                newAge = new JsonAgeStageFormat();
                newAge.AgeID = i+3;
                newAge.AgeSprite = 3;
                newAge.AgeName = "헨젤과 그레텔";
                newAge.AgeInfo = "챕터 4는 헨젤과 그레텔입니다.\n 마녀와 괴물들을 피해 마녀의 묘약을 얻으세요!\n";
                newAge.EmblemSprite = 3;
                newAge.EmblemName = "마녀의 심장";
                newAge.EmblemInfo = "공격력: 114up\n방어력: 200up\n크리티컬확률: 300";
                newAge.CharSprite = 3;
                newAge.CharName = "헨젤과 그레텔";
                _jsonAgeStageFormat.Add(newAge);
            }
            string jsonData = JsonConvert.SerializeObject(_jsonAgeStageFormat);
            jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
            File.WriteAllText(Application.dataPath + "/Scripts/AgePick/jsonAgeInfo.json", jsonData);
            
            AssetDatabase.Refresh();
        }
    }
}
