using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButtonScript : MonoBehaviour, I_InteractableObject
{
    protected bool _buttonPressed;
    public virtual void ButtonPressed()
    {
        _buttonPressed = true;
        Debug.Log("Button Pressed");
    }

    public void Interact()
    {
        ButtonPressed();
    }

    //Yeah this is weird abstraction. We may be doing weird 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Interact();
    }
}
