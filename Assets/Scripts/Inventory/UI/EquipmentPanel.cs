using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform leftSlots;
    [SerializeField] Transform rightSlots;
    public EquipmentSlot[] equipmentSlots;
    StatModifier statModifier;
    JsonLoadItem jsonLoadItem;
    public int charID;

    private void Awake()
    {
        //equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        equipmentSlots = new EquipmentSlot[8];
        setEquipmentSlot();
        GameObject UIManager = GameObject.Find("UIManager");
        jsonLoadItem = UIManager.GetComponent<JsonLoadItem>();
        charID = jsonLoadItem.charID;
        statModifier = transform.parent.gameObject.transform.GetChild(2).GetChild(0).GetComponent<StatModifier>();
        refreshUI();
    }

    private void setEquipmentSlot()
    {
         EquipmentSlot[] slots = leftSlots.GetComponentsInChildren<EquipmentSlot>();
         for(int i = 0; i < 4; i++){
            equipmentSlots[i] = slots[i];
        }
        slots = rightSlots.GetComponentsInChildren<EquipmentSlot>();
        for(int i = 4; i < 8; i++){
            equipmentSlots[i] = slots[i-4];
        }
    }

    public void loadEquipmentPanelUI(){
        charID = jsonLoadItem.charID;
        for(int i = 0; i < equipmentSlots.Length; i++){
            equipmentSlots[i].Item = null;
        }
        refreshUI();
    }

    public void refreshUI()
    {
        print("EP: "+jsonLoadItem.allEquippedItems[charID].Count);
        for (int i = 0; i < jsonLoadItem.allEquippedItems[charID].Count; i++)
        {
            for (int j = 0; j < equipmentSlots.Length; j++)
            {
                if (jsonLoadItem.allEquippedItems[charID][i].EquipmentType == equipmentSlots[j].EquipmentType && equipmentSlots[j].Item == null)
                {
                    equipmentSlots[j].Item = jsonLoadItem.allEquippedItems[charID][i];
                    break;
                }
            }
        }
    }

    public bool addItem(EquippableItem item, out EquippableItem previousItem)
    {
        bool checkEmptySlot = false;
        int index = 0;
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                if (equipmentSlots[i].Item == null)
                {
                    checkEmptySlot = true;
                    index = i;
                    break;
                }
                else
                {
                    index = i;
                }
            }
        }
        if (checkEmptySlot)
        {
            //빈 슬롯이 있는 경우 
            equipmentSlots[index].Item = item;
            equipmentSlots[index].Item.charID = jsonLoadItem.charID;
            previousItem = null;
            statModifier.calculateItemStat(item, true);
            statModifier.refreshItemStatUI();
            return true;
        }
        else
        {
            //빈 슬롯이 없는 경우 마지막 슬롯의 전 아이템과 대체하라
            previousItem = (EquippableItem)equipmentSlots[index].Item;
            equipmentSlots[index].Item = item;
            equipmentSlots[index].Item.charID = jsonLoadItem.charID;
            statModifier.calculateItemStat(item, true);
            statModifier.calculateItemStat(previousItem, false);
            statModifier.refreshItemStatUI();
            return true;
        }
        previousItem = null;
        return false;

    }

    public bool removeItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                statModifier.calculateItemStat(item, false);
                statModifier.refreshItemStatUI();
                return true;
            }
        }
        return false;
    }
}
