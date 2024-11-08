using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class WaveDisplay : MonoBehaviour
{
    Slider slider;
    TMP_Text infoText;
    Button activateButton;
    // Start is called before the first frame update
    void Start()
    {
        activateButton = GetComponentInChildren<Button>();
        slider = GetComponent<Slider>();
        infoText = GetComponentInChildren<TMP_Text>();
        infoText.text = "Начать";
        activateButton.onClick.AddListener(StartSpawning);  
    }

    void StartSpawning()
    {
        FindAnyObjectByType<EnemySpawner>().StartSpawning();
    }

    public void SetWave(string str, float duration)
    {
        slider.DOKill();
        infoText.text = str;
        slider.value = 0;
        slider.DOValue(1, duration).SetEase(Ease.Linear);
    }

    public void SetBuild(string str, float duration)
    {
        slider.DOKill();
        infoText.text = str;
        slider.value = 1;
        slider.DOValue(0, duration).SetEase(Ease.Linear);
    }

    
}
