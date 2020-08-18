using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryType : MonoBehaviour
{
    Inventory inventory;
    List<EquippableItem> items;

    bool[] clickType = new bool[4];


    public void Awake(){
        GameObject Inven = GameObject.Find("Inventory");
        inventory = Inven.GetComponent<Inventory>();
        
        items = inventory.items;
        clickType[0] = true;
        clickType[1] = false;
        clickType[2] = false;
        clickType[3] = false;
    }

    public void refreshItem(){
        items.Clear();
        items = inventory.items;
    }

    public void clickButton(int index){
        switch(index){
            case 0: clickType[0] = true; clickType[1] = false; clickType[2] = false; clickType[3] = false; break;
            case 1: clickType[0] = false; clickType[1] = true; clickType[2] = false; clickType[3] = false; break;
            case 2: clickType[0] = false; clickType[1] = false; clickType[2] = true; clickType[3] = false; break;
            case 3: clickType[0] = false; clickType[1] = false; clickType[2] = false; clickType[3] = true; break;
            default: break;
        }

        refreshUIByClickButton();
    }

    public void refreshUIByClickButton(){
        int index = 0;
        for(int i = 0; i < clickType.Length; i++){
            if(clickType[i]){
                index = i; 
                break;
            }
        }
        switch(index){
            case 0: clickToTalButton(); break;
            case 1: clickClothesButton(); break;
            case 2: clickWeaponButton(); break;
            case 3: clickItemButtons(); break;
            default: break;
        }
    }

    public void clickToTalButton(){
        inventory.refreshUI(items);
    }

    public void clickClothesButton(){
        List<EquippableItem> clothesItems = new List<EquippableItem>();
        for(int i = 0; i < items.Count; i++){
            EquippableItem item = (EquippableItem)items[i];
            if(item.EquipmentType == EquipmentType.Helmet || item.EquipmentType ==  EquipmentType.Chest || item.EquipmentType ==  EquipmentType.Gloves || item.EquipmentType ==  EquipmentType.Boots)
                clothesItems.Add(item);
        }
        inventory.refreshUI(clothesItems);
    }
    public void clickWeaponButton(){
        List<EquippableItem> weaponItems = new List<EquippableItem>();
        for(int i = 0; i < items.Count; i++){
            EquippableItem item = (EquippableItem)items[i];
            if(item.EquipmentType ==  EquipmentType.Weapon)
                weaponItems.Add(item);
        }
        inventory.refreshUI(weaponItems);
    }

    public void clickItemButtons(){
        List<EquippableItem> extraItems = new List<EquippableItem>();
        for(int i = 0; i < items.Count; i++){
            EquippableItem item = (EquippableItem)items[i];
            if(item.EquipmentType ==  EquipmentType.Potion || item.EquipmentType == EquipmentType.Accessory)
                extraItems.Add(item);
        }
        inventory.refreshUI(extraItems);
    }
}
