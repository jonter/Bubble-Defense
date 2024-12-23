using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    UpgradeTowerButtons upgradeButtons;
    Tower selectedTower;

    public static TowerUpgrade instance;

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
            HintText.Show("���� ����� �������� �� �� ������ � ������");
            return;
        }
        selectedTower = t;
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

    public void Deselect()
    {
        selectedTower = null;
        upgradeButtons.HideButtons();
    }

    public void Upgrade()
    {
        if (selectedTower == null) return;
        int upgradePrice = selectedTower.upgradedTower.price;
        if (GameCoins.SpendCoins(upgradePrice) == false)
        {
            HintText.Show("������������ ����� ��� ��������", 1.5f);
        }
        else
        {
            selectedTower.Upgrade();
            selectedTower = null;
        }
        upgradeButtons.HideButtons();
    }

    public void ExtraUpgrade()
    {
        if (selectedTower == null) return;
        int upgradePrice = selectedTower.extraUpgradeTower.price;
        if (GameCoins.SpendCoins(upgradePrice) == false)
        {
            HintText.Show("������������ ����� ��� ��������", 1.5f);
        }
        else
        {
            selectedTower.ExtraUpgrade();
            selectedTower = null;
        }
        upgradeButtons.HideButtons();
    }

    public void Sell()
    {
        if (selectedTower == null) return;
        selectedTower.SellTower();
        upgradeButtons.HideButtons();
        selectedTower = null;
    }

    
}
