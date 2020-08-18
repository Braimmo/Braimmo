using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inventory -> codeEditor 씬으로 넘어갈 때 캐릭터 넘겨주는 class
public class fromCharToCode : MonoBehaviour
{
    public static fromCharToCode instance;
    public int characterID;     public string name;
    public int level;           public float experience;
    public float health_origin;        public float damage_origin;
    public float weaponRange_origin;   public float speed_origin;
    public float defense_origin;       public float criticalRate_origin;
    public float health_item;        public float damage_item;
    public float weaponRange_item;   public float speed_item;
    public float defense_item;       public float criticalRate_item;
    public float health_total;        public float damage_total;
    public float weaponRange_total;   public float speed_total;
    public float defense_total;       public float criticalRate_total;
    public int prefabID;     public float maxHealth;
    public List<string> equippedItemIds = new List<string>();
    void Awake(){
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
}
