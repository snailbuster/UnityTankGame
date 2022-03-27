using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager bg;
    public AudioManager tankExplosion;
    public AudioManager shellExplosion;
    public AudioManager tankFire;

    public static AudioManager _audioManagerInstance;

    private void Awake()
    {
       // _audioManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bgAudioPlay()
    {
        
    }

    public void tankExplosionAudioPlay()
    {
        
    }

    public void shellExplosionAudioPlay()
    {
        
    }

    public void tankFireAudioPlay()
    {
        
    }


}
