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
    int AccountCount;
    void Awake()
    {
        checkAccountCount();
    }

    public void checkAccountCount()
    {
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/AccountCounter.json");
        AccountCount = JsonConvert.DeserializeObject<int>(JData);
        print("this is " + AccountCount);
    }
    
    /*
    void Start()
    {
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        Debug.Log("Userinfo = " + data.tutorialStage);
    }
    */
    public void goHomeScene()
    {
        checkAccountCount();
        GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID = AccountCount - 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }

    public List<Condition_CodeModifier>conditionList = new List<Condition_CodeModifier>();
    public List<Action_CodeModifier>actionList = new List<Action_CodeModifier>();
    public void tutoCodeResetter(int accountID)
    {
        conditionList = new List<Condition_CodeModifier>();
        conditionList.Add(new Condition_CodeModifier(101, 1001, "HPMT",             "내 체력 25% 이상",             0, 0f, 0f, 888.8f, 25f,"Condition1","Up", -1));

        string jdata = JsonConvert.SerializeObject(conditionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(Application.dataPath + "/Scripts/CodeEditor/ConditionData.json",format);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountCount.ToString() + "/ConditionData.json",jdata); //ID 추가해주기

        data = new UserInformation();
        data.name = "Newbie";
        data.experience = 0;
        data.gem = 10;
        data.money = 100;
        data.ageID = 0;
        data.stageID = 0;
        data.tutorialStage = 0;
        data.level = 0;

        jdata = JsonConvert.SerializeObject(data);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountCount.ToString() + "/userInformation.json",jdata); //ID 추가해주기


        AssetDatabase.Refresh();

    }

    public void CreateNewAccount()
    {
        string guid = AssetDatabase.CreateFolder("Assets/Resources/Json_AccountInfo", AccountCount.ToString());
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        tutoCodeResetter(AccountCount);
        AccountResetter(AccountCount);
        GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID = AccountCount;

        string jdata = JsonConvert.SerializeObject(AccountCount+1);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/AccountCounter.json",jdata); //ID 추가해주기


        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }

    public void AccountResetter(int accountID)
    {
        List<CharacterInformation_item> characters = new List<CharacterInformation_item>();

        string jdata = JsonConvert.SerializeObject(characters);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + accountID.ToString() + "/CharacterInfo.json", jdata);

        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + accountID.ToString() + "/CharacterInfo.json");
        characters = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(jsonData);
        
        CharacterInformation_item character = new CharacterInformation_item();
        character.ID_Character_Global = 0;
        character.ID_Character_Account = 0;
        character.name = "Alice";
        character.level = 1;
        character.experience = 1000;
        
        character.health_origin = 100;
        character.damage_origin = 30;
        character.weaponRange_origin = 10;
        character.speed_origin = 10;
        character.defense_origin= 100;
        character.criticalRate_origin = 50; 
        character.health_item = 0;
        character.damage_item = 0;
        character.weaponRange_item = 0;
        character.speed_item = 0;
        character.defense_item = 0;
        character.criticalRate_item = 0; 
        character.health_total = character.health_origin + character.health_item;
        character.damage_total = character.damage_origin + character.damage_item;
        character.weaponRange_total = character.weaponRange_origin + character.weaponRange_item;
        character.speed_total = character.speed_origin + character.speed_item;
        character.defense_total = character.speed_origin + character.speed_item;
        character.criticalRate_total = character.criticalRate_origin + character.criticalRate_item;
        
        character.maxHealth =  character.health_total;
        character.prefabID = 3;
        characters.Add(character);

        jdata = JsonConvert.SerializeObject(characters);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + accountID.ToString() + "/CharacterInfo.json", jdata);
        AssetDatabase.Refresh();
        //______________________________________________________________________________________________________________
        List<JsonEquippableItem> jsonEquippedItem = new List<JsonEquippableItem>();
        jsonEquippedItem.Add(new JsonEquippableItem("4_0_0_0", "Staff", 0f, 0f,      25f, 30f,       10f,       0f, 0f,        0f, 0f,      30f, 30f,   10f,50f,     false, -1, 0));
        jsonEquippedItem.Add(new JsonEquippableItem("5_0_0_0", "Potion", 30f, 30f,    0f,0f,         0f,        0f, 0f,        0f, 0f,      0f,0f,      0f,0f,       false, -1, 1));
        jsonData = JsonConvert.SerializeObject(jsonEquippedItem);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + accountID.ToString() + "/ItemsInfo.json", jsonData);
        //______________________________________________________________________________________________________________
        string guid = AssetDatabase.CreateFolder("Assets/Resources/Json_AccountInfo/"+AccountCount.ToString(), "Alice");
        string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
        //______________________________________________________________________________________________________________
        TargetOrderInformation theTargetOrderInformation = new TargetOrderInformation();
        theTargetOrderInformation.closeFar = "close";
        theTargetOrderInformation.aimOrderArray = new int[] {4,3,0,2,1};
        jdata = JsonConvert.SerializeObject(theTargetOrderInformation);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ accountID.ToString() + "/Alice/TargetOrder.json",jdata); //ID 추가해주기
        //______________________________________________________________________________________________________________
        actionList = new List<Action_CodeModifier>();
        actionList.Add(new Action_CodeModifier(1,   "useWeapon",        "무기 사용",            2, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(2,   "useHealthPotion",  "체력 포션 사용",       3, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(3,   "moveForward",      "타겟에게 전진",        1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(4,   "moveBackward",     "타겟에게서 후진",      1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(5,   "moveLeft",         "타겟의 좌측으로 이동", 1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(6,   "moveRight",        "타겟의 우측으로 이동", 1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(7,   "moveNorth",        "맵의 위쪽으로 이동",   1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(8,   "moveSouth",        "맵의 아래쪽으로 이동", 1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(9,   "moveWest",         "맵의 왼쪽으로 이동",   1, 2, 0f, 0f));
        actionList.Add(new Action_CodeModifier(10,  "moveEast",         "맵의 오른쪽으로 이동", 1, 2, 0f, 0f));
        jdata = JsonConvert.SerializeObject(actionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ accountID.ToString() + "/Alice/ActionData.json",jdata); //ID 추가해주기
        AssetDatabase.Refresh();
    }

}
