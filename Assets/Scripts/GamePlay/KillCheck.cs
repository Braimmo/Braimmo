using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCheck : MonoBehaviour
{
    public bool alive;
    public List<string> char_targettingMe = new List<string>();
    void Awake()
    {
        alive = true;
        this.transform.tag = "Alive";
    }
    void Update()
    {
        if(this.transform.GetComponent<EachCharacterStat>().health <= 0)
        {
            if(alive == true)
            {
                this.transform.parent.GetComponent<CharacterMovement>().dieMotion();
                alive = false;
                this.transform.tag = "Dead";
            }
            Destroy(this.transform.parent.gameObject,3f);
            print("this character dies...");
        }
    }
}
