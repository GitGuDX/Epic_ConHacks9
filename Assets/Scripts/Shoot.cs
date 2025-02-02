using Fusion;
using UnityEngine;

public class Shoot : NetworkBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    private GunData currentGunData;
    private float nextShotTime;
    private AmmoSystem ammoSystem;
    private bool hasGun = true;
    private bool isShooting = false;

    void Awake()
    {
        ammoSystem = GetComponent<AmmoSystem>();
    }

    void Update() {
        isShooting = Input.GetMouseButton(0);
    }

    public override void FixedUpdateNetwork()
    {
        if (isShooting && Time.time >= nextShotTime && ammoSystem.CanShoot() && hasGun)
        {
            ShootBullet();
            ammoSystem.UseAmmo();
            nextShotTime = Time.time + (1f / currentGunData.fireRate);
        }
        isShooting = false;
    }

  
    public void SetHasGun(bool value)
    {
        hasGun = value;
    }

     protected void ShootBullet()
    {
        switch (currentGunData.weaponType)
        {
            case WeaponType.Rifle:
                ShootRifle();
                break;
            case WeaponType.Shotgun:
                ShootShotgun();
                break;
            case WeaponType.Pistol:
                ShootPistol();
                break;
        }
    }

    private void ShootPistol()
    {
        // Small random spread for pistol
        float randomSpreadX = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);
        float randomSpreadY = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);
        
        Quaternion spreadRotation = Quaternion.Euler(randomSpreadX, randomSpreadY, 0) * shootPoint.rotation;
        var spawnedBullet = Runner.Spawn(bullet, shootPoint.position, spreadRotation);
        spawnedBullet.GetComponent<Bullet>().MovementDirection = spreadRotation * Vector3.forward;
    }

    private void ShootRifle()
    {
        float randomSpreadX = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);
        float randomSpreadY = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);

        var spawnedBullet = Runner.Spawn(bullet, shootPoint.position, shootPoint.rotation);
        spawnedBullet.GetComponent<Bullet>().MovementDirection = shootPoint.forward;
    }

    private void ShootShotgun()
    {
        for (int i = 0; i < currentGunData.pelletCount; i++)
        {
            float randomSpreadX = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);
            float randomSpreadY = Random.Range(-currentGunData.spreadAngle, currentGunData.spreadAngle);
            
            Quaternion spreadRotation = Quaternion.Euler(randomSpreadX, randomSpreadY, 0) * shootPoint.rotation;
            var spawnedBullet = Runner.Spawn(bullet, shootPoint.position, spreadRotation);
            spawnedBullet.GetComponent<Bullet>().MovementDirection = spreadRotation * Vector3.forward;
        }
    }

    public void SetGunData(GunData newGunData)
    {
        currentGunData = newGunData;
    }
}

