using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLocked : MonoBehaviour
{
    public Key tpkey = Key.V;
    public bool mouseLocked = false;
    public bool telep = false;
    public GameObject teleportpart;


    public Key mousekey = Key.G;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {


        if (Keyboard.current[mousekey].wasPressedThisFrame) //Keyboard.current.gKey.wasPressedThisFrame
        {
            mouseLocked = !mouseLocked;

            if (mouseLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }


            if (Keyboard.current[tpkey].wasPressedThisFrame)
            {
                if (teleportpart != null)
                {
                    CharacterController characc = GetComponent<CharacterController>();

                    if (characc != null)
                    {
                        characc.enabled = false;
                    }
                    if (characc != null)
                    {
                        characc.enabled = true;
                    }
                    Debug.Log("tp good");

                }
                else
                {
                    Debug.Log("v not pressed");
                }
            }

        }
    }
}
