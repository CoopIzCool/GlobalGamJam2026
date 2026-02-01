using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButtonScript : MonoBehaviour, I_InteractableObject
{
    protected bool _buttonPressed = false;
    [SerializeField] private DualButtonPlacement dualButton;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;
    protected virtual void ButtonPressed()
    {
        if (!_buttonPressed && DualButtonPressed()) 
        {
            _buttonPressed = true;
            if(defaultSprite != null)
            {
                StartCoroutine(ButtonPressedEffect());
            }
            Debug.Log("Button Pressed");
        }

        if(defaultSprite == null)
        {
            defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            pressedSprite = defaultSprite;
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

    IEnumerator ButtonPressedEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pressedSprite;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = defaultSprite;  
    }

}
