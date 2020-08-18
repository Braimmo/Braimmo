using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//charqacterInfo + item 
public class CharacterInformation_item{
    public int ID_Character_Global;     public int ID_Character_Account;
    public string name;
    public int level;           public float experience;
    public float health_origin;        public float damage_origin;
    public float weaponRange_origin;   public float speed_origin;
    public float defense_origin;       public float criticalRate_origin;
    public float criticalDamage_origin;
    public float health_item;        public float damage_item;
    public float weaponRange_item;   public float speed_item;
    public float defense_item;       public float criticalRate_item;
    public float criticalDamage_item;
    public float health_total;        public float damage_total;
    public float weaponRange_total;   public float speed_total;
    public float defense_total;       public float criticalRate_total;
    public float criticalDamage_total;
    public int prefabID;     public float maxHealth;
    public List<string> equippedItemIds = new List<string>();
}


