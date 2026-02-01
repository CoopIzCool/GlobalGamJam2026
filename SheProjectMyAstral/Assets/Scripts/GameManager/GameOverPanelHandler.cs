using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float bufferTimeBeforePanelAppears;
    [SerializeField] private AudioClip AlertAudioClip;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.stateDictionary["GameOver"].EnteredEvent.AddListener(TriggerGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TriggerGameOver()
    {
        SoundFXManager.instance.playSoundFxClip(AlertAudioClip, gameObject.transform, 0.5f);
        print("Entered");
        StartCoroutine(GameOverSequence());
    }

    public IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(bufferTimeBeforePanelAppears);
        gameOverPanel.SetActive(true);
    }
}
