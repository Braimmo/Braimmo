using UnityEngine;

public class Tuto_SceneManager2 : MonoBehaviour
{
    UserInformation data_1;

    public void goCharacterScene()
    {
        Debug.Log("HomeSceneMenuControl.data.tutorialStage = " + HomeSceneMenuControl.data.tutorialStage);
        if (HomeSceneMenuControl.data.tutorialStage == 3 || HomeSceneMenuControl.data.tutorialStage == 10)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_Inventory");
    }

    public void goStoryMode()
    {
        if (HomeSceneMenuControl.data.tutorialStage == 11)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tuto_AgePickScene");
    }
}
