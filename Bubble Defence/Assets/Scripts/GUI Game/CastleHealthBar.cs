using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CastleHealthBar : MonoBehaviour
{
    Slider myslider;
    [SerializeField] Image icon;
    [SerializeField] Sprite destroyedCastle;
    // Start is called before the first frame update
    void Start()
    {
        myslider = GetComponent<Slider>();
        FindAnyObjectByType<Castle>().OnDamage += DisplayHealth;
    }

    void DisplayHealth(float percent)
    {
        myslider.DOKill();
        myslider.DOValue(percent, 0.15f).SetEase(Ease.InOutCubic);

        if(percent < 0.001f)
        {
            icon.sprite = destroyedCastle;
            icon.color = Color.gray;
        }
    }

    
}
