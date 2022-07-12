using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class StartUI : MonoBehaviour
{
    [SerializeField] Image fadeout;
    [SerializeField] Image fadein;
    [SerializeField] Image option = null;
    [SerializeField] Image hideMap = null;
    [SerializeField] Button close;
    [SerializeField] Button Start;

    private void OnEnable()
    {
        option.rectTransform.localScale = new Vector3(0, 0, 0);
        option.gameObject.SetActive(false);

        fadeout.color = new Color(0, 0, 0);
        fadeout.DOFade(0, 2f);
    }
    //�� �ҷ�����
    public void SceneLoader(string sceneName)
    {
        fadeout.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
    //���� ����
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
    //����â ����
    public void Option()
    {
        hideMap.gameObject.SetActive(true);

        option.gameObject.SetActive(true);
        option.gameObject.transform.DOScale(1, 1);
    }
    //����â �ݱ� 
    public void Close()
    {
        StartCoroutine(CloseWindow());
    }
    IEnumerator CloseWindow()
    {
        hideMap.gameObject.SetActive(false);

        option.gameObject.transform.DOScale(0, 0.6f);
        yield return new WaitForSeconds(0.4f);
        option.gameObject.SetActive(false);
    }
}