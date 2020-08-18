using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField]
    private Text name;
    void Awake()
    {
        name = GetComponent<Text>();
        EquipmentType equipType = gameObject.GetComponentInParent<EquipmentSlot>().EquipmentType;
        name.text = equipType.ToString();
    }
}
