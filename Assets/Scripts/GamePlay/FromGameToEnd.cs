using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromGameToEnd : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public float experience;
    public float money;
    public int gem;
    public string gameWin_Lose;
    public List<string> awardIDs;
}
