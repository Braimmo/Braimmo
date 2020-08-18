using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class passIDbyEnter : MonoBehaviour
{
    public InputField InputText;
    public void SendtheText()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<tempID>().theID = InputText.text;
    }

    public void goEditorPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_CodeEditor", LoadSceneMode.Single);

    }
}