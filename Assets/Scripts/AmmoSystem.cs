using UnityEngine;
using System.Collections;
using System;

public class AmmoSystem : MonoBehaviour
{
    private GunData currentGunData;
    private Boolean isReloading = false;
    public int currentAmmo { get; private set; } = 0;
    public int totalAmmo { get; private set; }

    void Update()
    {
        //Checks if the player presses the R key and the player is not reloading and the current ammo is less than the magazine size
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < currentGunData.magazineSize)
        {
            //Starts the reload coroutine
            StartCoroutine(Reload());
        }
    }

    public void SetGunData(GunData newGunData)
    {
        currentGunData = newGunData;
        currentAmmo = newGunData.magazineSize;
        totalAmmo = newGunData.maxTotalAmmo;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        //Waits for the reload time before continuing
        yield return new WaitForSeconds(currentGunData.reloadTime);

        //Calculates the amount of ammo needed to fill the magazine
        int ammoNeeded = currentGunData.magazineSize - currentAmmo;
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
