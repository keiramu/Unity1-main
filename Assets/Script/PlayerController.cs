using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Look")]
    public Transform lookCamera;
    public float sensitivityX = 20;
    public float sensitivityY = 20;
    public float maxYrotation = 90;
    public float minYrotation = -90;
    private float currentYrotation;

    [Header("Inputs")]
    public InputActionAsset inputManager;

    public InputActionReference MoveAction;
    public InputActionReference LookAction;
    public InputActionReference ShootAction;
    public InputActionReference JumpAction;

    public Vector3 move;
    public Vector2 look;
    public bool shoot;
    public bool Jump;

    [Header("Movement")]
    public float MoveSpeed = 5;
    public float JumpImpulse = 5;
    public float Gravity = -20;

    CharacterController characterController;
    float ySpeed = 0;


    [Header("Shooting")]
    public float FireRate = 0.2f; //0.2
    public LayerMask ShootMask;
    public float shootDistance = 1000;
    public int damage = 5;
    public int maxAmmo = 6;
    public int currentAmmo = 6;

    //public Key rekey = Key.R;

    float NextShoot = 0;

    [Header("Effect")]
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
    public void Start()
    {
        inputManager.FindActionMap("Player").Enable();
        characterController = gameObject.GetComponent<CharacterController>();
        UpdateAmmo(0);

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
        GetInput();
        Rotate();
        Move();
        if (shoot == true)
        {
            Shoot();
        }


    }

    void GetInput()
    {
        move = MoveAction.action.ReadValue<Vector3>();
        look = LookAction.action.ReadValue<Vector2>();

        shoot = ShootAction.action.WasPressedThisFrame();
        Jump = JumpAction.action.WasPressedThisFrame();
    }
    void Move()
    {
        Vector3 movement = transform.TransformDirection(move);
        movement = movement * MoveSpeed;
        //ww
        if (!characterController.isGrounded)
        {
            movement = movement * MoveSpeed * notinairfloatt;
        }
        //ww
        



        if (Jump == true && characterController.isGrounded)
        {
            ySpeed = JumpImpulse;
        }
        else if (ySpeed > Gravity)
        {
            ySpeed += Gravity * Time.deltaTime;
        }
        movement.y = ySpeed;
        characterController.Move(movement * Time.deltaTime);
    }
    void Rotate()
    {
        transform.Rotate(transform.up, look.x * sensitivityX * Time.deltaTime);

        currentYrotation += look.y * sensitivityY * Time.deltaTime;
        currentYrotation = Mathf.Clamp(currentYrotation, minYrotation, maxYrotation);

        lookCamera.eulerAngles =
            new Vector3(-currentYrotation,
            lookCamera.eulerAngles.y,
            lookCamera.eulerAngles.z);


    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            return;
        }
        UpdateAmmo(-1);
        UpdateAmmoGUI();



        RaycastHit hit;
        bool didhit = Physics.Raycast(lookCamera.position,
            lookCamera.forward,
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
            Vector3 lineEnd = lookCamera.position + (lookCamera.forward * shootDistance);
            newBullet.GetComponent<BullletMover>().Initialise(lineEnd);
            print("missed");
        }
    }

    void UpdateAmmo(int value) 
    {
        currentAmmo += value;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        ammoGUI.UpdateText("Rifle", currentAmmo, maxAmmo);

    }
    //wwwwwwwwwww
    void UpdateAmmoGUI()
    {
        if (ammofill != null)
        {
            ammofill.UpdateAmmoBar(currentAmmo, maxAmmo);
        }
    }

    //wwwwwwwwwwwwwwww
    public bool ReceivePickup(Pickup pickup)
    {
        if (pickup is AmmoPickup)
        {
            return PickupAmmo(pickup);
        }
        else if (pickup is HealthPickup)
        {
            characterHealth health = GetComponent<characterHealth>();
            if (health != null)
            {
                return health.Heal((int)pickup.value);
            }
        
        }

        return false;

        

    }
    bool PickupAmmo(Pickup Ammo)
    {
        if(currentAmmo < maxAmmo)
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