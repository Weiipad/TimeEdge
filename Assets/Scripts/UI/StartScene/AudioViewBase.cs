using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioViewBase : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip Music;
    public float[] samples;
    public FFTWindow FFTWindowType;
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        if(audioPlayer == null)
            audioPlayer = gameObject.AddComponent<AudioSource>();
        if (audioPlayer.clip == null)
            audioPlayer.clip = Music;
        if (samples == null || samples.Length < 64)
            samples = new float[512];
    }

    void FixedUpdate()
    {
        audioPlayer.GetSpectrumData(samples, 0, FFTWindowType);
    }
}
