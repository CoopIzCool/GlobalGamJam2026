using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButtonScript : MonoBehaviour, I_InteractableObject
{
    protected bool _buttonPressed = false;
    [SerializeField] private DualButtonPlacement dualButton;
    protected virtual void ButtonPressed()
    {
        if (!_buttonPressed && DualButtonPressed()) 
        {
            _buttonPressed = true;
            Debug.Log("Button Pressed");
        }
    }

    public void Interact()
    {
        ButtonPressed();
    }

    //Yeah this is weird abstraction. We may be doing weird 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact();
    }

    protected bool DualButtonPressed()
    {
        if(!dualButton)
        {
            return true;
        }
        return dualButton.ButtonPressed;
    }

}
