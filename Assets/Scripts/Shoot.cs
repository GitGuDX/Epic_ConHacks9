using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float fireRate = 3f;
    public WeaponType weaponType = WeaponType.Rifle;
    public int pelletCount = 5;
    public float spreadAngle = 15f;
    private float timeBetweenShots;
    private float nextShotTime;
    private AmmoSystem ammoSystem;

    void Start()
    {
        ammoSystem = GetComponent<AmmoSystem>();
        timeBetweenShots = 1f / fireRate;
        nextShotTime = Time.time;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextShotTime && ammoSystem.CanShoot())
        {
            ShootBullet();
            ammoSystem.UseAmmo();
            nextShotTime = Time.time + timeBetweenShots;
        }

        // Example weapon switching with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponType = WeaponType.Rifle;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponType = WeaponType.Shotgun;
    }

     protected virtual void ShootBullet()
    {
        switch (weaponType)
        {
            case WeaponType.Rifle:
                ShootRifle();
                break;
            case WeaponType.Shotgun:
                ShootShotgun();
                break;
        }
    }

    private void ShootRifle()
    {
        GameObject instBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Vector3 direction = transform.forward;
        instBullet.GetComponent<Bullet>().move = direction;
        Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void ShootShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            float randomSpreadX = Random.Range(-spreadAngle, spreadAngle);
            float randomSpreadY = Random.Range(-spreadAngle, spreadAngle);
            
            Quaternion spreadRotation = Quaternion.Euler(randomSpreadX, randomSpreadY, 0) * transform.rotation;
            GameObject instBullet = Instantiate(bullet, shootPoint.position, spreadRotation);
            Vector3 direction = spreadRotation * Vector3.forward;
            instBullet.GetComponent<Bullet>().move = direction;
            Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}

public enum WeaponType
{
    Rifle,
    Shotgun
}