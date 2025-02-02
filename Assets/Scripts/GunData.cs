using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapons/Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public GameObject gunPrefab;
    public WeaponType weaponType;
    public float fireRate;
    public int magazineSize;
    public int maxTotalAmmo;
    public float reloadTime;
    public float lifeTime;
    
    // Shotgun specific
    public int pelletCount;
    public float spreadAngle;
}

// Define the WeaponType enum
public enum WeaponType
{
    Pistol,
    Shotgun,
    Rifle,
    Sniper
}