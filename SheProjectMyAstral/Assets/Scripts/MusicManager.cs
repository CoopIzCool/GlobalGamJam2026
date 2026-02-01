using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;
    [SerializeField] AudioSource musicSource;
    // Start is called before the first frame update


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


    }
}
