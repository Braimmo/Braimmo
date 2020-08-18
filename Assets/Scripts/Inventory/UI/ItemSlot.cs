using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{  
    [SerializeField] Image Image;
    private RectTransform rectTransform;
    
    //private Item _item; 
    private EquippableItem _item;
    public Character character;
    public int charID;
    public EquippableItem Item{
        get {return _item;}
        set{
            _item = value;

            if(_item == null){
                Image.gameObject.SetActive(false);
            }else{
                Image.gameObject.SetActive(true);
                Image.sprite = _item.icon;
                Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 1f);
                _item.itemID = value.itemID;
                _item.healthAdd = value.healthAdd;       _item.healthMulti = value.healthMulti;
                _item.damageAdd = value.damageAdd;       _item.damageMulti = value.damageMulti;
                _item.weaponRange = value.weaponRange;
                _item.speedAdd = value.speedAdd;         _item.speedMulti = value.speedMulti;
                _item.defenseAdd = value.defenseAdd;     _item.defenseMulti = value.defenseMulti;
                _item.criticalDamageAdd = value.criticalDamageAdd;      _item.criticalDamageMulti = value.criticalDamageMulti;
                _item.criticalRateAdd = value.criticalRateAdd;      _item.criticalRateMulti = value.criticalRateMulti;
                _item.itemName = value.itemName; 
            }
        }
    }
    protected virtual void Awake(){
        rectTransform = GetComponent<RectTransform>();
        if(Image == null)
            Image = GetComponent<Image>();
        GameObject UI = GameObject.Find("UIManager");
        character = UI.GetComponent<Character>();
        charID = UI.GetComponent<JsonLoadItem>().charID;
    }

}
