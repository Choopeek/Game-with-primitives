using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundSpawnObject : MonoBehaviour
{
    private AudioSource playSound;
    public AudioClip soundToPlay;
    private float soundVolumeFull = 1.0f;

    private float destroyTimer = 1.0f;
    
    //this script plays a sound and destroys the gameObject. Good idea to get rid of hard coded values.
    void Start()
    {
        playSound = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        playSound.PlayOneShot(soundToPlay, soundVolumeFull);
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
    
}
