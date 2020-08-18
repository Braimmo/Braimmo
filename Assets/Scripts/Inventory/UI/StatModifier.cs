using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatModifier : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    Character character;
    EquippableItem equippedItemStat;
    CharacterInformation_item characterStat;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        GameObject UIManager = GameObject.Find("UIManager");
        character = UIManager.GetComponent<Character>();
    }
    public void Awake()
    {   
        characterStat = new CharacterInformation_item();
        equippedItemStat = new EquippableItem();
        Debug.Log("stat modifier");
    }
    public void finalizeStatModify()
    {
        character.equippedItemStat = equippedItemStat;
    }
    public void calculateItemStat(EquippableItem item, bool isEquipped)
    {
        if (isEquipped)
        {
            equippedItemStat.healthAdd += item.healthAdd;
            equippedItemStat.healthMulti += characterStat.health_origin * (item.healthAdd / 100 );

            equippedItemStat.damageAdd += item.damageAdd;
            equippedItemStat.damageMulti += characterStat.damage_origin * (item.damageMulti / 100);

            equippedItemStat.weaponRange += item.weaponRange;

            equippedItemStat.speedAdd += item.speedAdd; 
            equippedItemStat.speedMulti += characterStat.speed_origin * (item.speedMulti / 100);

            equippedItemStat.defenseAdd += item.defenseAdd;
            equippedItemStat.defenseMulti += characterStat.defense_origin * (item.defenseMulti / 100);

            equippedItemStat.criticalDamageAdd += item.criticalDamageAdd;
            equippedItemStat.criticalDamageMulti += characterStat.criticalDamage_origin * (item.criticalDamageMulti / 100 );

            equippedItemStat.criticalRateAdd += item.criticalRateAdd;
            equippedItemStat.criticalRateMulti += characterStat.criticalRate_origin * (item.criticalRateMulti / 100);
        }
        else
        {
            equippedItemStat.healthAdd -= item.healthAdd;
            equippedItemStat.healthMulti -= characterStat.health_origin * (item.healthAdd / 100 );

            equippedItemStat.damageAdd -= item.damageAdd;
            equippedItemStat.damageMulti -= characterStat.damage_origin * (item.damageMulti / 100);

            equippedItemStat.weaponRange -= item.weaponRange;

            equippedItemStat.speedAdd -= item.speedAdd; 
            equippedItemStat.speedMulti -= characterStat.speed_origin * (item.speedMulti / 100);

            equippedItemStat.defenseAdd -= item.defenseAdd;
            equippedItemStat.defenseMulti -= characterStat.defense_origin * (item.defenseMulti / 100);

            equippedItemStat.criticalDamageAdd -= item.criticalDamageAdd;
            equippedItemStat.criticalDamageMulti -= characterStat.criticalDamage_origin * (item.criticalDamageMulti / 100 );

            equippedItemStat.criticalRateAdd -= item.criticalRateAdd;
            equippedItemStat.criticalRateMulti -= characterStat.criticalRate_origin * (item.criticalRateMulti / 100);
        }
    }

    public void setInitialItemStatUI(List<EquippableItem> equippedItems, CharacterInformation_item characterStat)
    {   
        equippedItemStat = new EquippableItem();
        this.characterStat = characterStat;
        Debug.Log("set initialize");
        for (int i = 0; i < equippedItems.Count; i++)
        {
            equippedItemStat.healthAdd += equippedItems[i].healthAdd;
            equippedItemStat.healthMulti += characterStat.health_origin * (equippedItems[i].healthAdd / 100 );

            equippedItemStat.damageAdd += equippedItems[i].damageAdd;
            equippedItemStat.damageMulti += characterStat.damage_origin * (equippedItems[i].damageMulti / 100);

            equippedItemStat.weaponRange += equippedItems[i].weaponRange;

            equippedItemStat.speedAdd += equippedItems[i].speedAdd; 
            equippedItemStat.speedMulti += characterStat.speed_origin * (equippedItems[i].speedMulti / 100);

            equippedItemStat.defenseAdd += equippedItems[i].defenseAdd;
            equippedItemStat.defenseMulti += characterStat.defense_origin * (equippedItems[i].defenseMulti / 100);

            equippedItemStat.criticalDamageAdd += equippedItems[i].criticalDamageAdd;
            equippedItemStat.criticalDamageMulti += characterStat.criticalDamage_origin * (equippedItems[i].criticalDamageMulti / 100 );

            equippedItemStat.criticalRateAdd += equippedItems[i].criticalRateAdd;
            equippedItemStat.criticalRateMulti += characterStat.criticalRate_origin * (equippedItems[i].criticalRateMulti / 100);
        }
        for (int i = 0; i < statDisplays.Length; i++)
        {
            string name = statDisplays[i].name;
            switch (name)
            {
                case "health": statDisplays[i].ItemStatText.text =  (equippedItemStat.healthAdd + equippedItemStat.healthMulti).ToString(); break;
                case "damage": statDisplays[i].ItemStatText.text =  (equippedItemStat.damageAdd+ equippedItemStat.damageMulti).ToString(); break;
                case "weaponRange": statDisplays[i].ItemStatText.text =  equippedItemStat.weaponRange.ToString(); break;
                case "speed": statDisplays[i].ItemStatText.text =  (equippedItemStat.speedAdd + equippedItemStat.speedMulti).ToString(); break;
                case "defense": statDisplays[i].ItemStatText.text =  (equippedItemStat.defenseAdd + equippedItemStat.defenseMulti).ToString(); break;
                case "criticalDamage": statDisplays[i].ItemStatText.text =  (equippedItemStat.criticalDamageAdd + equippedItemStat.criticalDamageMulti).ToString(); break;
                case "criticalRate": statDisplays[i].ItemStatText.text =  (equippedItemStat.criticalRateAdd + equippedItemStat.criticalRateMulti).ToString(); break;
                default: statDisplays[i].ItemStatText.text = "There are no cases"; break;
            }
        }


    }
    public void refreshItemStatUI()
    {
        for (int i = 0; i < statDisplays.Length; i++)
        {
            string name = statDisplays[i].name;
            switch (name)
            {
                 case "health": statDisplays[i].ItemStatText.text =  (equippedItemStat.healthAdd + equippedItemStat.healthMulti).ToString(); break;
                case "damage": statDisplays[i].ItemStatText.text =  (equippedItemStat.damageAdd+ equippedItemStat.damageMulti).ToString(); break;
                case "weaponRange": statDisplays[i].ItemStatText.text =  equippedItemStat.weaponRange.ToString(); break;
                case "speed": statDisplays[i].ItemStatText.text =  (equippedItemStat.speedAdd + equippedItemStat.speedMulti).ToString(); break;
                case "defense": statDisplays[i].ItemStatText.text =  (equippedItemStat.defenseAdd + equippedItemStat.defenseMulti).ToString(); break;
                case "criticalDamage": statDisplays[i].ItemStatText.text =  (equippedItemStat.criticalDamageAdd + equippedItemStat.criticalDamageMulti).ToString(); break;
                case "criticalRate": statDisplays[i].ItemStatText.text =  (equippedItemStat.criticalRateAdd + equippedItemStat.criticalRateMulti).ToString(); break;
                default: statDisplays[i].ItemStatText.text = "There are no cases"; break;
            }
        }
    }

}
