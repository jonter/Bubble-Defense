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

    public bool Show = false;

    [SerializeField] float animTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);

        upgradeButton.transform.localScale = new Vector3();
        destroyButton.transform.localScale = new Vector3();

        upgradeButton.onClick.AddListener(TowerUpgrade.instance.Upgrade);
        destroyButton.onClick.AddListener(TowerUpgrade.instance.Sell);
    }

    public void ShowButtons(Tower t, int upgradePrice, int destroyPrice)
    {
        Vector3 worldPos = t.transform.position + new Vector3(0, 0.2f, 0);
        Vector2 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        DisplayPrices(upgradePrice, destroyPrice);
        if(Show == false)
        {
            transform.position = canvasPos;
            StartCoroutine(ShowButtonsCoroutine());
        }
        else
        {
            transform.DOMove(canvasPos, animTime);
        }
    }

    void DisplayPrices(int upgradePrice, int destroyPrice)
    {
        TMP_Text upgradeText = upgradeButton.GetComponentInChildren<TMP_Text>();
        TMP_Text destroyText = destroyButton.GetComponentInChildren<TMP_Text>();

        upgradeText.text = "-" + upgradePrice;
        destroyText.text = "+" + destroyPrice;
    }

    IEnumerator ShowButtonsCoroutine()
    {
        upgradeButton.gameObject.SetActive(true);
        destroyButton.gameObject.SetActive(true);

        upgradeButton.GetComponent<RectTransform>()
            .DOAnchorPos(new Vector2(100, 100), animTime);
        destroyButton.GetComponent<RectTransform>()
           .DOAnchorPos(new Vector2(100, -100), animTime);

        upgradeButton.transform.DOScale(1, animTime);
        destroyButton.transform.DOScale(1, animTime);
        yield return new WaitForSeconds(animTime);
        Show = true;
    }


    public void HideButtons()
    {
        Show = false;
        StartCoroutine(HideButtonsCoroutine());
    }

    IEnumerator HideButtonsCoroutine()
    {
        
        upgradeButton.GetComponent<RectTransform>()
            .DOAnchorPos(new Vector2(0, 0), animTime);
        destroyButton.GetComponent<RectTransform>()
           .DOAnchorPos(new Vector2(0, 0), animTime);

        upgradeButton.transform.DOScale(0, animTime);
        destroyButton.transform.DOScale(0, animTime);
        yield return new WaitForSeconds(animTime);

        upgradeButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
    }


}
