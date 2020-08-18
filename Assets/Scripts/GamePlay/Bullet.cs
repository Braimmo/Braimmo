using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    //public float damage = 50f;
    public GameObject hitEffect;
    public GameObject player;
    private GameObject enemy;
    private float damage;
    Vector3 a = new Vector3(0,0,0);
    public float bulletSpeed;
    void Start()
    {
        if(this.transform.name.Contains("Melee"))
        {
            print("bullet is melee! setting to 100");
            bulletSpeed = 100.0f;
        }
        else
        {
            print("bullet is not melee. its name is " + this.transform.name);
            bulletSpeed = 10.0f;
        }
    }

    private void Update()
    {
        transform.position += transform.TransformDirection(Vector3.up) * bulletSpeed * Time.deltaTime;
    }

    public void setBullet(GameObject shooter, float importedDamage)
    {
        player = shooter;
        damage = importedDamage;
        //a = new Vector3(player.transform.rotation.x + 90.0f, player.transform.rotation.y, player.transform.rotation.z);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision on " + collision.collider.gameObject.name);

        try
        {
            if (collision.collider.gameObject.name != this.transform.GetComponent<Bullet>().player.name && collision.collider.gameObject.tag != "Bullet")
            {
                //Debug.Log("bullet hit");
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                effect.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                Destroy(effect, 1.0f);
                Destroy(gameObject);
                enemy = collision.collider.gameObject;
                //Debug.Log("enemy.tag = " + enemy.tag);

                if (enemy.tag == "Enemy")
                {
                    Image theImage = enemy.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
                    EachCharacterStat theStat = enemy.transform.GetChild(1).GetComponent<EachCharacterStat>();
                    enemy.transform.GetComponent<CharacterMovement>().isHit=true;
                    
                    float calcDamage;
                    if(theStat.health - damage < 0)
                    {
                        calcDamage = theStat.health;
                    }
                    else
                    {
                        calcDamage = damage;
                    }
                    float newHealthPercentage = (theStat.health - calcDamage)/theStat.maxHealth;
                    theStat.health -= calcDamage; 
                    DOTween.To(()=>theImage.fillAmount, x=> theImage.fillAmount = x, newHealthPercentage, 1.0f);
                }
                else if (enemy.tag == "Player")
                {
                    Image theImage = enemy.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
                    EachCharacterStat theStat = enemy.transform.GetChild(1).GetComponent<EachCharacterStat>();
                    enemy.transform.GetComponent<CharacterMovement>().isHit=true;
                    
                    float calcDamage;
                    if(theStat.health - damage < 0)
                    {
                        calcDamage = theStat.health;
                    }
                    else
                    {
                        calcDamage = damage;
                    }

                    float newHealthPercentage = (theStat.health - calcDamage)/theStat.maxHealth;
                    theStat.health -= calcDamage; 
                    DOTween.To(()=>theImage.fillAmount, x=> theImage.fillAmount = x, newHealthPercentage, 1.0f);
                }
            }
        }
        catch (System.Exception)
        {
            
        }
    }
}
