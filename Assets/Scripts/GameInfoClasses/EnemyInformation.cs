using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyInformation
{
    public int characterID;     public string name;
    public int level;           //public float experience;
    public float health;        public float damage;
    public float weaponRange;   public float speed;
    public float defense;       public float criticalRate;
    public float criticalDamage;  public string prefabID;

    public EnemyInformation()
    {
        this.characterID = 0;
        this.name = "";
        this.level = 0;
        this.health = 0;
        this.damage = 0; 
        this.weaponRange = 0;
        this.speed = 0;
        this.defense = 0;
        this.criticalRate = 0;
        this.criticalDamage = 0;
        this.prefabID = "";
    }

    public EnemyInformation(int characterID, string name, int level, float health, float damage, float weaponRange, float speed, float defense, float criticalRate, float criticalDamage, string prefabID)
    {
        this.characterID = characterID;
        this.name = name;
        this.level = level;
        this.health = health;
        this.damage = damage; 
        this.weaponRange = weaponRange;
        this.speed = speed;
        this.defense = defense;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;
        this.prefabID = prefabID;
    }
}