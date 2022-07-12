using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    [SerializeField] Image myImage;
    private void Start()
    {
        StartCoroutine(ColorChange());
    }
    private IEnumerator ColorChange()
    {
        while (true)
        {
            float r = Random.Range(0, 1f);
            float g = Random.Range(0, 1f);
            float b = Random.Range(0, 1f);
            Color newColor = new Color(r, g, b, 1f);

            myImage.color = newColor;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
