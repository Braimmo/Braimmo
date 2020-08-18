using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InventoryMenuControl : MonoBehaviour
{
    public Button codeEditor;
     void Start()
    {
        if(HomeSceneMenuControl.data.tutorialStage == 4)
        codeEditor.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {  
        if (HomeSceneMenuControl.data.tutorialStage == 5)
        {
            if(DialogueManager_CharacterScene.sentences.Count == 0)
            codeEditor.interactable = true;
        }
    }
}
