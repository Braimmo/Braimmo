using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_SceneManager1 : MonoBehaviour
{
    [SerializeField] GameObject savinPanelUI2;
    JsonSaveItem jsonSaveItem;
    UserInformation data_1;

    void Awake()
    {
        GameObject UIManager = GameObject.Find("UIManager");
        jsonSaveItem = UIManager.GetComponent<JsonSaveItem>();
    }

    public void goCodeEditing()
    {
        savinPanelUI2.SetActive(true);
    }

    public void YesCodeEditing()
    {
        Debug.Log("press code edit");
        jsonSaveItem.saveEquipItem();
        if (HomeSceneMenuControl.data.tutorialStage == 5)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor");
    }

    public void NoCodeEditing()
    {
        savinPanelUI2.SetActive(false);
    }

    public void goToCharacterScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");
    }


    public void goHomeScene()
    {
        Debug.Log("stage = " + HomeSceneMenuControl.data.tutorialStage);
        if (HomeSceneMenuControl.data.tutorialStage == 10)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            print("++ has happened");
            HomeSceneMenuControl.SaveTutorial();
        }
        jsonSaveItem.saveEquipItem();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_HomeScene");
    }



}
