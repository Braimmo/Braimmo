using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SavingPanelUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject savingPanelUI;
    JsonSaveItem jsonSaveItem;

    void Awake(){
        GameObject UIManager = GameObject.Find("UIManager");
        jsonSaveItem = UIManager.GetComponent<JsonSaveItem>();
    }
    public void goBackButton()
    {
        Debug.Log("press go back");
        savingPanelUI.SetActive(true);
    }

    public void Yes(){
        jsonSaveItem.saveEquipItem();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }

    public void No(){
        savingPanelUI.SetActive(false);
    }

     public void OnPointerDown(PointerEventData eventData){
        if(eventData.pointerEnter.tag == "goBackPanel"){
             Debug.Log("click the goBackPanel");
         }
     }  

  
}
