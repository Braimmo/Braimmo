using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickAnimations : MonoBehaviour
{
    private GameObject toAnimate;
    public void SaveButtonClicked()
    {
        toAnimate = GameObject.Find("SaveButton");
        AnimationForClick(toAnimate);
    }

    public void DeleteButtonClicked()
    {
        toAnimate = GameObject.Find("InventoryButton");
        AnimationForClick(toAnimate);
    }

    public void AnimationForClick(GameObject ObjectToAnimate)
    {   
        var seq = LeanTween.sequence();
        seq.append( LeanTween.scale(ObjectToAnimate, new Vector2(1.5f,1.5f),0.3f) );
        seq.append( LeanTween.scale(ObjectToAnimate, new Vector2(1f,1f),0.1f) );
    }
}
