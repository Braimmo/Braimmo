 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;

    private Image image;
  
    protected override void Awake(){
        base.Awake();
        gameObject.name = EquipmentType.ToString() + "Slots";
        image = GetComponent<Image>();
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

   
}
    