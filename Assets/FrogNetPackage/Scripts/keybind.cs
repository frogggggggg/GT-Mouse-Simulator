using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class keybind : MonoBehaviour
{
    public UnityEngine.InputSystem.Key key = UnityEngine.InputSystem.Key.Escape;
    public GameObject enableObject;
    public bool enableMouse = false;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard[key].wasPressedThisFrame)
        {
            if (enableObject != null)
            {
                enableObject.SetActive(!enableObject.activeSelf);
                if (enableMouse) PlayerCamera.cursorLocked = !enableObject.activeSelf;
            }
        }
    }
}
