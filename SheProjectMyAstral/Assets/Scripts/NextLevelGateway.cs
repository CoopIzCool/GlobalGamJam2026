using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelGateway : MonoBehaviour
{
    #region Fields 
    [SerializeField] private string nextLevelName;
    [SerializeField] private AudioClip levelEnteredClip;
    #endregion Fields
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        //Ciruclar dependency? Maybe. Do I care at this moment? No.
        GameManager.Instance.stateDictionary["NextLevel"].EnteredEvent.AddListener(GoToNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoToNextLevel()
    {
        SoundFXManager.instance.playSoundFxClip(levelEnteredClip, gameObject.transform, 0.75f);
        BoilerPlate.Instance.GoToScene(nextLevelName,false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.TransitionTo("NextLevel");
    }
}
