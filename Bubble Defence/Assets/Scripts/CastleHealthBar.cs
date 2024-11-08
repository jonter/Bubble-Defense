using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        myslider.value = percent;
        if(percent < 0.001f)
        {
            icon.sprite = destroyedCastle;
            icon.color = Color.gray;
        }
    }

    
}
