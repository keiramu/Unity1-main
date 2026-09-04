using UnityEngine;
using UnityEngine.UI;

public class AmmoBLTgui : MonoBehaviour
{
    public Image ammofill;


    public void UpdateAmmoBar(int currentAmmo, int MaxAmmo)
    {
        float percentage = (float)currentAmmo / (float)MaxAmmo;
        ammofill.fillAmount = percentage;
    }
}
