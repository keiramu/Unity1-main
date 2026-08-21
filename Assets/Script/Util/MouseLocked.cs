using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLocked : MonoBehaviour
{
    public bool mouseLocked = false;

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
            

        }
    }
}
