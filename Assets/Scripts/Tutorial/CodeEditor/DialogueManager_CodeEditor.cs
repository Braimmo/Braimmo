using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class DialogueManager_CodeEditor : MonoBehaviour, IPointerDownHandler
{
    public Text dialogueText;
    // public GameObject nextText;
    public static string currentSentence;
    public static Queue<string> sentences;
    public CanvasGroup dialogueGroup;
    public float tpyingSpeed = 0.1f;
    private bool istyping;
    TutorialText tutorialtext;
    UserInformation data;

    int tutorial_Stage;

    void Start()
    {
        tutorial_Stage = HomeSceneMenuControl.data.tutorialStage;

        String[] sentences_;
        sentences_ = TutorialText.tutorialText[tutorial_Stage].ToArray();

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
            istyping = true;
            dialogueText.text = currentSentence;
        }
        else
        {

            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (tutorial_Stage != HomeSceneMenuControl.data.tutorialStage)
        {
            this.Start();
        }

        if (dialogueText.text.Equals(currentSentence))
        {
            //     nextText.SetActive(true);
            istyping = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!istyping)
            NextSentence();
    }
}


/*
 IEnumerator Typing(string line)
 {
     dialogueText.text = "";

     foreach (char letter in line.ToCharArray())
     {
         dialogueText.text += letter;
         yield return new WaitForSeconds(tpyingSpeed);
     }
 }*/
