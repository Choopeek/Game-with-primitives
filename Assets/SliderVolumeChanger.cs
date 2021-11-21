using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumeChanger : MonoBehaviour
{
    [SerializeField] string volumeParameter = "Master Volume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    private float multiplier = 30f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(AudionSliderValueChanged);
    }

    private void AudionSliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }
}
