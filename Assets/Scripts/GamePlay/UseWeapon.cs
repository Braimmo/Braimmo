using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    CharacterMovement playa;
    public GameObject player;

    public float bulletForce = 1f;

    void Start()
    {
        player = this.transform.parent.gameObject;
        playa = player.GetComponent<CharacterMovement>();
    }
    public virtual IEnumerator Shoots()
    {
        yield return null;
        yield return new WaitForSeconds(0.3f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        float damage = this.transform.parent.GetChild(1).GetComponent<EachCharacterStat>().damage;
        bullet.GetComponent<Bullet>().setBullet(player,damage);
        bullet.transform.Rotate(90.0f,0,0);
    }
}

