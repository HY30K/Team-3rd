using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ManagerScene : MonoBehaviour
{
    //�� �ҷ�����
    public void SceneLoader(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
    //���� ����
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
}
