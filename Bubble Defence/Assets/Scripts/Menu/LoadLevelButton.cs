using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
    [SerializeField] int levelNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        int max = PlayerPrefs.GetInt("level");
        Button btn = GetComponent<Button>();
        TMP_Text mytext = GetComponentInChildren<TMP_Text>();
        mytext.text = "" + levelNum;
        if(levelNum > max + 1)
            btn.interactable = false;
        else
            btn.interactable = true;

        btn.onClick.AddListener(LoadLevel);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(levelNum);
    }

    
}
