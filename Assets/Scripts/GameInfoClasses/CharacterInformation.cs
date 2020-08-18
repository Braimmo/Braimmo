using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation
{
    public int characterID;     public string name;
    public int level;           public float experience;
    public float health;        public float damage;
    public float weaponRange;   public float speed;
    public float defense;       public float criticalRate;
    public string prefabID;

    public CharacterInformation(){}


    public CharacterInformation(int characterID, string name, int level, float experience, float health, float damage, float weaponRange, float speed, float defense, float criticalRate, string prefabID)
    {
        this.characterID = characterID;
        this.name = name;
        this.level = level;
        this.experience = experience;
        this.health = health;
        this.damage = damage; 
        this.weaponRange = weaponRange;
        this.speed = speed;
        this.defense = defense;
        this.criticalRate = criticalRate;
        this.prefabID = prefabID;
    }
}

public class SelectedCharacterIDs
{
    public int characterID;
    public SelectedCharacterIDs()
    {
        this.characterID = 0;
    }

    public SelectedCharacterIDs(int characterID)
    {
        this.characterID = characterID;
    }
}
