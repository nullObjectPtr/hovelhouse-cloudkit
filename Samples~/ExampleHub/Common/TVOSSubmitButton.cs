using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TVOSSubmitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This is for TVOS remotes
        // Clicking on the touchpad will trigger the currently selected button
        if (Input.GetKeyDown(KeyCode.Joystick1Button14))
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                var button = selected.GetComponent<Button>();
                if (button != null)
                    button.onClick.Invoke();
            }
        }
    }
}
