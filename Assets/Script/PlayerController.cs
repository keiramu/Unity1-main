using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float FireRate = 0.2f;
    public LayerMask ShootMask;
    float NextShoot = 0;

    [Header("Effect")]
    public GameObject hitEffect;
    public GameObject muzzleFlash;
    public Transform muzzle;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        inputManager.FindActionMap("Player").Enable();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Rotate();
        Move();
        if(shoot == true)
        {
            Shoot();
        }
        
    }

    void GetInput()
    {
        move = MoveAction.action.ReadValue<Vector3>();
        look = LookAction.action.ReadValue<Vector2>();

        shoot = ShootAction.action.WasCompletedThisFrame();
        Jump = JumpAction.action.WasCompletedThisFrame();
    }
    void Move()
    {
        Vector3 movement = transform.TransformDirection(move);
        movement = movement * MoveSpeed;
        

        
        if (Jump == true && characterController.isGrounded)
        {
            ySpeed = JumpImpulse;
        }
        else if(ySpeed > Gravity)
        movement.y = ySpeed;
        characterController.Move(movement*Time.deltaTime);
    }
    void Rotate()
    {
        transform.Rotate(transform.up, look.x * sensitivityX * Time.deltaTime);

        currentYrotation += look.y * sensitivityY * Time.deltaTime;
        currentYrotation = Mathf.Clamp(currentYrotation,minYrotation,maxYrotation);

        lookCamera.eulerAngles = 
            new Vector3(-currentYrotation, 
            lookCamera.eulerAngles.y, 
            lookCamera.eulerAngles.z);
        
    
    }

    void Shoot()
    {
        RaycastHit hit;
        bool didhit = Physics.Raycast(lookCamera.position,
            lookCamera.forward,
            out hit,
            1000,
            ShootMask);

        Instantiate(muzzleFlash, muzzle);

        if (didhit)
        {
            print($"shot {hit.collider.gameObject.name} at {hit.point}");
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
        else
        {
            print("missed");
        }
    }
}