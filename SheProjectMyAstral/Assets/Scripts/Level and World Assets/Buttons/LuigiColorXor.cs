using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MarioColorSwitchButton;


public class LuigiColorXor : MarioColorSwitchButton
{

    private void Start()
    {
        
    }

    // Start is called before the first frame update
    protected override void ButtonPressed()
{
    base.ButtonPressed();
        //If we were to have some sort of fancy transition logic to make things cool and lerp like and
        //Turn previous on assets off
        for (int i = 0; i < switchColorContainers[0].activeGameObjects.Length; i++)
        {
            
                switchColorContainers[0].activeGameObjects[i].SetActive(false);
            
            
        }
        for (int i = 0; i < switchColorContainers[1].activeGameObjects.Length; i++)
        {
            
                switchColorContainers[1].activeGameObjects[i].SetActive(true);
            
        }
        //Vice Versa

    }
}
