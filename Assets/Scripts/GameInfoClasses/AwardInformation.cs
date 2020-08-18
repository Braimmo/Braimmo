using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardInformation : MonoBehaviour
{
    
}

public class AwardDB_Item
{
    //public int itemType;
    //public int rarity;
    //public int itemUniqueID;
    //public int itemPrefabID;
    public string itemTotalID;
    public AwardDB_Item(string itemTotalID)
    {
        this.itemTotalID = itemTotalID;
    }
    public AwardDB_Item()
    {
        this.itemTotalID = "";
    }
}

public class AwardDB_Action
{

}

public class AwardDB_Condition
{
    public int conditionID;
    public int range_min;
    public int range_max;
    public string upDownOn;
    public string conditionPrefab;
    public AwardDB_Condition(int conditionID, int range_min, int range_max, string upDownOn, string conditionPrefab)
    {
        this.conditionID = conditionID;
        this.range_min = range_min;
        this.range_max = range_max;
        this.upDownOn = upDownOn;
        this.conditionPrefab = conditionPrefab;
    }
    public AwardDB_Condition()
    {
        this.conditionID = 0;
        this.range_min = 0;
        this.range_max = 100;
        this.upDownOn = "";
    }
}

public class AwardDB_MoneyGemExp
{

}