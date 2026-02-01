using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderStabllizer : MonoBehaviour
{
    private float master;
    private float music;
    private float soundfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveVolumes()
    {
        GameObject[] masterVolume = GameObject.FindGameObjectsWithTag("slider_master");
        GameObject[] musicVolume = GameObject.FindGameObjectsWithTag("slider_music");
        GameObject[] soundfxVolume = GameObject.FindGameObjectsWithTag("slider_soundfx");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
