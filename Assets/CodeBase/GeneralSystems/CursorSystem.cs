using UnityEngine;

namespace CodeBase.GeneralSystems
{
    public class CursorSystem
    {
        public void ActivateGameMode()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ActivateDialogueMode()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}