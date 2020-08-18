using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Helmet, //0
    Chest,  //1
    Gloves, //2
    Boots,  //3
    Weapon, //4
    Potion, //5
    Accessory,  //6
}

public class EquippableItem 
{
    public string itemID;
    public float healthAdd;         public float healthMulti;
    public float damageAdd;         public float damageMulti;
    public float weaponRange;
    public float speedAdd;          public float speedMulti;
    public float defenseAdd;        public float defenseMulti;  
    public float criticalDamageAdd;   public float criticalDamageMulti;
    public float criticalRateAdd;   public float criticalRateMulti;
    public string itemName;
    public int charID = -1;    public int itemID_forAccount;
    public Sprite icon;

    [Space]
   public EquipmentType EquipmentType;
}
 