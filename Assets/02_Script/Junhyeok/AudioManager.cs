using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource[] sfxs;
    private AudioSource bgm;

    public AudioSource[] SFXS => sfxs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        sfxs = GetComponents<AudioSource>();
    }

    private void Update()
    {
        bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        for (int i = 0; i < AudioManager.instance.SFXS.Length; i++)
        {
            sfxs[i].volume = PlayerPrefs.GetFloat("Volume", 1);
        }

        bgm.volume = PlayerPrefs.GetFloat("Volume", 1) * 0.3f;
    }
}
