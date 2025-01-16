using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCoins : MonoBehaviour
{
    TMP_Text coinstext;
    [SerializeField] int coins = 0;

    public static GameCoins instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coinstext = GetComponentInChildren<TMP_Text>();
        coinstext.text = "" + coins;
    }

    public void AddGameCoins(int add)
    {
        coins += add;
        coinstext.text = "" + coins;
    }

    public bool SpendGameCoins(int price)
    {
        if (price > coins) return false;
        coins -= price;
        coinstext.text = "" + coins;
        return true;
    }

    public static void AddCoins(int add)
    {
        instance.AddGameCoins(add);
    }

    public static bool SpendCoins(int price)
    {
        bool success = instance.SpendGameCoins(price);
        return success; 
    }
    
}
