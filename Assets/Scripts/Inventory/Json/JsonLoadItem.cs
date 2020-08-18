using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEditor;

public class JsonLoadItem : MonoBehaviour
{
    //현재 캐릭터 id가 0이라고 가정 -> 나중에 변경해야 함
    public int charID = 0;
    //public JsonFormat jsonFormat;

    public List<EquippableItem> unequippedItems;
    public List<EquippableItem> equippedItems;
    EquippableItem equippableItem;
    //public CharacterStat characterStat;
    //public EquippedItemStat equippedItemStat;
    public CharacterInformation_item characterStat;
    public List<CharacterInformation_item> characterStats;
    public List<JsonEquippableItem> jsonEquippableItem;
    public List<List<EquippableItem>> allEquippedItems;
    public int AccountID = 0; //계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ

    public void Awake()
    {
        //load();
        print("json load awake");
        characterStat = new CharacterInformation_item();
        unequippedItems = new List<EquippableItem>();
        equippedItems = new List<EquippableItem>();

        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json");
        jsonEquippableItem = JsonConvert.DeserializeObject<List<JsonEquippableItem>>(jsonData);


        jsonData = null;
        jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/CharacterInfo.json");
        characterStats = JsonConvert.DeserializeObject<List<CharacterInformation_item>>(jsonData);
        characterStat = characterStats[charID];

        setAllEquippedItem();
        loadOnScene();
        print("json load done: " + equippedItems.Count + ", " + unequippedItems.Count);
    }
    public void setAllEquippedItem()
    {
        allEquippedItems = new List<List<EquippableItem>>();
        for (int i = 0; i < characterStats.Count; i++)
        {
            allEquippedItems.Add(new List<EquippableItem>());
        }
        for (int i = 0; i < jsonEquippableItem.Count; i++)
        {
            EquippableItem newItem = new EquippableItem();
            newItem = makeItemSlots(jsonEquippableItem[i]);
            switch (jsonEquippableItem[i].charID)
            {
                case 0: allEquippedItems[0].Add(newItem); break;
                // case 1: allEquippedItems[1].Add(newItem); break;
                // case 2: allEquippedItems[2].Add(newItem); break;
                // case 3: allEquippedItems[3].Add(newItem); break;
                // case 4: allEquippedItems[4].Add(newItem); break;
                // case 5: allEquippedItems[5].Add(newItem); break;
                // case 6: allEquippedItems[6].Add(newItem); break;
                // case 7: allEquippedItems[7].Add(newItem); break;
                // case 8: allEquippedItems[8].Add(newItem); break;
                // case 9: allEquippedItems[9].Add(newItem); break;
                default: unequippedItems.Add(newItem); break;
            }
        }
    }

    public void refreshCharStatInfo(int _charID)
    {
        charID = _charID;
        characterStat = characterStats[charID];
        //print("characterStat: "+characterStat.ID_Character_Global);
    }
    public void loadOnScene()
    {
        print("jsonLoad current char id: " + charID);
        characterStat = characterStats[charID];


        for (int i = 0; i < allEquippedItems[charID].Count; i++)
        {
            equippedItems.Add(allEquippedItems[charID][i]);
        }
        print("equipped item: " + equippedItems.Count + ", unequppied item: " + unequippedItems.Count);
    }

    public void load()
    {
        List<JsonEquippableItem> jsonEquippedItem = new List<JsonEquippableItem>();
        // jsonEquippedItem.Add(new JsonEquippableItem("0_0_0_0", "helmet", 0f, 0f, 70f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("0_0_1_1", "helmet2", 0f, 0f, 100f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("1_0_0_0", "armor", 100f, 0f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("1_0_1_1", "armor2", 200f, 0f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_0_0", "sword", 0f, 120f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_1_1", "ax1", 0f, 100f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_2_2", "ax2", 0f, 120f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_3_3", "ax3", 0f, 130f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_4_4", "ax4", 0f, 140f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("4_0_5_5", "bow", 0f, 150f, 0f, 0f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("5_0_0_0", "potion", 0f, 0f, 0f, 70f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("5_0_1_1", "potion1", 0f, 0f, 0f, 90f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("5_0_2_2", "potion2", 0f, 0f, 0f, 110f, 0f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("6_0_0_0", "gem", 0f, 0f, 0f, 0f, 50f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("6_0_1_1", "gem2", 0f, 0f, 0f, 0f, 70f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("6_0_2_2", "heart", 0f, 0f, 0f, 0f, 00f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("6_0_3_3", "map", 0f, 100f, 0f, 0f, 00f, 0, false, -1));
        // jsonEquippedItem.Add(new JsonEquippableItem("6_0_4_4", "book", 0f, 0f, 100f, 0f, 00f, 0, false, -1));
        // 0        1       2       3       4               5       6       7           8               9   
        // itemID, itemName, health, damage, weaponRange, speed, defense, criticalDamage, critcalRate, itemEquipped, charID
        jsonEquippedItem.Add(new JsonEquippableItem("4_0_0_0", "Staff", 0f, 0f,      25f, 30f,       10f,       0f, 0f,        0f, 0f,      30f, 30f,   10f,50f,     false, -1, 0));
        jsonEquippedItem.Add(new JsonEquippableItem("5_0_0_0", "Potion", 30f, 30f,    0f,0f,         0f,        0f, 0f,        0f, 0f,      0f,0f,      0f,0f,       false, -1, 1));

        string jsonData = JsonConvert.SerializeObject(jsonEquippedItem);
        jsonData = JValue.Parse(jsonData).ToString(Formatting.Indented);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string format = System.Convert.ToBase64String(bytes);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString() + "/ItemsInfo.json", jsonData);
        //File.WriteAllText(Application.dataPath + "/Scripts/Inventory/Json/jsonEquippedItemListData.json",jsonData);
        AssetDatabase.Refresh();
    }

    public EquippableItem makeItemSlots(JsonEquippableItem item)
    {
        string itemID = item.itemID;
        string[] result = itemID.Split('_');
        EquippableItem newItem = new EquippableItem();
        newItem.itemName = item.itemName;
        newItem.itemID = item.itemID;
        newItem.itemID_forAccount = item.itemID_forAccount;
        int type = int.Parse(result[0]);
        int id = int.Parse(result[2]);
        print("type: " + type + "id: " + id);
        newItem.EquipmentType = equipmentTypeToEquipmentType(type);
        newItem.icon = Resources.Load<Sprite>("ItemDB/" + newItem.EquipmentType + "/" + id);
        newItem.healthAdd = item.healthAdd;                     newItem.healthMulti = item.healthMulti;
        newItem.damageAdd = item.damageAdd;                     newItem.damageMulti = item.damageMulti;
        newItem.weaponRange = item.weaponRange;     
        newItem.speedAdd = item.speedAdd;                       newItem.speedMulti = item.speedMulti;
        newItem.defenseAdd = item.defenseAdd;                   newItem.defenseMulti = item.defenseMulti;
        newItem.criticalDamageAdd = item.criticalDamageAdd;     newItem.criticalDamageMulti = item.criticalDamageMulti;
        newItem.criticalRateAdd = item.criticalRateAdd;         newItem.criticalRateMulti = item.criticalRateMulti;
        return newItem;
    }

    private EquipmentType equipmentTypeToEquipmentType(int type)
    {
        switch (type)
        {
            case 0: return EquipmentType.Helmet; break;
            case 1: return EquipmentType.Chest; break;
            case 2: return EquipmentType.Gloves; break;
            case 3: return EquipmentType.Boots; break;
            case 4: return EquipmentType.Weapon; break;
            case 5: return EquipmentType.Potion; break;
            case 6: return EquipmentType.Accessory; break;
            default: return EquipmentType.Accessory;
        }
    }

}
