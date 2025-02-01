using UnityEngine;
using System.Collections;

public class AmmoSystem : MonoBehaviour
{
    public int magazineSize = 30;
    public int currentAmmo;
    public int totalAmmo = 90;
    public float reloadTime = 2f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        //Checks if the player presses the R key and the player is not reloading and the current ammo is less than the magazine size
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < magazineSize)
        {
            //Starts the reload coroutine
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        //Waits for the reload time before continuing
        yield return new WaitForSeconds(reloadTime);

        //Calculates the amount of ammo needed to fill the magazine
        int ammoNeeded = magazineSize - currentAmmo;
        if (totalAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        //If the total ammo is less than the ammo needed, fill the magazine with the remaining ammo
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }

        isReloading = false;
    }

    //Checks if the player can shoot based off their ammo
    public bool CanShoot()
    {
        return currentAmmo > 0 && !isReloading;
    }

    //Decreases the current ammo by 1
    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }
}
