using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxSceneManager : MonoBehaviour
{
    public void onClickExitButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
    }
}
