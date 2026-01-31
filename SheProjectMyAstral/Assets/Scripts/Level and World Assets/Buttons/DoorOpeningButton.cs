using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningButton : BaseButtonScript
{
    #region Fields
    [SerializeField] private DoorObject doorAnswer;
    #endregion 

    public override void ButtonPressed()
    {
        base.ButtonPressed();
        //Add door open command
        _buttonPressed = doorAnswer.OpenDoorCheck();
    }
}
