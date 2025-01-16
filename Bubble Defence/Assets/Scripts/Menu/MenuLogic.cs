using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] RectTransform mainPanel;
    [SerializeField] RectTransform levelsPanel;

    [SerializeField] float animTime = 0.5f;
    bool isAnim = false;

    [SerializeField] Button startButton;
    [SerializeField] Button levelsButton;
    [SerializeField] Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        levelsButton.onClick.AddListener(ShowLevelsPanel);
        backButton.onClick.AddListener(GoBack);

        levelsPanel.anchoredPosition = new Vector2(0, 1500);
        levelsPanel.gameObject.SetActive(true);

        int max = PlayerPrefs.GetInt("level");
        if(max > 0)
        {
            startButton.GetComponentInChildren<TMP_Text>().text = "Продолжить";
        }
    }

    void StartGame()
    {
        if (isAnim == true) return;
        isAnim = true;
        mainPanel.DOAnchorPosY(1500, animTime).SetEase(Ease.InBack);
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(animTime);
        int max = PlayerPrefs.GetInt("level");
        SceneManager.LoadScene(max+1);
    }

    IEnumerator ChangePanelCoroutine(RectTransform hide, RectTransform show)
    {
        isAnim = true;
        hide.DOAnchorPosY(1500, animTime).SetEase(Ease.InBack);
        yield return new WaitForSeconds(animTime);
        show.DOAnchorPosY(0, animTime).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(animTime);
        isAnim = false;
    }

    void ShowLevelsPanel()
    {
        if (isAnim == true) return;
        StartCoroutine(ChangePanelCoroutine(mainPanel, levelsPanel));
    }

    void GoBack()
    {
        if (isAnim == true) return;
        StartCoroutine(ChangePanelCoroutine(levelsPanel, mainPanel));
    }

    
}
