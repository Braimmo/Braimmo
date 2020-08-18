using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckConditionData : MonoBehaviour
{
   private float oAuseWeapon, oAuseHealthPotion, oAmoveForward, oAmoveBackward, oAmoveLeft, oAmoveRight, oAmoveNorth, oAmoveSouth, oAmoveWest, oAmoveEast;
   private int[] theCases;
   private float[] oAZZZZ = new float[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
   public string[] conditionImageList = {""};
   public string[] conditionValueList = {""};
   public string[] conditionStateList = {""};
   // 순서
   //oAuseWeapon, oAuseHealthPotion, oAmoveForward, oAmoveBackward, oAmoveLeft, oAmoveRight, oAmoveNorth, oAmoveSouth, oAmoveWest, oAmoveEast

    public bool CheckCondition_Specific(int action, List<List<Condition_Specific>> conditionList_)
    {
        bool a = false;
        //Debug.Log("Checking for action "+action);
        if ( this.transform.GetComponent<MakeAlgorithmForGame_1>().cases[action-1] == 0)
            return false;

        a = action_(conditionList_[action]);

        if (a == true)//이게 뭘까..? 아무 쓸모가 없는 것 같은데?
        {
            oAZZZZ[action-1] = 1f;
            //여기서부턴 컨디션 읽어오는 걸로 쓰자.
        }
        else
        {
            oAZZZZ[action-1] = 0f;
            conditionImageList = new string[] {""};
            conditionValueList = new string[] {""};
            conditionStateList = new string[] {""};
        }
        return a;
    }
    public bool action_(List<Condition_Specific> conditionList_)
    {
        int b = conditionList_.Count; //b는 컨디션리스트 전체에 있어서 conditionList_[액션]이 가진 총 컨디션 숫자이다.
        conditionImageList = new string[b];
        conditionValueList = new string[b];
        bool a0 = false, a1 = false, a2 = false, a3 = false, a4 = false;
        bool a00 = true, a11 = true, a22 = true, a33 = true, a44 = true;

        for (int i = 0; i < b; i++) { //여기 b인 총 and된 조건들을 for 루프 한다.
            switch (conditionList_[i].branchNum)
            { //branchNum이라는 애가 0 1 2 3 4이기 때문에 내생각에는 저 case들은 총 조건 and가 된 리스트를 말하는 것 같다.
                case 0: //
                    a0 = CheckConditionValue(conditionList_[i].conditionName, conditionList_[i].conditionValue);
                    if (a0 == false)
                        a00 = false;
                    break;
                case 1:
                    a1 = CheckConditionValue(conditionList_[i].conditionName, conditionList_[i].conditionValue);
                    if (a1 == false)
                        a11 = false;
                    break;
                case 2:
                    a2 = CheckConditionValue(conditionList_[i].conditionName, conditionList_[i].conditionValue);
                    if (a2 == false)
                        a22 = false;
                    break;
                case 3:
                    a3 = CheckConditionValue(conditionList_[i].conditionName, conditionList_[i].conditionValue);
                    if (a3 == false)
                        a33 = false;
                    break;
                case 4:
                    a4 = CheckConditionValue(conditionList_[i].conditionName, conditionList_[i].conditionValue);
                    if (a4 == false)
                        a44 = false;
                    break;
                default:
                    break;

            }
            conditionImageList[i] = conditionList_[i].conditionPrefab;
            conditionValueList[i] = conditionList_[i].conditionValue.ToString();
            conditionStateList[i] = conditionList_[i].conditionValueState;
        }
        if ((a0 == true && a00 == true) || (a1 == true && a11 == true) || (a2 == true && a22 == true) || (a3 == true && a33 == true) || (a4 == true && a44 == true))
            return true;
        else
            return false;

    }



    public bool CheckConditionValue(string conditionName,float conditionValue)
    {
        bool b = false;
        EachCharacterStat stat = this.transform.GetChild(1).GetComponent<EachCharacterStat>();

        // 해야될 것
        //     DPS를 제대로 계산하기
        //         현재는 damage로 통일

    switch (conditionName)
    {
            case "HPMT":
                b = HPMT(stat.health, conditionValue);
                break;
            case "HPLT":
                b = HPLT(stat.health, conditionValue);
                break;
            case "DPSMT":
                b = DPSMT(stat.damage, conditionValue);
                break;
            case "DPSLT":
                b = DPSLT(stat.damage, conditionValue);
                break;
            case "EnemyMT":
                b = ENEMYMT(stat.enemyNumInRange, conditionValue);
                break;
            case "EnemyLT":
                b = ENEMYLT(stat.enemyNumInRange, conditionValue);
                break;
            case "IsStunned":
                b = IsStunned(stat.isStunned, conditionValue);
                break;
            case "BuffedAtk":
                b = BuffedAtk(stat.buffedAtk, conditionValue);
                break;
            case "DebuffedAtk":
                b = DebuffedAtk(stat.debuffedAtk, conditionValue);
                break;
            case "BuffedDef":
                b = BuffedDef(stat.buffedDef, conditionValue);
                break;
            case "DebuffedDef":
                b = DebuffedDef(stat.debuffedAtk, conditionValue);
                break;
            case "OAuseWeapon":
                b = OAuseWeapon(oAuseWeapon, conditionValue);
                break;
            case "OAuseHealthPotion":
                b = OAuseHealthPotion(oAuseHealthPotion, conditionValue);
                break;
            case "OAmoveForward":
                b = OAmoveForward(oAmoveForward, conditionValue);
                break;
            case "OAmoveBackward":
                b = OAmoveBackward(oAmoveBackward, conditionValue);
                break;
            case "OAmoveLeft":
                b = OAmoveLeft(oAmoveLeft, conditionValue);
                break;
            case "OAmoveRight":
                b = OAmoveRight(oAmoveRight, conditionValue);
                break;
            case "OAmoveNorth":
                b = OAmoveNorth(oAmoveNorth, conditionValue);
                break;
            case "OAmoveSouth":
                b = OAmoveSouth(oAmoveSouth, conditionValue);
                break;
            case "OAmoveWest":
                b = OAmoveWest(oAmoveWest, conditionValue);
                break;
            case "OAmoveEast":
                b = OAmoveEast(oAmoveEast, conditionValue);
                break;
            default:
                break;
        }
        return b;

    }

    public bool OAuseWeapon(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }

    public bool OAuseHealthPotion(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveForward(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
        {
            Debug.Log("move forward is false!");
            return false;
        }

    }
    public bool OAmoveBackward(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveLeft(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveRight(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveNorth(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveSouth(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveWest(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }
    public bool OAmoveEast(float character_value, float condition_value)
    {
        //condition_value == 1
        if (character_value == condition_value)
            return true;
        else
            return false;

    }

    public bool HPMT(float character_value, float condition_value)
    {
        if (character_value > condition_value)
            return true;
        else
            return false;
    }

    public bool HPLT(float character_value, float condition_value)
    {
        if (character_value < condition_value)
            return true;

        else
            return false;
    }

    public bool DPSMT(float character_value, float condition_value)
    {
        if (character_value > condition_value)
            return true;
        else
            return false;
    }

    public bool DPSLT(float character_value, float condition_value)
    {
        if (character_value < condition_value)
            return true;
        else
            return false;
    }



    public bool IsStunned(float character_value, float condition_value)
    {
        //character_value 값이 0이면 stunned된 상태
        if (character_value == condition_value)
            return true;
        else
            return false;
    }

    public bool BuffedAtk(float character_value, float condition_value)
    {
        //character_value 값이 0이면 stunned된 상태
        if (character_value == condition_value)
            return true;
        else
            return false;
    }


    public bool DebuffedAtk(float character_value, float condition_value)
    {
        //character_value 값이 0이면 stunned된 상태
        if (character_value == condition_value)
            return true;
        else
            return false;
    }


    public bool BuffedDef(float character_value, float condition_value)
    {
        //character_value 값이 0이면 stunned된 상태
        if (character_value == condition_value)
            return true;
        else
            return false;
    }

    public bool DebuffedDef(float character_value, float condition_value)
    {
        //character_value 값이 0이면 stunned된 상태
        if (character_value == condition_value)
            return true;
        else
            return false;
    }

    public bool ENEMYMT(float character_value, float condition_value)
    {
        if (character_value >= condition_value)
            return true;

        else
            return false;
    }


    public bool ENEMYLT(float character_value, float condition_value)
    {
        if (character_value <= condition_value)
        {
            return true;
        }

        else
            return false;
    }
    
}
