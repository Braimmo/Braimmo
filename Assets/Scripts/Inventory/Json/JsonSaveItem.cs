using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class JsonSaveItem : MonoBehaviour
{
    //fromCharToCode toCode;
    EquipmentPanel equipmentPanel;
    Inventory inventory;
    Character character;
    JsonLoadItem jsonLoadItem;
    GameObject EP;
    GameObject IV;
    GameObject UIManager;
    public CharacterInformation_item characterStat;
    List<JsonEquippableItem> jsonEquippableItems;
    List<JsonEquippableItem> result;
    public List<CharacterInformation_item> characterStats;
    public List<List<EquippableItem>> allEquippedItems;
    public int charID = 0;
    public int AccountID;

    void Awake()
    {   
        AccountID = GameObject.Find("AccountID_DontDestroy").GetComponent<AccountID>().theID;
        jsonEquippableItems = new List<JsonEquippableItem>();
        characterStat = new CharacterInformation_item();

        EP = GameObject.Find("Equipment Panel");
        equipmentPanel = EP.GetComponent<EquipmentPanel>();

        IV = GameObject.Find("Inventory");
        inventory = IV.GetComponent<Inventory>();


        UIManager = GameObject.Find("UIManager");
        character = UIManager.GetComponent<Character>();

        jsonLoadItem = UIManager.GetComponent<JsonLoadItem>();
        characterStats = jsonLoadItem.characterStats;
//        print(characterStats[0].ID_Character_Global);

        allEquippedItems = UIManager.GetComponent<JsonLoadItem>().allEquippedItems;

        GameObject CP = GameObject.Find("charID_DontDestroy");
       // toCode = CP.GetComponent<fromCharToCode>();

    }



    public void saveEquipItem()
    {
        fromCharToCode.instance.characterID = charID;
        fromCharToCode.instance.name = characterStats[charID].name;
//        
        character.FinishEquip();

        result = new List<JsonEquippableItem>();

        //character stat 추가
        addCharacterStat();
        _saveCharEquippedItem();
        //allEquippedItems -> jsonEquippable item + characterInformation_stat에추가
        addEquippedItem();
        //unequipped item 추가
        addUnequippedItem();


        string jsonData = JsonConvert.SerializeObject(result);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string format = System.Convert.ToBase64String(bytes);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json", jsonData);

        jsonData = "";
        jsonData = JsonConvert.SerializeObject(characterStats);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json", jsonData);

        AssetDatabase.Refresh();

        if (GameObject.Find("ChatBox")){
            if (HomeSceneMenuControl.data.tutorialStage == 4)
            {
                HomeSceneMenuControl.data.tutorialStage = 6;
                HomeSceneMenuControl.SaveTutorial();
            }
            else if(HomeSceneMenuControl.data.tutorialStage == 14){
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_CodeEditor");

        }
        else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor");

        }
    }

    public void saveCharEquippedItem(int _charID)
    {
        character.FinishEquip();
        addCharacterStat();
        _saveCharEquippedItem();
        charID = _charID;
        
        fromCharToCode.instance.characterID = charID;
        fromCharToCode.instance.name = characterStats[charID].name;
    }

    private void _saveCharEquippedItem()
    {
        allEquippedItems[charID].Clear();
        for (int i = 0; i < equipmentPanel.equipmentSlots.Length; i++)
        {
            if (equipmentPanel.equipmentSlots[i].Item != null)
            {
                allEquippedItems[charID].Add(equipmentPanel.equipmentSlots[i].Item);
            }
        }
    }

    public void addEquippedItem()
    {
        for (int i = 0; i < allEquippedItems.Count; i++)
        {
            characterStats[i].equippedItemIds.Clear();
            for (int j = 0; j < allEquippedItems[i].Count; j++)
            {
                JsonEquippableItem item = makeJsonEquippableItem(allEquippedItems[i][j], true, i);
                result.Add(item); 
                characterStats[i].equippedItemIds.Add(item.itemID);
            }
        }

        print("equipped itme: " + result.Count);
    }

    public void addUnequippedItem()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName != null)
            {
                // Debug.Log("inventory item: " + inventory.items[i].itemName);
                JsonEquippableItem item = makeJsonEquippableItem((EquippableItem)inventory.items[i], false, -1);
                result.Add(item);
            }
        }
        print("unequipped itme: " + result.Count);
    }

    public void addCharacterStat()
    {
        // characterStats[charID].characterID = character.characterStat.characterID;
        // characterStats[charID].name = character.characterStat.name;
        // characterStats[charID].level = character.characterStat.level;
        // characterStats[charID].experience = character.characterStat.experience;
        // characterStats[charID].health_origin = character.characterStat.health_origin;
        // characterStats[charID].damage_origin = character.characterStat.damage_origin;
        // characterStats[charID].weaponRange_origin = character.characterStat.weaponRange_origin;
        // characterStats[charID].speed_origin = character.characterStat.speed_origin;
        // characterStats[charID].defense_origin = character.characterStat.defense_origin;
        // characterStats[charID].criticalRate_origin = character.characterStat.criticalRate_origin;

        //장착한 item의 stat
        characterStats[charID].health_item = character.equippedItemStat.healthAdd + character.equippedItemStat.healthMulti;
        characterStats[charID].damage_item = character.equippedItemStat.damageAdd + character.equippedItemStat.damageMulti;
        characterStats[charID].weaponRange_item = character.equippedItemStat.weaponRange;
        characterStats[charID].speed_item = character.equippedItemStat.speedAdd + character.equippedItemStat.speedMulti;
        characterStats[charID].defense_item = character.equippedItemStat.defenseAdd + character.equippedItemStat.defenseMulti;
        characterStats[charID].criticalDamage_item = character.equippedItemStat.criticalDamageAdd + character.equippedItemStat.criticalDamageMulti;
        characterStats[charID].criticalRate_item = character.equippedItemStat.criticalRateAdd + character.equippedItemStat.criticalRateMulti + character.equippedItemStat.criticalRateMulti;

        //total
        characterStats[charID].health_total = characterStats[charID].health_origin + characterStats[charID].health_item;
        characterStats[charID].damage_total = characterStats[charID].damage_origin + characterStats[charID].damage_item;
        characterStats[charID].weaponRange_total = characterStats[charID].weaponRange_origin + characterStats[charID].weaponRange_item;
        characterStats[charID].speed_total = characterStats[charID].speed_origin + characterStats[charID].speed_item;
        characterStats[charID].defense_total = characterStats[charID].defense_origin + characterStats[charID].damage_item;
        characterStats[charID].criticalDamage_total = characterStats[charID].criticalDamage_origin + characterStats[charID].criticalDamage_item;
        characterStats[charID].criticalRate_total = characterStats[charID].criticalRate_origin + characterStats[charID].criticalRate_item;

        characterStats[charID].maxHealth = characterStats[charID].health_total;
       // characterStats[charID].prefabID = character.characterStat.prefabID;
    }


    public JsonEquippableItem makeJsonEquippableItem(EquippableItem item, bool isEquipped, int charID)
    {
        JsonEquippableItem newItem = new JsonEquippableItem(item.itemID, item.itemName, item.healthAdd, item.healthMulti, item.damageAdd, item.damageMulti, item.weaponRange, item.speedAdd, item.speedMulti, item.defenseAdd, 
        item.defenseMulti, item.criticalDamageAdd, item.criticalDamageMulti, item.criticalRateAdd, item.criticalRateMulti, isEquipped, charID, item.itemID_forAccount);
        return newItem; 
    }


}
