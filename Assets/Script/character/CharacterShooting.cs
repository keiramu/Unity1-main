using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class CharacterShooting : MonoBehaviour
{
    [Header("Shooting")]
    public Transform sightOrigin;
    public float FireRate = 0.2f; //0.2
    public LayerMask ShootMask;
    public float shootDistance = 1000;
    public int damage = 5;

    [Header("Ammo")]
    public bool useAmmo = false;
    public int maxAmmo = 6;
    public int currentAmmo = 6;

    [Header("Effects")]

    public GameObject hitEffect;
    public GameObject muzzleFlash;
    public GameObject bulletEffect;
    public Transform muzzle;

    [Header("GUI")]
    public AmmoTextControl ammoGUI;
    public AmmoBLTgui ammofill;
    [UnitHeaderInspectable("TESTING")]
    public float notinairfloatt = 0.9f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (useAmmo)
        {
            UpdateAmmo(0);
            UpdateAmmoGUI();
        }
    }

    public void Shoot()
    {
        if (useAmmo && currentAmmo <= 0)
        {
            return;
        }
        if (useAmmo)
        {
            UpdateAmmo(-1);
            UpdateAmmoGUI();
            
        }


        RaycastHit hit;
        bool didhit = Physics.Raycast(sightOrigin.position,
            sightOrigin.forward,
            out hit,
            1000,
            ShootMask);

        Instantiate(muzzleFlash, muzzle);

        GameObject newBullet = Instantiate(bulletEffect, muzzle.position, Quaternion.identity);

        if (didhit)
        {
            characterHealth targetHealth = hit.collider.GetComponent<characterHealth>();
            if (targetHealth != null)
                targetHealth.takeDamage(damage);
            //print($"shot {hit.collider.gameObject.name} at {hit.point}");
            newBullet.GetComponent<BullletMover>().Initialise(hit.point);
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
        else
        {
            Vector3 lineEnd = sightOrigin.position + (sightOrigin.forward * shootDistance);
            newBullet.GetComponent<BullletMover>().Initialise(lineEnd);
            print("missed");
        }
    }

    void UpdateAmmo(int value)
    {
        currentAmmo += value;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        ammoGUI.UpdateText("Rifle ", currentAmmo, maxAmmo);

    }
    //AMMO FUNCTION
    void UpdateAmmoGUI()
    {
        if (ammofill != null)
        {
            ammofill.UpdateAmmoBar(currentAmmo, maxAmmo);
            print("reloaded");
        }
    }

    public bool PickupAmmo(Pickup Ammo)
    {
        if (currentAmmo < maxAmmo)
        {
            UpdateAmmo((int)Ammo.value);
            UpdateAmmoGUI();

            return true;
        }
        else
        {
            return false;

        }
    }
}
