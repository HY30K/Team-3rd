using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ManagerScene : MonoBehaviour
{
    //씬 불러오기
    public void SceneLoader(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
    //게임 종료
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
}
