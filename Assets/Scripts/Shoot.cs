using Fusion;
using UnityEngine;

public class Shoot : NetworkBehaviour
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
    private bool hasGun = true;
    private bool isShooting = false;

    void Awake()
    {
        ammoSystem = GetComponent<AmmoSystem>();
        timeBetweenShots = 1f / fireRate;
        nextShotTime = Time.time;
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
            nextShotTime = Time.time + timeBetweenShots;
        }
        isShooting = false;
    }

    public void SetHasGun(bool value)
    {
        hasGun = value;
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
        GameObject instBullet = Runner.Spawn(bullet, shootPoint.position, shootPoint.rotation).gameObject;
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
            GameObject instBullet = Runner.Spawn(bullet, shootPoint.position, spreadRotation).gameObject;
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
