using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradeTowerButtons : MonoBehaviour
{
    [SerializeField] Button upgradeButton;
    [SerializeField] Button destroyButton;
    [SerializeField] Button extraUpgradeButton;

    public bool Show = false;

    [SerializeField] float animTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        extraUpgradeButton.gameObject.SetActive(false);

        upgradeButton.transform.localScale = new Vector3();
        destroyButton.transform.localScale = new Vector3();
        extraUpgradeButton.transform.localScale = new Vector3();

        upgradeButton.onClick.AddListener(TowerUpgrade.instance.Upgrade);
        destroyButton.onClick.AddListener(TowerUpgrade.instance.Sell);
        extraUpgradeButton.onClick.AddListener(TowerUpgrade.instance.ExtraUpgrade);
    }

    public void ShowButtons(Tower t, int upgradePrice, int destroyPrice)
    {
        Vector3 worldPos = t.transform.position + new Vector3(0, 0.2f, 0);
        Vector2 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        DisplayPrices(upgradePrice, destroyPrice);
        StartCoroutine(ShowButtonCoroutine(upgradeButton, new Vector2(100, 100)));
        StartCoroutine(ShowButtonCoroutine(destroyButton, new Vector2(100, -100)));
        if (Show == false)
        {
            Show = true;
            transform.position = canvasPos;
        }
        else
        {
            transform.DOMove(canvasPos, animTime);
            StartCoroutine(HideButtonCoroutine(extraUpgradeButton));
        }
    }


    public void ShowExtraButtons(Tower t, int upgradePriceA, int upgradePriceB, int destroyPrice)
    {
        Vector3 worldPos = t.transform.position + new Vector3(0, 0.2f, 0);
        Vector2 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        DisplayPrices(upgradePriceA, upgradePriceB, destroyPrice);
        StartCoroutine(ShowButtonCoroutine(upgradeButton, new Vector2(100, 100)));
        StartCoroutine(ShowButtonCoroutine(extraUpgradeButton, new Vector2(250, 100)));
        StartCoroutine(ShowButtonCoroutine(destroyButton, new Vector2(100, -100)));
        if (Show == false)
        {
            Show = true;
            transform.position = canvasPos;
        }
        else
        {
            transform.DOMove(canvasPos, animTime);
        }
    }



    public void ShowDestroyButton(Tower t, int sellPrice)
    {
        Vector3 worldPos = t.transform.position + new Vector3(0, 0.2f, 0);
        Vector2 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        DisplayPrices(sellPrice);
        StartCoroutine(ShowButtonCoroutine(destroyButton, new Vector2(100, 0)));
        if (Show == false)
        {
            Show = true;
            transform.position = canvasPos;
        }
        else
        {
            transform.DOMove(canvasPos, animTime);
            StartCoroutine(HideButtonCoroutine(upgradeButton));
            StartCoroutine(HideButtonCoroutine(extraUpgradeButton));
        }
    }

    void DisplayPrices(int sellPrice)
    {
        TMP_Text destroyText = destroyButton.GetComponentInChildren<TMP_Text>();
        destroyText.text = "+" + sellPrice;
    }

    void DisplayPrices(int upgradePrice, int destroyPrice)
    {
        TMP_Text upgradeText = upgradeButton.GetComponentInChildren<TMP_Text>();
        TMP_Text destroyText = destroyButton.GetComponentInChildren<TMP_Text>();

        upgradeText.text = "-" + upgradePrice;
        destroyText.text = "+" + destroyPrice;
    }

    void DisplayPrices(int upgradePriceA, int upgradePriceB, int destroyPrice)
    {
        TMP_Text upgradeText = upgradeButton.GetComponentInChildren<TMP_Text>();
        TMP_Text destroyText = destroyButton.GetComponentInChildren<TMP_Text>();
        TMP_Text extraUpgradeText = extraUpgradeButton.GetComponentInChildren<TMP_Text>(); 

        upgradeText.text = "-" + upgradePriceA;
        destroyText.text = "+" + destroyPrice;
        extraUpgradeText.text = "-" + upgradePriceB;
    }

    IEnumerator ShowButtonCoroutine(Button btn, Vector2 pos)
    {
        btn.gameObject.SetActive(true);

        btn.GetComponent<RectTransform>()
            .DOAnchorPos(pos, animTime);

        btn.transform.DOScale(1, animTime);
        yield return new WaitForSeconds(animTime);
        
    }


    public void HideButtons()
    {
        Show = false;
        StartCoroutine(HideButtonCoroutine(upgradeButton));
        StartCoroutine(HideButtonCoroutine(destroyButton));
        StartCoroutine(HideButtonCoroutine(extraUpgradeButton)); 
    }

    IEnumerator HideButtonCoroutine(Button btn)
    {
        btn.GetComponent<RectTransform>()
            .DOAnchorPos(new Vector2(0, 0), animTime);


        btn.transform.DOScale(0, animTime);
        
        yield return new WaitForSeconds(animTime);

        btn.gameObject.SetActive(false);
    }


}
