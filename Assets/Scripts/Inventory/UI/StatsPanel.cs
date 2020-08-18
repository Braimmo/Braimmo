using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour

{
    [SerializeField] StatDisplay[] statDisplays;
    Character character;
    private enum StatType{
        health, damage, weaponRange, speed, defense, criticalDamage, criticalRate
    }


    private void OnValidate(){
        statDisplays = GetComponentsInChildren<StatDisplay>();
        GameObject UIManager = GameObject.Find("UIManager");
        character = UIManager.GetComponent<Character>();

       // setStatNames();
    }


    public void setStatNames(){
        for(int i = 0; i < statDisplays.Length; i++){
            StatType type = (StatType)i;
            statDisplays[i].name =  type.ToString();
            statDisplays[i].NameText.text = type.ToString();
        }
    }

    public void setCharacterStatValuesUI(CharacterInformation_item characterStat){
        for(int i = 0; i < statDisplays.Length; i++){
            string name = statDisplays[i].name;
            switch(name){
                case "health": statDisplays[i].ValueText.text = characterStat.health_origin.ToString(); break;
                case "damage": statDisplays[i].ValueText.text = characterStat.damage_origin.ToString(); break;
                case "weaponRange": statDisplays[i].ValueText.text = characterStat.weaponRange_origin.ToString(); break;
                case "speed": statDisplays[i].ValueText.text = characterStat.speed_origin.ToString(); break;
                case "defense": statDisplays[i].ValueText.text = characterStat.defense_origin.ToString(); break;
                case "criticalDamage": statDisplays[i].ValueText.text = characterStat.criticalDamage_origin.ToString(); break;
                case "criticalRate": statDisplays[i].ValueText.text = characterStat.criticalRate_origin.ToString(); break;
                default: statDisplays[i].ValueText.text = "There are no cases"; break;
            }
        }
    }
    
}
