using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip Left;
    public AudioClip Right;

    AudioSource source;
    Coroutine soundSpawner = null;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartGenerating()
    {
        soundSpawner = StartCoroutine(generateSounds());
    }

    public void StopGenerating()
    {
        if(soundSpawner != null)
            StopCoroutine(soundSpawner);
    }

    IEnumerator generateSounds()
    {
        while(true)
        {
            switch(Random.Range(0, 2))
            {
                case 0:
                    source.clip = Left;
                    source.panStereo = -1;
                    break;
                case 1:
                    source.clip = Right;
                    source.panStereo = 1;
                    break;
            }
            source.Play();
            yield return new WaitForSeconds(Random.Range(30f, 60f));
        }
    }
}
