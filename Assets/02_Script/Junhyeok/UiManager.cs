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
        if (isOpen == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }
    private void OnEnable()
    {
        /*option.rectTransform.localScale = new Vector3(0, 0, 0);
        option.gameObject.SetActive(false);

        fadeout.color = new Color(0, 0, 0);
        fadeout.DOFade(0, 2f);*/
    }
    //씬 불러오기
    public void SceneLoader(string sceneName)
    {
        source.Play();

        fadeout.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
    //게임 종료
    public void Quit()
    {
        source.Play();
        //Application.Quit();
        Debug.Log("Quit");
    }
    //설정창 열기
    public void Option()
    {
        source.Play();
        hideMap.gameObject.SetActive(true);

        option.gameObject.SetActive(true);
        option.gameObject.transform.DOScale(1, 1).SetUpdate(true);
        isOpen = true;
    }
    //사운드설정 열기
    public void Sound()
    {
        source.Play();

        hideMap.gameObject.SetActive(true);

        sound.gameObject.SetActive(true);
    }
    //사운드설정 닫기
    public void Back()
    {
        source.Play();

        hideMap.gameObject.SetActive(false);

        sound.gameObject.SetActive(false);
    }
    //설정창 닫기 
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
