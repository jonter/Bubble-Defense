using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow;
    [SerializeField] float animTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        pauseWindow.SetActive(false);
        pauseWindow.transform.localScale = new Vector3();
    }

    public void MakePause()
    {
        if (pauseWindow.activeSelf == true) return;
        pauseWindow.SetActive(true);
        Time.timeScale = 0;
        pauseWindow.transform.DOScale(1, animTime).
            SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void Continue()
    {
        if (pauseWindow.activeSelf == false) return;
        pauseWindow.transform.DOScale(0, animTime).
            SetEase(Ease.OutBack).SetUpdate(true);
        StartCoroutine(ContinueCoroutine());
    }

    IEnumerator ContinueCoroutine()
    {
        yield return new WaitForSecondsRealtime(animTime);
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
    }
    

    
}
