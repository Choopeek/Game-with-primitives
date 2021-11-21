using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    float soundVolumeFull = 1f;    
    float soundVolumeQuarter = 0.25f;

    AudioSource enemySound;
    [SerializeField] AudioClip topAttackSound;
    [SerializeField] AudioClip columnAttackSound;
    [SerializeField] AudioClip waveAttackSound;
    [SerializeField] AudioClip burstAttackSound;
    [SerializeField] AudioClip TopPassingBySound;
    [SerializeField] AudioClip BurstPassingBySound;
    
    void Start()
    {
        enemySound = GetComponent<AudioSource>();
    }

    public void TopAttackSound()
    {
        enemySound.PlayOneShot(topAttackSound, soundVolumeQuarter);
    }

    public void WaveAttackSound()
    {
        enemySound.PlayOneShot(waveAttackSound, soundVolumeFull);
    }

    public void ColumnAttackSound()
    {
        enemySound.PlayOneShot(columnAttackSound, soundVolumeFull);
    }
    public void BurstAttackSound()
    {
        enemySound.PlayOneShot(burstAttackSound, soundVolumeFull);
    }

    public void TopPassingBySoundPlayback()
    {
        enemySound.PlayOneShot(TopPassingBySound, soundVolumeFull);
    }

    public void BurstPassingByPlayback()
    {
        enemySound.PlayOneShot(BurstPassingBySound, soundVolumeFull);
    }

}
