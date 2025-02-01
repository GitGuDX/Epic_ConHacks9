using UnityEngine;

public class ShotgunShoot : Shoot
{
    public int pelletCount = 5;
    public float spreadAngle = 15f;

    protected override void ShootBullet()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate random spread
            float randomSpreadX = Random.Range(-spreadAngle, spreadAngle);
            float randomSpreadY = Random.Range(-spreadAngle, spreadAngle);

            // Create rotation with spread
            Quaternion spreadRotation = Quaternion.Euler(randomSpreadX, randomSpreadY, 0) * transform.rotation;

            // Instantiate bullet
            GameObject instBullet = Instantiate(bullet, shootPoint.position, spreadRotation);
            Vector3 direction = spreadRotation * Vector3.forward;
            instBullet.GetComponent<Bullet>().move = direction;

            // Ignore collision with player
            Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}