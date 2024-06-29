using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;

    public void Start()
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.Play();
    }
}
