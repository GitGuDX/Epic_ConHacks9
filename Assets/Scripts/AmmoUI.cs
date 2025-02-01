using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    private AmmoSystem ammoSystem;

    void Start()
    {
        ammoSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<AmmoSystem>();
    }

    void Update()
    {
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = $"AMMO: {ammoSystem.currentAmmo}/{ammoSystem.totalAmmo}";
    }
}