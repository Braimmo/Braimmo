using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFromGamePlay
{
    public float experience;
    public float money;
    public int gem; 
    public string gameWin_Lose;
    public List<string> awardIDs;

    public DataFromGamePlay()
    {
        this.experience = 0;
        this.gem = 0;
        this.money = 0;
        this.gameWin_Lose = "";
        this.awardIDs = new List<string>();
    }

    public DataFromGamePlay(float experience, float money, int gem, string gameWin_Lose, List<string> awardIDs)
    {
        this.experience = experience;
        this.money = money;
        this.gem = gem;
        this.gameWin_Lose = gameWin_Lose;
        this.awardIDs = awardIDs;
    }

}
