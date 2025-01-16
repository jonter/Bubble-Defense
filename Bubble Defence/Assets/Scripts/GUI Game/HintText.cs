using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HintText : MonoBehaviour
{
    TMP_Text mytext;
    [SerializeField] float animTime = 0.3f;

    static HintText instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mytext = GetComponent<TMP_Text>();
        mytext.text = "";
    }

    public void ShowHint(string str, float duration = 3)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOKill();
        mytext.text = str;
        rect.anchoredPosition = new Vector2(0, -150);
        rect.DOAnchorPos(new Vector2(0, 250), animTime).SetEase(Ease.OutElastic);

        rect.DOAnchorPos(new Vector2(0, -150), animTime).SetDelay(duration);
    }

    public static void Show(string str, float duration = 3)
    {
        instance.ShowHint(str, duration);
    }

    
}
