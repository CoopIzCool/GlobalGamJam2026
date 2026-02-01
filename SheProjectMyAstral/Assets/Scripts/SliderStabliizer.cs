using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class SliderStabllizer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;
    private Slider instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this.gameObject.GetComponent<Slider>();
        }
    }

    public void loadVolumes()
    {
        float volume;
        audioMixer.GetFloat(this.tag, out volume);
        instance.value = Mathf.Pow(10, volume/20f);
        SoundFXManager.instance.playSoundFxClip(clickSound, transform, 0.33f);
    }

    public void playClick(BaseEventData data)
    {
        SoundFXManager.instance.playSoundFxClip(hoverSound, transform, 1f);
    }

    public void OnDisable()
    {
        SoundFXManager.instance.playSoundFxClip(clickSound, transform, 0.33f);
    }
}
