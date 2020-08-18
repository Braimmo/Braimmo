/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_1 : CheckConditionData
{
    List<List<Condition_Specific>> conditionList_;
    List<Action_> data;
    MakeAlgorithmForGame_1 makeAlgorithmForGame;

    void Start()
    {
        makeAlgorithmForGame = new MakeAlgorithmForGame_1();
        conditionList_ = makeAlgorithmForGame.MakeAlgorithm(this.transform.GetChild(1).GetComponent<EachCharacterStat>().name);
        print(this.transform.GetChild(1).GetComponent<EachCharacterStat>().name + " mounted");
    }
    public bool CheckCondition_(int action)
    {
        return CheckCondition_Specific(action, conditionList_);
    }

}
*/