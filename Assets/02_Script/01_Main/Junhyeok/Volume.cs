using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private bool isChanged;

    private void Update()
    {
        if (!isChanged)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        }
    }

    public void VolumeChange()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void VolumeUpButtonDown()
    {
        isChanged = true;
        StartCoroutine("VolumeUp");
    }

    public void VolumeUpButtonUp()
    {
        StopCoroutine("VolumeUp");
        isChanged = false;
    }

    public void VolumeDownButtonDown()
    {
        isChanged = true;
        StartCoroutine("VolumeDown");
    }

    public void VolumeDownButtonUp()
    {
        StopCoroutine("VolumeDown");
        isChanged = false;
    }

    IEnumerator VolumeUp()
    {
        while (true)
        {
            volumeSlider.value += 0.01f;

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator VolumeDown()
    {
        while (true)
        {
            volumeSlider.value -= 0.01f;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
