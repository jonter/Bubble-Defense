using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    UpgradeTowerButtons upgradeButtons;
    Tower selectedTower;

    public static TowerUpgrade instance;
    [SerializeField] GameObject radiusSphere;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        upgradeButtons = FindAnyObjectByType<UpgradeTowerButtons>();
    }

    public void Select(Tower t)
    {
        if(t.GetIsReady() == false)
        {
            HintText.Show("Пока башня строится ты не можешь её менять");
            return;
        }
        selectedTower = t;
        ShowAttackRadius(t);
        if(t.upgradedTower == null)
        {
            int sellPrice = t.price;
            upgradeButtons.ShowDestroyButton(t, sellPrice);
        }
        else if(t.extraUpgradeTower == null)
        {
            int upgradePrice = t.upgradedTower.price;
            int sellPrice = t.price;
            upgradeButtons.ShowButtons(t, upgradePrice, sellPrice);
        }
        else
        {
            int upgradePriceA = t.upgradedTower.price;
            int upgradePriceB = t.extraUpgradeTower.price;
            int sellPrice = t.price;
            upgradeButtons.ShowExtraButtons(t, upgradePriceA, upgradePriceB, sellPrice);
        }
    }

    void ShowAttackRadius(Tower t)
    {
        float attackRaduis = t.GetRadius();
        radiusSphere.transform.localScale = Vector3.one * attackRaduis * 2;
        radiusSphere.transform.position = t.transform.position + Vector3.down;
        radiusSphere.SetActive(true);
    }

    public void Deselect()
    {
        selectedTower = null;
        upgradeButtons.HideButtons();
        radiusSphere.SetActive(false);
    }

    public void Upgrade()
    {
        if (selectedTower == null) return;
        int upgradePrice = selectedTower.upgradedTower.price;
        if (GameCoins.SpendCoins(upgradePrice) == false)
        {
            HintText.Show("Недостаточно денег для прокачки", 1.5f);
        }
        else
        {
            selectedTower.Upgrade();
        }
        Deselect();
    }

    public void ExtraUpgrade()
    {
        if (selectedTower == null) return;
        int upgradePrice = selectedTower.extraUpgradeTower.price;
        if (GameCoins.SpendCoins(upgradePrice) == false)
        {
            HintText.Show("Недостаточно денег для прокачки", 1.5f);
        }
        else
        {
            selectedTower.ExtraUpgrade();
        }
        Deselect();
    }

    public void Sell()
    {
        if (selectedTower == null) return;
        selectedTower.SellTower();
        Deselect();
    }

    
}
