using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] Image charImage;
    public JsonSaveItem jsonSaveItem;
    public JsonLoadItem jsonLoadItem;
    public EquipmentPanel equipmentPanel;
    public Character character;
    public Inventory inventory;
    public int charId;
    private CharacterInformation_item _character;
    public CharacterInformation_item Character
    {
        get { return _character; }
        set
        {
            _character = value;
            if (_character == null)
            {
                charImage.color = new Color(charImage.color.r, charImage.color.g, charImage.color.b, 0f);
            }
            else
            {
                charImage.sprite = Resources.Load<Sprite>("CharacterDB/" + value.ID_Character_Global);
                charImage.color = new Color(charImage.color.r, charImage.color.g, charImage.color.b, 1f);
            }
        }
    }

    void Awake()
    {
        GameObject UIManager = GameObject.Find("UIManager");
        jsonSaveItem = UIManager.GetComponent<JsonSaveItem>();
        jsonLoadItem = UIManager.GetComponent<JsonLoadItem>();
        character = UIManager.GetComponent<Character>();

        GameObject Inventory = GameObject.Find("Inventory");
        inventory = Inventory.GetComponent<Inventory>();

        GameObject EP = GameObject.Find("Equipment Panel");
        equipmentPanel = EP.GetComponent<EquipmentPanel>();
    }

    public void pickCharacter()
    {
        
        jsonLoadItem.refreshCharStatInfo(Character.ID_Character_Global);
        jsonSaveItem.saveCharEquippedItem(Character.ID_Character_Global); 
        print("char id: " + jsonLoadItem.charID);
        character.loadCharacterUI();
        equipmentPanel.loadEquipmentPanelUI();
       

    }
}
