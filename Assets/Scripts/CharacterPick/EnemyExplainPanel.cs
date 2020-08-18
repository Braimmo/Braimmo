using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CharacterPick
{
    public class EnemyExplainPanel : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] public Text charName;
        [SerializeField] public GameObject health;
        [SerializeField] public GameObject damage;
        [SerializeField] public GameObject defense;
        [SerializeField] public GameObject weaponRange;
        [SerializeField] public GameObject speed;
        [SerializeField] public GameObject criticalDamage;
        [SerializeField] public GameObject criticalRate;
        [SerializeField] private GameObject me;

        public void setEnemyExplainPanel(EnemyInformation enemyInfo)
        {
            charName.text = enemyInfo.name;

            health.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.health.ToString();

            damage.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.damage.ToString();

            defense.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.defense.ToString();

            weaponRange.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.weaponRange.ToString();

            speed.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.speed.ToString();

            criticalDamage.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.criticalDamage.ToString();

            criticalRate.transform.GetChild(0).GetComponent<Text>().text = enemyInfo.criticalRate.ToString();

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
