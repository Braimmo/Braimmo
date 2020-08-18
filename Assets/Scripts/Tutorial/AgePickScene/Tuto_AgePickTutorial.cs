using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_AgePickTutorial : MonoBehaviour
{
    public void TutorialManage()
    {
        if (HomeSceneMenuControl.data.tutorialStage == 12)
        {
            HomeSceneMenuControl.data.tutorialStage++;
            HomeSceneMenuControl.SaveTutorial();
        }
    }
}
