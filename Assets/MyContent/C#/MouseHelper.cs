using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseHelper {

    public static void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void EnableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
