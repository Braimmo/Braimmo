using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


public class Tuto_MissionChat : MonoBehaviour
{
    public Text missionChat;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager_CodeEditor.sentences.Count == 0)
        {
            missionChat.text = DialogueManager_CodeEditor.currentSentence;
        }

    }
}
