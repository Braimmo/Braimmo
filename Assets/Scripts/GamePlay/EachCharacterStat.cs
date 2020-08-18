using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EachCharacterStat : MonoBehaviour
{

    public float enemyNumInRange;
    public int isStunned;      public int isSlowed;
    public int buffedAtk;      public int debuffedAtk;
    public int buffedDef;      public int debuffedDef;    
    public int characterID;     public string name;
    public int level;           public float experience;
    public float health;        public float damage;
    public float weaponRange;   public float speed;
    public float defense;       public float criticalRate;
    public string prefabID;     public float maxHealth;
    public float aimedDistance; public int potionCount;
    public float healthPercentage;

    void Awake()
    {
        potionCount = 1;
    }
    void Update()
    {
        healthPercentage = health / maxHealth * 100;
        healthPercentage = Mathf.CeilToInt(healthPercentage);
        this.transform.parent.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthPercentage.ToString() + "%";
    }
}