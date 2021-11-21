using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropShipSoundController : MonoBehaviour
{
    AudioSource dropshipSounds;    

    [SerializeField] AudioClip dropshipEngine1;
    [SerializeField] AudioClip dropshipEngine2;

    float fullVolume = 1.0f;    
    
    void Start()
    {
        dropshipSounds = GetComponent<AudioSource>();
        
    }

    
    public void PlayEngineSound()
    {
        dropshipSounds = GetComponent<AudioSource>();
        dropshipSounds.PlayOneShot(dropshipEngine1, fullVolume);
        
    }

    
}
