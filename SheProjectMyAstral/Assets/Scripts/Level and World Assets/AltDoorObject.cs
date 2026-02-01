using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltDoorObject : MonoBehaviour
{
    #region Fields
    [SerializeField] private float doorOpeningSpeed;
    [SerializeField] private Transform doorOpenLocation;
    public bool bDoorLocked { get; private set; }
    private bool _bDoorOpening = false;
    #endregion Fields

    #region Properties
    public bool SetUnlocked
    {
        set { bDoorLocked = value; }
    }
    #endregion Properties


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Door Opening Logic here
        //if (_bDoorOpening)
        //{
        //    if (Vector2.Distance(transform.position, doorOpenLocation.position) <= 0.5f)
        //    {
        //        _bDoorOpening = false;
        //    }
        //    else
        //    {
        //        transform.position = Vector3.Lerp(transform.position, doorOpenLocation.position, doorOpeningSpeed * Time.fixedDeltaTime);
        //    }
        //}
    }

    public bool OpenDoorCheck()
    {
        // Handle Door locked logic here
        if (bDoorLocked)
        {
            _bDoorOpening = false;
            Debug.Log("Door is locked");
            return false;
        }
        else
        {
            _bDoorOpening = true;
            return true;
        }
    }

    public void UnlockDoor()
    {
        if (bDoorLocked)
        {
            bDoorLocked = false;
            // just make the door disappear instead of "opening" it
            this.gameObject.SetActive(false);
        }
        //Insert any visuals for the door being unlocked here!
    }

}

