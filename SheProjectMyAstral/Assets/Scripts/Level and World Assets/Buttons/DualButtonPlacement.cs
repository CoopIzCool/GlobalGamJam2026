using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DualButtonPlacement :MonoBehaviour, I_InteractableObject
{
    #region 
    public bool ButtonPressed;
    [SerializeField] private int triggerCount = 0;
    #endregion
    public void Interact()
    {
        //throw new System.NotImplementedException();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Entered Trigger");
        Interact();
        if (triggerCount <= 0)
        {
            triggerCount = 1;
            ButtonPressed = true;
        }
        else
        {
            triggerCount++;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerCount--;
        if (triggerCount <= 0)
        {
            triggerCount = 0;
            ButtonPressed = false;
        }
    }
}
