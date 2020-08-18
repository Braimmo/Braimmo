using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotsPanel : MonoBehaviour
{
    public ItemSlot[] itemSlots;
    void OnValidate(){
        itemSlots = this.transform.GetComponentsInChildren<ItemSlot>();
//        Debug.Log("itemSlots: "+itemSlots.Length);
    }
}
