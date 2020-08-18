using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class passIDbyEnter_Kyle : MonoBehaviour
{
    public InputField InputText;
    public int AccountID = 0;//계정이름ㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁㅁ
    public void SendtheText()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<fromCharToCode>().name = InputText.text;
    }

    public void goEditorPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CodeEditor", LoadSceneMode.Single);
    }
    public void writeAccountInfo()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<tempID>().theID = "Json_AccountInfo/" + AccountID.ToString() + "/";
    }
    public void writeGameInfo()
    {
        GameObject.Find("charID_DontDestroy").GetComponent<tempID>().theID = "Json_GameInfo/";
    }
}
