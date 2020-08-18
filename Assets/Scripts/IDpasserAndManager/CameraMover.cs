using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;
public class CameraMover : MonoBehaviour
{
    public GameObject camera;
    
    void Awake()
    {
        camera.transform.position = new Vector2(0,0);
    }
    public void ToCodeEditorPage()
    {
        camera.transform.position = new Vector2(-522,719);
    }
    public void ToMainPage()
    {
        camera.transform.position = new Vector2(0,0);
    }
    public void ToCharacterCreationPage()
    {
        camera.transform.position = new Vector2(519,726);
    }
    public InputField charCreate_AccountText;
    public InputField charCreate_IDCharacterGlobalText;
    public InputField charCreate_IDCharacterAccountText;
    public InputField charCreate_nameText;

    public void createCharacter_Mine()
    {
        List<CharacterInformation_item> characters = new List<CharacterInformation_item>();
        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + charCreate_AccountText.text + "/CharacterInfo.json");
        characters = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(jsonData);
        
        CharacterInformation_item character = new CharacterInformation_item();
        character.ID_Character_Global = int.Parse(charCreate_IDCharacterGlobalText.text);
        character.ID_Character_Account = int.Parse(charCreate_IDCharacterAccountText.text);
        character.name = charCreate_nameText.text;
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

        string jdata = JsonConvert.SerializeObject(characters);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + charCreate_AccountText.text + "/CharacterInfo.json", jdata);
        AssetDatabase.Refresh();
    }
    public void createCharacter_Enemy()
    {
        
    }
    
    public int stageID;
    public InputField createAward_Items_stageID_Text;
    public InputField createAward_Items_toAdd_Text;

    public void toCreateAward_ItemsPage()
    {
        camera.transform.position = new Vector2(519,-734);
    }
    public void createAward_Items()
    {
        //Json_GameInfo에서 StageAwards에서
        //숫자 (stage 이름)을 읽어오는 칸이 하나 필요하고
        //거기서 아이템을 읽어온다.

        stageID = int.Parse(createAward_Items_stageID_Text.text);

        AwardDB_Item toAddAwardDB = new AwardDB_Item();
        List<AwardDB_Item> AwardDB = new List<AwardDB_Item>();
        string jsonData;
        try
        {
            jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_GameInfo/StageAwards/" + stageID + "/AwardDB_Item.json");
            AwardDB = JsonConvert.DeserializeObject<List<AwardDB_Item>>(jsonData);
        }
        catch (System.Exception)
        {
            print("errrrrrrrrrrrrror");
        }
        
        string toAdd = "";

        toAdd = createAward_Items_toAdd_Text.text;
        toAddAwardDB.itemTotalID = toAdd;

        AwardDB.Add(toAddAwardDB);

        string jdata = JsonConvert.SerializeObject(AwardDB);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_GameInfo/StageAwards/" + stageID + "/AwardDB_Item.json", jdata);
        AssetDatabase.Refresh();
    }


    public InputField createAward_Conditions_stageID_Text;
    public InputField createAward_Conditions_conditionID_Text;
    public InputField createAward_Conditions_rangeMin_Text;
    public InputField createAward_Conditions_rangeMax_Text;
    public InputField createAward_Conditions_upDownOn_Text;
    public InputField createAward_Conditions_prefab_Text;

    public void toCreateAward_ConditionPage()
    {
        camera.transform.position = new Vector2(-552,-734);
    }

    public void createAward_Conditions()
    {
        //Json_GameInfo에서 StageAwards에서
        //숫자 (stage 이름)을 읽어오는 칸이 하나 필요하고
        //거기서 아이템을 읽어온다.

        stageID = int.Parse(createAward_Conditions_stageID_Text.text);

        AwardDB_Condition toAddAwardDB = new AwardDB_Condition();
        List<AwardDB_Condition> AwardDB = new List<AwardDB_Condition>();
        string jsonData;
        try
        {
            jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_GameInfo/StageAwards/" + stageID + "/AwardDB_Condition.json");
            AwardDB = JsonConvert.DeserializeObject<List<AwardDB_Condition>>(jsonData);
        }
        catch (System.Exception)
        {
            print("errrrrrrrrrrrrror");
        }
        int conditionID = 0, rangeMin = 0, rangeMax = 0;
        string upDownOn = ""; string thePrefab = "";

        conditionID = int.Parse(createAward_Conditions_conditionID_Text.text);
        rangeMin = int.Parse(createAward_Conditions_rangeMin_Text.text);
        rangeMax = int.Parse(createAward_Conditions_rangeMax_Text.text);
        upDownOn = createAward_Conditions_upDownOn_Text.text;
        thePrefab = createAward_Conditions_prefab_Text.text;

        toAddAwardDB.conditionID = conditionID;
        toAddAwardDB.range_max = rangeMax;
        toAddAwardDB.range_min = rangeMin;
        toAddAwardDB.upDownOn = upDownOn;
        toAddAwardDB.conditionPrefab = thePrefab;

        AwardDB.Add(toAddAwardDB);

        string jdata = JsonConvert.SerializeObject(AwardDB);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_GameInfo/StageAwards/" + stageID + "/AwardDB_Condition.json", jdata);
        AssetDatabase.Refresh();
    }
}
