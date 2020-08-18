using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;

public class LoadPlayerPrefab : MonoBehaviour
    {
    public int playerInitialNum;        public int playerRemainingNum;
    public GameObject[] prefabList;
    //public List<CharacterInformation> characterInfoList = new List<CharacterInformation>();
    public List<CharacterInformation_item> allCharList = new List<CharacterInformation_item>();
    public List<SelectedCharacterIDs> characterIDsList = new List<SelectedCharacterIDs>();
    public List<CharacterInformation_item> characterInfoListON = new List<CharacterInformation_item>();
    public int AccountID;
    void Awake()
    {
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        loadCharacterIDsFromJson();
        loadCharacterStatsFromJson();
        instantiateCharacterON();
    }
    public void loadCharacterIDsFromJson()
    {
        /*
        TextAsset JData = Resources.Load("여기에는 캐릭터 픽 창에서 선택한 3?명의 캐릭터 ID") as TextAsset;
            string jdata = JData.text;
            //string jdata = File.ReadAllText(Application.dataPath + "/Resources/CodeEditor/ActionData.json");
            //byte[] bytes = System.Convert.FromBase64String(jdata);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes); // this is the actual string
            //actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(reformat);
            characterIDsList = JsonConvert.DeserializeObject<List<SelectedCharacterIDs>>(jdata);

            //load on scene
        */
        List<int> tempArray = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").transform.GetComponent<_StageInformation>().selectedCharacterIDs;

        for(int i = 0; i < tempArray.Count; i++)
        {
            characterIDsList.Add(new SelectedCharacterIDs(tempArray[i]));
        }
        
        print("ID created!");
    }
    public void loadCharacterStatsFromJson()
    {
       
        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json");
        allCharList = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(jsonData);
            //string jdata = File.ReadAllText(Application.dataPath + "/Resources/CodeEditor/ActionData.json");
            //byte[] bytes = System.Convert.FromBase64String(jdata);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes); // this is the actual string
            //actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(reformat);
        
        /*
        //여기에는 내 모든 캐릭터의 데이터가 존재하고, 그걸 loadCharacterIDsFromJson에서 ID만 받아와서 생성하는거임.
        //                               characterID,       name,       lvl,exp,    hp,     dmg,    Range,  speed,  def,    critRate,   prefab
        characterInfoList.Add(new CharacterInformation(1,   "Ssong",    4,  1,      100,    60,     10,     5,      100,    50,         "Prefab_3")); //1는 지금동안은 무조건 내 캐릭터임.
        characterInfoList.Add(new CharacterInformation(2,   "Ssong",    3,  1,      100,    60,     10,     5,      100,    50,         "Prefab_3")); //1는 지금동안은 무조건 내 캐릭터임.
        characterInfoList.Add(new CharacterInformation(3,   "Ssong",    2,  1,      100,    10,     10,     5,      100,    50,         "Prefab_3")); //1는 지금동안은 무조건 내 캐릭터임.
        characterInfoList.Add(new CharacterInformation(4,   "Ssong",    1,  1,      100,    10,     10,     5,      100,    50,         "Prefab_3")); //1는 지금동안은 무조건 내 캐릭터임.
        */

        print("infoList created!");

        playerInitialNum = characterIDsList.Count; //2
        for(int i = 0; i < playerInitialNum; i++)
        {
            int tempID = characterIDsList[i].characterID;
            for(int j = 0; j < allCharList.Count; j++)
            {
                if(allCharList[j].ID_Character_Global == tempID)
                {
                    characterInfoListON.Add(allCharList[j]);
                    break;
                }
            }
        }
        print("charInfoListON created! number is " + characterInfoListON.Count);
    }
    public GameObject playerPrefab;
    public GameObject player;
    public GameObject wholePrefab;
    public void instantiateCharacterON()
    {
        for(int i = 0; i < playerInitialNum; i++)
        {
            player = (GameObject)Instantiate(Resources.Load("GamePlay/Prefab_"+characterInfoListON[i].prefabID), new Vector3(0,0,0), Quaternion.identity);
            player.transform.SetParent(wholePrefab.transform);
            player.transform.position = generatedPosition();
            player.transform.GetChild(3).tag = "Camera_Active";
            player.transform.tag = "Player";
            player.transform.name = "Player_"+(i+1).ToString();
            print(player.transform.position);

            EachCharacterStat tempStat = player.transform.GetChild(1).GetComponent<EachCharacterStat>();
            tempStat.characterID = characterInfoListON[i].ID_Character_Global;
            tempStat.name = characterInfoListON[i].name;
            tempStat.level = characterInfoListON[i].level;
            tempStat.experience = characterInfoListON[i].experience;
            tempStat.health = characterInfoListON[i].health_total;
            tempStat.damage = characterInfoListON[i].damage_total;
            tempStat.weaponRange = characterInfoListON[i].weaponRange_total;
            tempStat.speed = characterInfoListON[i].speed_total;
            tempStat.defense = characterInfoListON[i].defense_total;
            tempStat.criticalRate = characterInfoListON[i].criticalRate_total;
            tempStat.prefabID = characterInfoListON[i].prefabID.ToString();
            tempStat.maxHealth = characterInfoListON[i].health_total;
        }
    }


    private float xMin = -4f;        private float xMax = 24f;
    private float zMin = -4f;        private float zMax = 5f;
    Vector3 generatedPosition()
    {
        float x = UnityEngine.Random.Range(xMin,xMax);
        float z = UnityEngine.Random.Range(zMin,zMax);
        
        return new Vector3(x,2,z);
    }
}