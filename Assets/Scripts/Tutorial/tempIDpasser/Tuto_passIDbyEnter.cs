using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tuto_passIDbyEnter : MonoBehaviour
{
    public InputField InputText;
    public void SendtheText()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<tempID>().theID = InputText.text;
    }

    public void goEditorPage()
    {
        if (HomeSceneMenuControl.data.tutorialStage == 5)
        {
            Tuto_Resetter tuto_Resetter = new Tuto_Resetter();
            tuto_Resetter.Startz();
        }


        if (HomeSceneMenuControl.data.tutorialStage < 17)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_CodeEditor", LoadSceneMode.Single);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor", LoadSceneMode.Single);

    }
}
