using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemExplainPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Text itemName;
    [SerializeField] Text itemExplain;
    [SerializeField] Button equipButton;
    Character character;
    GameObject outsideBox;

    private GameObject _explainPanel;
    public GameObject ExplainPanel
    {
        get { return _explainPanel; }
        set
        {
            _explainPanel = value;
            if (_explainPanel != null)
            {
                Debug.Log("explainPanel instnace set");
            }
        }
    }

    private bool _isEquipped;
    public bool IsEquipped
    {
        get { return _isEquipped; }
        set
        {
            _isEquipped = value;
            setEquipButtonText(_isEquipped);
        }
    }
    private EquippableItem _item;
    public EquippableItem Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                Debug.Log("explain panel set");
                setItemExplainText(Item);
            }
        }
    }

    void Awake()
    {
        GameObject UIManager = GameObject.Find("UIManager");
        character = UIManager.GetComponent<Character>();
        IsEquipped = false;
        outsideBox = transform.GetChild(0).gameObject;
    }

    public void setEquipButtonText(bool isEquipped)
    {
        if (!isEquipped)
        {
            equipButton.GetComponentInChildren<Text>().text = "장착";
        }
        else if (isEquipped)
        {
            equipButton.GetComponentInChildren<Text>().text = "해제";
        }
    }

    public void setItemExplainText(EquippableItem item)
    {
        Debug.Log("set item name and explaination");
        itemName.text = item.itemName;
        //설명 넣는칸
        float[] value = new float[13];
        value[0] = item.healthAdd; value[1] = item.healthMulti;
        value[2] = item.damageAdd; value[3] = item.damageMulti;
        value[4] = item.weaponRange;
        value[5] = item.speedAdd; value[6] = item.speedMulti;
        value[7] = item.defenseAdd; value[8] = item.defenseMulti;
        value[9] = item.criticalDamageAdd; value[10] = item.criticalDamageMulti;
        value[11] = item.criticalRateAdd; value[12] = item.criticalRateMulti;

        string[] typeName = new string[13];
        typeName[0] = "healthAdd"; typeName[1] = "healthMulti";
        typeName[2] = "damageAdd"; typeName[3] = "damageMulti";
        typeName[4] = "weaponRange";
        typeName[5] = "speedAdd"; typeName[6] = "speedMulti";
        typeName[7] = "defenseAdd"; typeName[8] = "defenseMulti";
        typeName[9] = "criticalDamageAdd"; typeName[10] = "criticalDamageMulti";
        typeName[11] = "criticalRateAdd"; typeName[12] = "criticalRateMulti";

        string explainText = "EquipmentType: " + item.EquipmentType + "\nItem Name: " + item.itemName;
        
        int j = 0;
        for(int i = 0; i < value.Length; i++, j++){
            if(value[i] > 0){
                if(j % 2 == 0) {
                    explainText += "\n" +typeName[i] + ":";
                    explainText += " " + value[i];
                }
                else if(j % 2 == 1){
                    explainText += "\n" +typeName[i] + ":";
                    explainText += " " + value[i];
                }
            }
        }

        // explainText = "EquipmentType: " + item.EquipmentType + "\nhealth: " + item.health + "\ndamage: " + item.damage + "\nweaponRange: " + item.weaponRange +
        // "\ndefense: " + item.defense + "\nspeed: " + item.speed + "\ncritical rate: " + item.criticalRate;
        itemExplain.text = explainText;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "OutsideBox")
        {
            Debug.Log("destroy");
            Destroy(ExplainPanel);
        }
    }

    public void clickEquipButton()
    {
        Debug.Log("equipButton");
        if (!IsEquipped)
        {
            character.Equip((EquippableItem)Item);
        }
        else if (IsEquipped)
        {
            print("unequip ");
            character.Unequip((EquippableItem)Item);
        }
        Destroy(ExplainPanel);
    }

    public void clickDeleteButton()
    {
        Debug.Log("deleteButton");
        Destroy(ExplainPanel);
    }

}
