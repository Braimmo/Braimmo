using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneManager1 : MonoBehaviour
{
    [SerializeField] GameObject savinPanelUI2;
    JsonSaveItem jsonSaveItem;

    void Awake(){
        GameObject UIManager = GameObject.Find("UIManager");
        jsonSaveItem = UIManager.GetComponent<JsonSaveItem>();
    }

    public void goCodeEditing(){
        savinPanelUI2.SetActive(true);
    }

    public void YesCodeEditing(){
        Debug.Log("press code edit");
        jsonSaveItem.saveEquipItem();
        UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor");
    }

    public void NoCodeEditing(){
        savinPanelUI2.SetActive(false);
    }

    public void goToCharacterScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");
    }

    public void goHomeScene(){        
        jsonSaveItem.saveEquipItem();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }
}
