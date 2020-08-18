using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] public GameObject savingPanelUI;
    [SerializeField] public Image charImage;

    public List<CharacterInformation_item> characterStats;
   // public CharacterInformation_item characterStat;
    public List<EquippableItem> unequippedItems;
    public List<EquippableItem> equippedItems;
    public List<List<EquippableItem>> allEquippedItems;
    public EquippableItem equippedItemStat; //stat modifier에서 씀
    

    StatsPanel statsPanel;
    StatModifier statModifier;
    CharacterLevel characterLevel;
    GameObject UIManager;
    public int charID;
    void Start()
    {
        UIManager = GameObject.Find("UIManager");
        unequippedItems = UIManager.GetComponent<JsonLoadItem>().unequippedItems;
        equippedItems = UIManager.GetComponent<JsonLoadItem>().equippedItems;
       // characterStat = UIManager.GetComponent<JsonLoadItem>().characterStat;  
        characterStats = UIManager.GetComponent<JsonLoadItem>().characterStats;
        print("character awake");
        equippedItemStat = new EquippableItem();

        GameObject Stat_Panel = GameObject.FindWithTag("StatsPanel");
        statsPanel = Stat_Panel.GetComponent<StatsPanel>();
        statModifier = Stat_Panel.GetComponent<StatModifier>();

        GameObject Character_Level = GameObject.FindWithTag("CharacterLevel");
        characterLevel = Character_Level.GetComponent<CharacterLevel>();

        allEquippedItems = UIManager.GetComponent<JsonLoadItem>().allEquippedItems;
        charID = UIManager.GetComponent<JsonLoadItem>().charID;
        charImage.sprite = Resources.Load<Sprite>("CharacterDB/" + charID);


        statsPanel.setCharacterStatValuesUI(characterStats[charID]);
        statModifier.setInitialItemStatUI(allEquippedItems[charID], characterStats[charID]);
        characterLevel.setCharacterLevelUI(characterStats[charID]);
       // loadCharacterUI();
    }

    public void loadCharacterUI()
    { 
        int charID = UIManager.GetComponent<JsonLoadItem>().charID;
        charImage.sprite = Resources.Load<Sprite>("CharacterDB/" + charID);
        print("character charID: "+charID);

        statsPanel.setCharacterStatValuesUI(characterStats[charID]);
        statModifier.setInitialItemStatUI(allEquippedItems[charID], characterStats[charID]);
        characterLevel.setCharacterLevelUI(characterStats[charID]);
    }

    public void FinishEquip()
    {
        statModifier.finalizeStatModify();
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.removeItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.addItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.addItem(previousItem);
                }
            }
            else
            {
                inventory.addItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (equipmentPanel.removeItem(item))
        {
            inventory.addItem(item);
        }
    }

}
