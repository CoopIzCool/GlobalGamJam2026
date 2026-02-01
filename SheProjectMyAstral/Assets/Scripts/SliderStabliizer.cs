using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderStabllizer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
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
        Debug.Log(volume);
        instance.value = Mathf.Pow(10, volume/20f);

    }
}
