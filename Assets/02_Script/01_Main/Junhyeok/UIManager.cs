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
    [SerializeField] Image inform = null;
    [SerializeField] Image key = null;
    [SerializeField] Image gauge = null;
    AudioSource source;
    bool isOpen = false;
    bool isOther = false;

    private void Awake()
    {
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isOpen == false && isOther == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Option();
        }
        else if (isOpen == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
        /*        if (isOther == true && Input.GetKeyDown(KeyCode.Escape))
                {
                    isOpen = false;
                    Back();
                }
                if (isOther == true)
                {
                    isOpen = false;
                }*/


        if (isOpen == true)
        {
            Time.timeScale = 0;
        }
        else if (isOpen == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        //else if (isOpen == false)
        //{
        //    Time.timeScale = 1;
        //}
        //else if (isOpen == false &&  isOther == false)
        //{
        //    Time.timeScale = 1;
        //}
    }
    //______________________________________________________________________________________
    //씬 불러오기
    public void SceneLoader(string sceneName)
    {
        source.Play();

        fadeout.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        }).SetUpdate(true);
    }
    //게임 종료
    public void Quit()
    {
        source.Play();
        //Application.Quit();
        Debug.Log("Quit");
    }
    //______________________________________________________________________________________
    //설정창 열기
    public void Option()
    {
        source.Play();
        hideMap.gameObject.SetActive(true);

        option.gameObject.SetActive(true);
        option.gameObject.transform.DOScale(1, 1).SetUpdate(true);
        isOpen = true;
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

        option.gameObject.transform.DOScale(0, 0.6f).SetUpdate(true);
        yield return new WaitForSeconds(0.4f);
        option.gameObject.SetActive(false);
    }
    //______________________________________________________________________________________
    //사운드설정 열기
    public void Sound()
    {
        source.Play();

        hideMap.gameObject.SetActive(true);

        sound.gameObject.SetActive(true);
        isOther = true;
    }
    //사운드설정 닫기
    public void Back()
    {
        source.Play();

        sound.gameObject.SetActive(false);
        if (isOpen == true)
        {
            hideMap.gameObject.SetActive(true);
        }
        else
        {
            hideMap.gameObject.SetActive(false);
        }
        isOther = false;
    }
    //______________________________________________________________________________________
    //정보 선택창 열기
    public void Inform()
    {
        source.Play();

        hideMap.gameObject.SetActive(true);

        inform.gameObject.SetActive(true);
        inform.gameObject.transform.DOScale(1, 1).SetUpdate(true);
    }
    //정보 선택창 닫기
    public void Close2()
    {
        StartCoroutine(CloseWindow2());
    }
    IEnumerator CloseWindow2()
    {
        source.Play();

        hideMap.gameObject.SetActive(false);

        inform.gameObject.transform.DOScale(0, 0.6f).SetUpdate(true);
        yield return new WaitForSeconds(0.4f);
        inform.gameObject.SetActive(false);
    }
    //______________________________________________________________________________________
    //조작키설정 열기
    public void Key()
    {
        source.Play();

        key.gameObject.SetActive(true);

        isOther = true;
    }
    //조작키설정 닫기
    public void CloseKey()
    {
        source.Play();

        key.gameObject.SetActive(false);

        isOther = false;
    }
    //______________________________________________________________________________________
    //UI안내 열기
    public void Gauge()
    {
        source.Play();

        gauge.gameObject.SetActive(true);

        isOther = true;
    }
    //UI안내 닫기
    public void CloseGauge()
    {
        source.Play();

        gauge.gameObject.SetActive(false);

        isOther = false;
    }
}
