using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButtonScript : MonoBehaviour, I_InteractableObject
{
    protected bool _buttonPressed = false;
    public virtual void ButtonPressed()
    {
        if(!_buttonPressed) 
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

}
