using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class CreateAwards : MonoBehaviour
{
    public void randomizeAward_Condition(int stageID,GameObject award_)
    {
        AwardDB_Condition toAddAwardDB = new AwardDB_Condition();
        List<AwardDB_Condition> AwardDB = new List<AwardDB_Condition>();

        Condition_CodeModifier toAddCondition = new Condition_CodeModifier();
        List<Condition_CodeModifier>conditionList = new List<Condition_CodeModifier>();
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
        //AwardDB는 총 뽑을 수 있는 놈.
        int selectedIndex = Random.Range(0,AwardDB.Count);
        int theConditionID = AwardDB[selectedIndex].conditionID;
        int theMin = AwardDB[selectedIndex].range_min;
        int theMax = AwardDB[selectedIndex].range_max;
        string theUpDownOn = AwardDB[selectedIndex].upDownOn;
        string thePrefab = AwardDB[selectedIndex].conditionPrefab;
        int selectedValue = Random.Range(theMin,theMax);

        int AccountID = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>().AccountID;
        jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString()+ "/ConditionData.json");
        conditionList = JsonConvert.DeserializeObject<List<Condition_CodeModifier>>(jsonData);

        toAddCondition.conditionID = theConditionID;
        toAddCondition.conditionUniqueID = conditionList.Count + 1 + 1000;
        toAddCondition.conditionName = makeConditionName(theConditionID);
        toAddCondition.conditionDescription = makeConditionDescription(theConditionID, selectedValue);
        toAddCondition.conditionParent = 0;
        toAddCondition.conditionPositionX = 0f;
        toAddCondition.conditionPositionY = 0f;
        toAddCondition.goldValue = 888.8f;
        toAddCondition.conditionValue = selectedValue;
        toAddCondition.conditionPrefab = thePrefab;
        toAddCondition.conditionValueState = theUpDownOn;
        toAddCondition.usingCharID = -1;

        conditionList.Add(toAddCondition);

        string jdata = JsonConvert.SerializeObject(conditionList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/"+ AccountID.ToString() + "/ConditionData.json",jdata); //ID 추가해주기

        award_.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CodeEditor/" + thePrefab);
        award_.transform.GetChild(1).GetComponent<Text>().text = "조건 - " + toAddCondition.conditionDescription;
    }
    string makeConditionName(int theID)
    {
        string theName = "";
        switch(theID)
        {
            case 101:   theName = "HPMT";   break;
            case 102:   theName = "HPLT";   break;
            case 201:   theName = "DPSMT";   break;
            case 202:   theName = "DPSLT";   break;
            case 203:   theName = "isStunned";   break;
            case 204:   theName = "BuffedAtk";   break;
            case 205:   theName = "DebuffedDef";   break;
            case 206:   theName = "BuffedDef";   break;
            case 207:   theName = "DebuffedAtk";   break;
            case 301:   theName = "EnemyMT";   break;
            case 302:   theName = "EnemyLT";   break;
            case 401:   theName = "OAuseWeapon";   break;
            case 402:   theName = "OAuseHealthPotion";   break;
            case 403:   theName = "OAmoveForward";   break;
            case 404:   theName = "OAmoveBackward";   break;
            case 405:   theName = "OAmoveLeft";   break;
            case 406:   theName = "OAmoveRight";   break;   
            case 407:   theName = "OAmoveNorth";   break;
            case 408:   theName = "OAmoveSouth";   break;
            case 409:   theName = "OAmoveWest";   break;
            case 410:   theName = "OAmoveEast";   break;
            default:
                break;
        }
        return theName;
    }

    string makeConditionDescription(int theID, int selectedValue)
    {
        string theDescription = "";
        switch(theID)
        {
            case 101:   theDescription = "내 체력 " + selectedValue + "% 이상";   break;
            case 102:   theDescription = "내 체력 " + selectedValue + "% 이하";   break;
            case 201:   theDescription = "한번에 받는 데미지 " + selectedValue + "% 이상";   break;
            case 202:   theDescription = "한번에 받는 데미지 " + selectedValue + "% 이하";   break;
            case 203:   theDescription = "내 상태 기절";   break;
            case 204:   theDescription = "공격력 버프 유지 중";   break;
            case 205:   theDescription = "방어력 디버프 유지 중";   break;
            case 206:   theDescription = "방어력 버프 유지 중";   break;
            case 207:   theDescription = "공격력 디버프 유지 중";   break;
            case 301:   theDescription = "공격 반경 안 적이 " + selectedValue + "명 이상";   break;
            case 302:   theDescription = "공격 반경 안 적이 " + selectedValue + "명 이하";   break;
            case 401:   theDescription = "무기 사용 중";   break;
            case 402:   theDescription = "체력 포션 사용 중";   break;
            case 403:   theDescription = "타겟에게 전진 중";   break;
            case 404:   theDescription = "타겟에게서 후진 중";   break;
            case 405:   theDescription = "타겟의 좌측으로 이동 중";   break;
            case 406:   theDescription = "타겟의 우측으로 이동 중";   break;   
            case 407:   theDescription = "맵의 위쪽으로 이동 중";   break;
            case 408:   theDescription = "맵의 아래쪽으로 이동 중";   break;
            case 409:   theDescription = "맵의 왼쪽으로 이동 중";   break;
            case 410:   theDescription = "맵의 오른쪽으로 이동 중";   break;
            default:
                break;
        }
        return theDescription;
    }
    public void randomizeAward_Item(int stageID, GameObject award_)
    {
        AwardDB_Item toAddAwardDB = new AwardDB_Item();
        List<AwardDB_Item> AwardDB = new List<AwardDB_Item>();

        JsonEquippableItem toAddItem = new JsonEquippableItem();
        List<JsonEquippableItem>JsonItemsList = new List<JsonEquippableItem>();
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

        //AwardDB는 총 뽑을 수 있는 놈.
        int selectedIndex = Random.Range(0,AwardDB.Count);
        string[] split_itemTotalID_toString = AwardDB[selectedIndex].itemTotalID.Split('_');
        // X_X_X_X니까 이 Array의 Length는 4가 될 것임.
        int[] split_itemTotalID = new int[4];
        for(int i = 0; i < split_itemTotalID_toString.Length; i++)
        {//int로 바꿔주자.
            split_itemTotalID[i] = int.Parse(split_itemTotalID_toString[i]);
        }
        // itemType rarity itemUniqueID itemPrefabID
        // 0-Helmet 1-Chest 2-Gloves    3-Boots 4-Weapon    5-Potion    6-Accessory
        
        int AccountID = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>().AccountID;
        jsonData = File.ReadAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString()+ "/ItemsInfo.json");
        JsonItemsList = JsonConvert.DeserializeObject<List<JsonEquippableItem>>(jsonData);

        toAddItem.itemID = AwardDB[selectedIndex].itemTotalID;

        //아이템 이름 정하기
        string name_rare = "";
        switch(split_itemTotalID[1])
        {
            case 0:   name_rare = "낡은 "   ;   break;
            case 1:   name_rare = "허름한 " ;   break;
            case 2:   name_rare = "보통의 " ;   break;
            case 3:   name_rare = "준수한 " ;   break;
            case 4:   name_rare = "명품의 " ;   break;
            case 5:   name_rare = "희귀한 " ;   break;
            case 6:   name_rare = "전설의 " ;   break;
            default:    break;
        }

        string name_type = "";
        string forPrefab = "";
        switch(split_itemTotalID[0])
        {
            case 0:
                forPrefab = "Helmet";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "철 헬멧"   ;   break;
                    case 1:   name_type = "천 모자" ;   break;
                    case 2:   name_type = "가죽 모자 " ;   break;
                    default:    break;
                }
                break;
            case 1:
                forPrefab = "Chest";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "철 갑옷"   ;   break;
                    case 1:   name_type = "셔츠" ;   break;
                    case 2:   name_type = "가죽 자켓" ;   break;
                    default:    break;
                }
                break;
            case 2:
                forPrefab = "Glove";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "철 글러브"   ;   break;
                    case 1:   name_type = "가죽 장갑" ;   break;
                    case 2:   name_type = "털장갑 " ;   break;
                    default:    break;
                }
                break;
            case 3:
                forPrefab = "Boots";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "짚신"   ;   break;
                    case 1:   name_type = "쪼리" ;   break;
                    case 2:   name_type = "부츠 " ;   break;
                    default:    break;
                }
                break;
            case 4:
                forPrefab = "Weapon";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "무기1"   ;   break;
                    case 1:   name_type = "무기2" ;   break;
                    case 2:   name_type = "무기3 " ;   break;
                    default:    break;
                }
                break;
            case 5:
                forPrefab = "Potion";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "체력 포션"   ;   break;
                    case 1:   name_type = "공격력 포션" ;   break;
                    case 2:   name_type = "방어력 포션 " ;   break;
                    default:    break;
                }
                break;
            case 6:
                forPrefab = "Accessory";
                switch(split_itemTotalID[2])
                {
                    case 0:   name_type = "진주 목걸이"   ;   break;
                    case 1:   name_type = "진주 팔찌" ;   break;
                    case 2:   name_type = "진주 귀걸이 " ;   break;
                    default:    break;
                }
                break;
            default:    break;
        }
        toAddItem.itemName = name_rare + name_type;
        toAddItem.healthAdd =           Mathf.RoundToInt(split_itemTotalID[1] * 10 * Random.Range(2.99f,9.99f));
        toAddItem.healthMulti =         Mathf.RoundToInt(split_itemTotalID[1] * 5 * Random.Range(1.00f,4.00f));
        toAddItem.damageAdd =           Mathf.RoundToInt(split_itemTotalID[1] * 3 * Random.Range(1.00f,4.00f));
        toAddItem.damageMulti =         Mathf.RoundToInt(split_itemTotalID[1] * 2 * Random.Range(1.00f,4.00f));
        toAddItem.weaponRange =         Mathf.RoundToInt(split_itemTotalID[1] * 1 * Random.Range(1.00f,4.00f));
        toAddItem.speedAdd =            Mathf.RoundToInt(split_itemTotalID[1] * 5 * Random.Range(1.00f,4.00f));
        toAddItem.speedMulti =          Mathf.RoundToInt(split_itemTotalID[1] * 1 * Random.Range(1.00f,4.00f));
        toAddItem.defenseAdd =          Mathf.RoundToInt(split_itemTotalID[1] * 5 * Random.Range(1.00f,4.00f));
        toAddItem.defenseMulti =        Mathf.RoundToInt(split_itemTotalID[1] * 5 * Random.Range(1.00f,4.00f));
        toAddItem.criticalDamageAdd =   Mathf.RoundToInt(split_itemTotalID[1] * 2 * Random.Range(1.00f,4.00f));
        toAddItem.criticalDamageMulti = Mathf.RoundToInt(split_itemTotalID[1] * 5 * Random.Range(1.00f,4.00f));
        toAddItem.criticalRateAdd =     Mathf.RoundToInt(split_itemTotalID[1] * 1 * Random.Range(1.00f,4.00f));
        toAddItem.criticalRateMulti =   Mathf.RoundToInt(split_itemTotalID[1] * 1 * Random.Range(1.00f,4.00f));
        toAddItem.itemEquipped = false;
        toAddItem.charID = -1;
        toAddItem.itemID_forAccount = JsonItemsList.Count;

        JsonItemsList.Add(toAddItem);

        string jdata = JsonConvert.SerializeObject(JsonItemsList);
        jdata = JValue.Parse(jdata).ToString(Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/Json_AccountInfo/" + AccountID.ToString()+ "/ItemsInfo.json",jdata); //ID 추가해주기

        //그리고 itemPrefab에 매치되는 그림을 가져와주자.

        award_.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemDB/" + forPrefab + "/" + split_itemTotalID[2].ToString());
        award_.transform.GetChild(1).GetComponent<Text>().text = toAddItem.itemName;
    }
}
