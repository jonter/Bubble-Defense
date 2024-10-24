using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildTowerButton : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    void Start()
    {
        TMP_Text priceText = GetComponentInChildren<TMP_Text>();
        priceText.text = "" + towerPrefab.price;
    }

}
