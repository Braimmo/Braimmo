using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터가 가지고 있는 모든 아이템들은 jsonEquippableItem으로 저장됨
//0_0_0_0 (아이템타입_희귀도_아이템고유id_아이템prefab)
//모든 수치가 add, multiply로 나눠줘야 함
  public class JsonEquippableItem{
        public string itemID;
        public string itemName;
        public float healthAdd;     public float healthMulti;
        public float damageAdd;     public float damageMulti;
        public float weaponRange;        //public float weaponRangeMulti;
        public float speedAdd;              public float speedMulti;        
        public float defenseAdd;            public float defenseMulti;  
        public float criticalDamageAdd;     public float criticalDamageMulti;
        public float criticalRateAdd;       public float criticalRateMulti;
        public bool itemEquipped;           public int itemID_forAccount;
        public int charID = -1;

        public JsonEquippableItem(){}
        public JsonEquippableItem(string itemID, string itemName, float healthAdd, float healthMulti, float damageAdd, float damageMulti, 
        float weaponRange, /*float weaponRangeMulti, */float speedAdd, float speedMulti, float defenseAdd, float defenseMulti, 
        float criticalDamageAdd, float criticalDamageMulti, float criticalRateAdd, float criticalRateMulti, bool itemEquipped, int charID, int itemID_forAccount){
            this.itemID = itemID;
            this.itemName = itemName;
            this.healthAdd = healthAdd;                     this.healthMulti = healthMulti;
            this.damageAdd = damageAdd;                     this.damageMulti = damageMulti;         
            this.weaponRange = weaponRange;                 //this.weaponRangeMulti = weaponRangeMulti;
            this.speedAdd = speedAdd;                       this.speedMulti = speedMulti;
            this.defenseAdd = defenseAdd;                   this.defenseMulti = defenseMulti;
            this.criticalDamageAdd = criticalDamageAdd;     this.criticalDamageMulti = criticalDamageMulti;
            this.criticalRateAdd = criticalRateAdd;         this.criticalRateMulti = criticalRateMulti;
            this.itemEquipped = itemEquipped;
            this.charID = charID;
            this.itemID_forAccount = itemID_forAccount;
        }
    }
