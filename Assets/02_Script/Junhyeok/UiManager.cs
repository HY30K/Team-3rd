using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image fadeout;
    [SerializeField] Image fadein;
    [SerializeField] Image option = null;
    [SerializeField] Image hideMap = null;
    [SerializeField] Image sound = null;
    [SerializeField] new GameObject audio = null;
    AudioSource source;
    bool isOpen = false;

    private void Awake()
    {
        source = audio.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isOpen == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Option();
        }
        else if (isOpen == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    private void OnEnable()
    {
        /*option.rectTransform.localScale = new Vector3(0, 0, 0);
        option.gameObject.SetActive(false);

        fadeout.color = new Color(0, 0, 0);
        fadeout.DOFade(0, 2f);*/
    }
    //�� �ҷ�����
    public void SceneLoader(string sceneName)
    {
        source.Play();

        fadeout.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
    //���� ����
    public void Quit()
    {
        source.Play();
        //Application.Quit();
        Debug.Log("Quit");
    }
    //����â ����
    public void Option()
    {
        source.Play();
        hideMap.gameObject.SetActive(true);

        option.gameObject.SetActive(true);
        option.gameObject.transform.DOScale(1, 1);
        isOpen = true;
    }
    //���弳�� ����
    public void Sound()
    {
        source.Play();

        hideMap.gameObject.SetActive(true);

        sound.gameObject.SetActive(true);
    }
    //���弳�� �ݱ�
    public void Back()
    {
        source.Play();

        hideMap.gameObject.SetActive(false);

        sound.gameObject.SetActive(false);
    }
    //����â �ݱ� 
    public void Close()
    {
        StartCoroutine(CloseWindow());
        isOpen = false;
    }
    IEnumerator CloseWindow()
    {
        source.Play();

        hideMap.gameObject.SetActive(false);

        option.gameObject.transform.DOScale(0, 0.6f);
        yield return new WaitForSeconds(0.4f);
        option.gameObject.SetActive(false);
    }
}
