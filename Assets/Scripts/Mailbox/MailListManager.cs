using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailListManager : MonoBehaviour
{
    public static int siblingIndex;
    public void onClickMaillist(){
        siblingIndex = this.transform.GetSiblingIndex();
        Debug.Log("siblingIndex = "+siblingIndex);

    }

}
