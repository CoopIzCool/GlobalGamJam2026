using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{

    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;


    // Start is called before the first frame update
    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }

        GameObject[] music = GameObject.FindGameObjectsWithTag("AudioManager");
        if (music.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        
    }

    public void playSoundFxClip(AudioClip clip, Transform spawnPosition, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnPosition.position, Quaternion.identity);
        DontDestroyOnLoad(audioSource);

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void click()
    {
        playSoundFxClip(clickSound, transform, 1);
    }
}
