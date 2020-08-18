using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTutoManager : MonoBehaviour
{
    public void TutoFinishClick()
    {
        HomeSceneMenuControl.data.tutorialStage++;
        HomeSceneMenuControl.SaveTutorial();
    }
}
