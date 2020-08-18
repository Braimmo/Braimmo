using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using UnityEngine.EventSystems;


namespace CharacterPick
{
    public class CharacterExplainPanel : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] public Text charName;
        [SerializeField] public Text charLevel;
        [SerializeField] public GameObject health;
        [SerializeField] public GameObject damage;
        [SerializeField] public GameObject defense;
        [SerializeField] public GameObject weaponRange;
        [SerializeField] public GameObject speed;
        [SerializeField] public GameObject criticalDamage;
        [SerializeField] public GameObject criticalRate;
        [SerializeField] public Button goCodeEditor;
        [SerializeField] private GameObject me;

        public void setCharExplainPanel(CharacterInformation_item charInfo)
        {   
            charName.text = charInfo.name;
            charLevel.text = "Lv " + charInfo.level.ToString();

            health.transform.GetChild(0).GetComponent<Text>().text = charInfo.health_origin.ToString();
            health.transform.GetChild(2).GetComponent<Text>().text = charInfo.health_item.ToString();

            damage.transform.GetChild(0).GetComponent<Text>().text = charInfo.damage_origin.ToString();
            damage.transform.GetChild(2).GetComponent<Text>().text = charInfo.damage_item.ToString();

            defense.transform.GetChild(0).GetComponent<Text>().text = charInfo.defense_origin.ToString();
            defense.transform.GetChild(2).GetComponent<Text>().text = charInfo.defense_item.ToString();

            weaponRange.transform.GetChild(0).GetComponent<Text>().text = charInfo.weaponRange_origin.ToString();
            weaponRange.transform.GetChild(2).GetComponent<Text>().text = charInfo.weaponRange_item.ToString();

            speed.transform.GetChild(0).GetComponent<Text>().text = charInfo.speed_origin.ToString();
            speed.transform.GetChild(2).GetComponent<Text>().text = charInfo.speed_item.ToString();

            criticalDamage.transform.GetChild(0).GetComponent<Text>().text = charInfo.criticalDamage_origin.ToString();
            criticalDamage.transform.GetChild(2).GetComponent<Text>().text = charInfo.criticalDamage_item.ToString();

            criticalRate.transform.GetChild(0).GetComponent<Text>().text = charInfo.criticalRate_origin.ToString();
            criticalRate.transform.GetChild(2).GetComponent<Text>().text = charInfo.criticalRate_item.ToString();
        }

        public void clickCodeEditorButton()
        {
            print("go code editor scene");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerEnter.name == "OutsideBox")
            {
                Debug.Log("destroy");
                Destroy(me);
            }
        }

    }
}
