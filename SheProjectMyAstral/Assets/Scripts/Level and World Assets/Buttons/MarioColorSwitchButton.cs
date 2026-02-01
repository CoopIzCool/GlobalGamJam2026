using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MarioColorSwitchButton : BaseButtonScript
{
    [Serializable]
    protected struct SwtichColorContainer
    {
        public GameObject[] activeGameObjects;
        public Color SwitchColor;
        //Do we add colors and other shit here?  IDK lets find out future me
    }

    #region Fields
    [SerializeField] protected SwtichColorContainer[] switchColorContainers;
    protected int _buttonActivatedIndex = 0;
    #endregion Fields

    private void Start()
    {
        //Set all items of first color active and all other colors inactive
        for(int i = 0; i < switchColorContainers[0].activeGameObjects.Length; i++)
        {
            switchColorContainers[0].activeGameObjects[i].SetActive(true);
        }

        ChangeButtonColor();

        for (int i = 1; i < switchColorContainers.Length; i++)
        {
            for (int j = 0; j < switchColorContainers[i].activeGameObjects.Length; j++)
            {
                switchColorContainers[i].activeGameObjects[j].SetActive(false);
            }
        }
    }

    protected override void ButtonPressed()
    {
        base.ButtonPressed();
        //If we were to have some sort of fancy transition logic to make things cool and lerp like and
        //Turn previous on assets off
        for (int i = 0; i < switchColorContainers[_buttonActivatedIndex].activeGameObjects.Length; i++)
        {
            switchColorContainers[_buttonActivatedIndex].activeGameObjects[i].SetActive(false);
        }

        _buttonActivatedIndex = (_buttonActivatedIndex + 1) % switchColorContainers.Length;
        ChangeButtonColor();

        //Vice Versa
        for (int i = 0; i < switchColorContainers[_buttonActivatedIndex].activeGameObjects.Length; i++)
        {
            
            
            switchColorContainers[_buttonActivatedIndex].activeGameObjects[i].SetActive(true);
        }
    }

    protected void ChangeButtonColor()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = switchColorContainers[_buttonActivatedIndex].SwitchColor;
    }
}
