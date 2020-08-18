using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class DialogueManager : MonoBehaviour, IPointerDownHandler
{
    public Text dialogueText;
   // public GameObject nextText;
    public string currentSentence;
    public static Queue<string> sentences;
    public CanvasGroup dialogueGroup;
    public float tpyingSpeed = 0.1f;
    TutorialText tutorialtext;
    UserInformation data;

    int tutorial_Stage;

    void Start()
    {
        /*
        data = new UserInformation();
        string JData = File.ReadAllText(Application.dataPath + "/Resources/UserInformation/userInformation.json");
        data = JsonConvert.DeserializeObject<UserInformation>(JData);
        tutorial_Stage = data.tutorialStage;
        */

        String[] sentences_;
        sentences_ = TutorialText.tutorialText[HomeSceneMenuControl.data.tutorialStage].ToArray();
                sentences = new Queue<string>();
                Ondialogue(sentences_); 
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            dialogueText.text = currentSentence;
        }
        else
        {
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
    }
 
    // Update is called once per frame
    void Update()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
            NextSentence();
    }

}


