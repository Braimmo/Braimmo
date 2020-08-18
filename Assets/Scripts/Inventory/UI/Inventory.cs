using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;



public class Inventory : MonoBehaviour
{
    public List<EquippableItem> items;
    public int temp = 222;
    [SerializeField] Transform itemsParent;
    [SerializeField] GameObject itemSlotsPanelPrefab;
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    JsonLoadItem jsonLoadItem;
    InventoryType inventoryType;
    List<ItemSlotsPanel> itemSlotsPanel;
    GameObject scrollPanel;
    GameObject UIManager;
    int column = 3;
    int row = 4;

    private void Awake()
    {
        UIManager = GameObject.Find("UIManager");
        jsonLoadItem = UIManager.GetComponent<JsonLoadItem>();
        GameObject InvenType = GameObject.Find("TypeButtons");
        inventoryType = InvenType.GetComponent<InventoryType>();
        scrollPanel = GameObject.Find("Scroll Panel");

        items = new List<EquippableItem>(jsonLoadItem.unequippedItems);
        Debug.Log("items num:" + items.Count);

        setItemSlots();

        refreshUI(items);
        Debug.Log("inventory awake");
    }

    public void loadInventoryUI()
    {
        jsonLoadItem = UIManager.GetComponent<JsonLoadItem>();
        itemSlots.Clear();
        items = new List<EquippableItem>(jsonLoadItem.unequippedItems);
        inventoryType.refreshItem();

        itemSlotsPanel.Clear();
        removeItemSlotsPanel();
        setItemSlots();
        refreshUI(items);

    }

    public void refreshUI(List<EquippableItem> items)
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Count; i++)
        { 
            itemSlots[i].Item = items[i];
            itemSlots[i].Item.charID = -1;
        }
        for (; i < itemSlots.Count; i++)
            itemSlots[i].Item = null;
    }

    public bool addItem(EquippableItem item)
    {
        if (isFull())
        {
            //return false;
            addNewItemSlotPanel();
        }

        items.Add(item);
        inventoryType.refreshUIByClickButton();
        return true;
    }

    public bool removeItem(EquippableItem item)
    {
        if (items.Remove(item))
        {
            inventoryType.refreshUIByClickButton();
            return true;
        }
        return false;
    }

    public bool isFull()
    {
        return items.Count >= itemSlots.Count;
    }

    public void setItemSlots()
    {
        if (itemsParent != null)
            itemSlotsPanel = itemsParent.GetComponentsInChildren<ItemSlotsPanel>().ToList();

        for (int i = 0; i < itemSlotsPanel.Count; i++)
        {
            for (int j = 0; j < column; j++)
                itemSlots.Add(itemSlotsPanel[i].itemSlots[j]);
        }

        int totalSlotNum = itemSlotsPanel.Count * 3;
        if (items.Count > totalSlotNum)
        {
            int leftItems = items.Count - column * row;
            int newPanel = leftItems / column;
            newPanel = (leftItems % column == 0) ? newPanel : newPanel + 1;
            for (int i = 0; i < newPanel; i++)
            {
                GameObject newItemSlotPanel = Instantiate(itemSlotsPanelPrefab, transform.position, transform.rotation);
                newItemSlotPanel.transform.SetParent(itemsParent);
                newItemSlotPanel.transform.localScale = new Vector3(1, 1, 1);
                ItemSlotsPanel isp = newItemSlotPanel.GetComponent<ItemSlotsPanel>();
                itemSlotsPanel.Add(isp);

                for (int j = 0; j < column; j++)
                    itemSlots.Add(isp.itemSlots[j]);
            }
            scrollPanel.GetComponent<ScrollRect>().enabled = true;
        }
    }

    public void removeItemSlotsPanel()
    {
        ItemSlotsPanel[] scripts = FindObjectsOfType<ItemSlotsPanel>();
        GameObject[] panels = new GameObject[scripts.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i] = scripts[i].gameObject;
        }

        int panelNumToDestroy = itemSlotsPanel.Count - 4;
        for (int i = 0; i < panelNumToDestroy; i++)
        {
            Destroy(panels[itemSlotsPanel.Count - i - 1]);
            int num = itemSlotsPanel.Count - 1 - i;
        }

    }

    public void addNewItemSlotPanel()
    {
        GameObject newItemSlotPanel = Instantiate(itemSlotsPanelPrefab, transform.position, transform.rotation);
        newItemSlotPanel.transform.SetParent(itemsParent);
        newItemSlotPanel.transform.localScale = new Vector3(1, 1, 1);
        ItemSlotsPanel isp = newItemSlotPanel.GetComponent<ItemSlotsPanel>();
        for (int i = 0; i < column; i++)
            itemSlots.Add(isp.itemSlots[i]);

        scrollPanel.GetComponent<ScrollRect>().enabled = true;
        inventoryType.refreshUIByClickButton();
    }

}
