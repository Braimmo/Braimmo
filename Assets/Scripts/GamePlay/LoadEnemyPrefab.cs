using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;

public class LoadEnemyPrefab : MonoBehaviour
    {
    public int playerInitialNum;        public int playerRemainingNum;
    public GameObject[] prefabList;

    public List<EnemyInformation> characterInfoList = new List<EnemyInformation>();
    public List<EnemyInformation> characterInfoListON = new List<EnemyInformation>();
    public List<SelectedCharacterIDs> characterIDsList = new List<SelectedCharacterIDs>();
    public _StageInformation stageInfo;


    void Awake()
    {
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

        stageInfo = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<_StageInformation>();

        for(int i = 0; i < stageInfo.enemyIds.Count; i ++)
        {
            characterIDsList.Add(new SelectedCharacterIDs(stageInfo.enemyIds[i]));
        }
        print("ID created!");
    }
    public void loadCharacterStatsFromJson()
    {
        /*
        TextAsset JData = Resources.Load("여기에는 모든 캐릭터 스텟이 담겨잇는 JSON") as TextAsset;
            string jdata = JData.text;
            //string jdata = File.ReadAllText(Application.dataPath + "/Resources/CodeEditor/ActionData.json");
            //byte[] bytes = System.Convert.FromBase64String(jdata);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes); // this is the actual string
            //actionList = JsonConvert.DeserializeObject<List<Action_CodeModifier>>(reformat);
            characterInfoList = JsonConvert.DeserializeObject<List<CharacterInformation>>(jdata);
        */
        //                                      characterID, name,      lvl,    hp,     dmg,    Range,  speed,  def,    critRate,   prefab
        characterInfoList.Add(new EnemyInformation(0,       "Enemy1",   1,      300,     5,     3,      5,       100,    50,         10,      "Prefab_6")); //여기서 중요한건 적캐릭터1의 아이디가 다 다르다는 것.
        characterInfoList.Add(new EnemyInformation(1,       "Enemy1",   1,      300,     5,     3,      5,       100,    50,         10,      "Prefab_6")); 
        characterInfoList.Add(new EnemyInformation(2,       "Enemy1",   1,      300,     5,      5,      2,      100,    50,        10,      "Prefab_2")); 
        characterInfoList.Add(new EnemyInformation(3,       "Enemy1",   1,      400,     5,      5,      2,      100,    50,        10,      "Prefab_2")); 
        characterInfoList.Add(new EnemyInformation(4,       "Enemy1",   1,      500,     5,      5,      2,      100,    50,        10,      "Prefab_2")); 
        print("infoList created! total char num of enemy is " + characterIDsList.Count);

        playerInitialNum = characterIDsList.Count; //총 캐릭터 개수
        for(int i = 0; i < playerInitialNum; i++) //총 캐릭터 개수 동안 for loop
        {
            int tempID = characterIDsList[i].characterID; //tempID 는 만들어진 첫 캐릭터의 ID
            for(int j = 0; i < characterInfoList.Count; j++)
            {
                if(characterInfoList[j].characterID == tempID) //만약 그 캐릭터가 이 캐릭터의 아이디가 맞다면?
                {
                    characterInfoListON.Add(characterInfoList[j]); //그 캐릭터 아이디로 캐릭터 생성...
                    break;
                }
            }
        }
        print("charInfoListON created! number is " + characterInfoListON.Count);
    }
    public GameObject enemyPrefab;
    public GameObject enemy;
    public GameObject wholePrefab;
    public void instantiateCharacterON()
    {
        for(int i = 0; i < playerInitialNum; i++)
        {
            enemy = (GameObject)Instantiate(Resources.Load("GamePlay/"+characterInfoListON[i].prefabID), new Vector3(0,0,0), Quaternion.identity);
            enemy.transform.SetParent(wholePrefab.transform);
            enemy.transform.position = generatedPosition();
            enemy.transform.GetChild(3).tag = "Camera_notActive";
            enemy.transform.tag = "Enemy";
            enemy.transform.name = "Enemy_"+(i+1).ToString();
            print(enemy.transform.position);

            EachCharacterStat tempStat = enemy.transform.GetChild(1).GetComponent<EachCharacterStat>();
            tempStat.characterID = characterInfoListON[i].characterID;
            tempStat.name = characterInfoListON[i].name;
            tempStat.level = characterInfoListON[i].level;
            tempStat.health = characterInfoListON[i].health;
            tempStat.damage = characterInfoListON[i].damage;
            tempStat.weaponRange = characterInfoListON[i].weaponRange;
            tempStat.speed = characterInfoListON[i].speed;
            tempStat.defense = characterInfoListON[i].defense;
            tempStat.criticalRate = characterInfoListON[i].criticalRate;
            tempStat.prefabID = characterInfoListON[i].prefabID;
            tempStat.maxHealth = characterInfoListON[i].health;
        }
    }


    private float xMin = -4f;        private float xMax = 24f;
    private float zMin = 16f;        private float zMax = 24f;
    Vector3 generatedPosition()
    {
        float x = UnityEngine.Random.Range(xMin,xMax);
        float z = UnityEngine.Random.Range(zMin,zMax);
        
        return new Vector3(x,2,z);
    }
}