using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
public class Tuto_MenuControl : UIManager
{
    GameObject DragDrop;
    void Start()
    {
        DragDrop = GameObject.Find("DragDrop");
 
}
    private void Update()
    {
        for (int i = 0; i < DragDrop.transform.childCount-1; i++)
        {
            if (string.Compare("useWeapon", DragDrop.transform.GetChild(i).name) == 0 && HomeSceneMenuControl.data.tutorialStage == 7)
            {
                HomeSceneMenuControl.data.tutorialStage++;
                HomeSceneMenuControl.SaveTutorial();
            }


            if (string.Compare("HPMT1001", DragDrop.transform.GetChild(i).name) == 0 && HomeSceneMenuControl.data.tutorialStage == 8)
            {
                HomeSceneMenuControl.data.tutorialStage++;
                HomeSceneMenuControl.SaveTutorial();
            }


            if (string.Compare("moveForward", DragDrop.transform.GetChild(i).name) == 0 && HomeSceneMenuControl.data.tutorialStage == 16)
            {
                HomeSceneMenuControl.data.tutorialStage++;
                HomeSceneMenuControl.SaveTutorial();
            }

            if (string.Compare("EnemyLT1002", DragDrop.transform.GetChild(i).name) == 0 && HomeSceneMenuControl.data.tutorialStage == 17)
            {
                HomeSceneMenuControl.data.tutorialStage++;
                HomeSceneMenuControl.SaveTutorial();
            }
        }
    }
    public void GoBack()
    {
            print("GoBack Clicked");
            if (goBackPopUpOpened == false)
            {
                goBackPopUp.SetActive(true);
            }
            else
            {
                goBackPopUpOpened = false;
                goBackPopUp.SetActive(false);
            }        
    }
    public void SaveUnchanged()
    {
        Debug.Log("saveUnchanged");
        if (HomeSceneMenuControl.data.tutorialStage == 9 || HomeSceneMenuControl.data.tutorialStage == 18)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_Inventory", LoadSceneMode.Single);

    }
}
