using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;

    bool isOver = false;

    [SerializeField] float animTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        winWindow.SetActive(false);
        winWindow.transform.localScale = new Vector3();
        loseWindow.SetActive(false);
        loseWindow.transform.localScale = new Vector3();

        EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
        spawner.OnLevelComplete += Win;

        Castle c = FindAnyObjectByType<Castle>();
        c.OnDamage += Lose;
    }

    void Win()
    {
        if (isOver == true) return;
        isOver = true;
        winWindow.SetActive(true);
        winWindow.transform.DOScale(1, animTime)
            .SetEase(Ease.OutBack).SetUpdate(true);
        Time.timeScale = 0.2f;
    }

    void SaveLevel()
    {
        int max = PlayerPrefs.GetInt("level");
        int index = SceneManager.GetActiveScene().buildIndex;

        if(index > max)
        {
            PlayerPrefs.SetInt("level", index);
        }
    }

    void Lose(float percent)
    {
        if (percent > 0.001f) return;
        if (isOver == true) return;
        isOver = true;
        loseWindow.SetActive(true);
        loseWindow.transform.DOScale(1, animTime)
            .SetEase(Ease.OutBack).SetUpdate(true);
        Time.timeScale = 0.2f;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void GoNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index+1);
    }

}
