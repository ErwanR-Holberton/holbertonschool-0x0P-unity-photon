using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMouse : MonoBehaviour
{
    public bool isMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (isMenu)
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
            Cursor.visible = true; // Makes the cursor visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
            Cursor.visible = false; // Hides the cursor
        }
    }

}
