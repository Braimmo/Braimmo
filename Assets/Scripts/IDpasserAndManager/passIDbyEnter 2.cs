﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class passIDbyEnter2 : MonoBehaviour
{
    public InputField InputText;
    public void SendtheText()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<tempID>().theID = InputText.text;
    }

    public void goEditorPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor", LoadSceneMode.Single);
    }
}