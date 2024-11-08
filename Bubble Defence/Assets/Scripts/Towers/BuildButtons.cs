using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildButtons : MonoBehaviour
{
    [SerializeField] Button archerButton;
    [SerializeField] Button magicButton;
    [SerializeField] Button canonButton;

    public bool Show = false;

    Waypoint selectedPoint;

    [SerializeField] float animTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        archerButton.gameObject.SetActive(false);
        magicButton.gameObject.SetActive(false);
        canonButton.gameObject.SetActive(false);

        archerButton.onClick.AddListener(SpawnArcherTower);
        magicButton.onClick.AddListener(SpawnMagicTower);
        canonButton.onClick.AddListener(SpawnCanonTower);

        archerButton.transform.localScale = new Vector3(0, 0, 0);
        magicButton.transform.localScale = new Vector3(0, 0, 0);
        canonButton.transform.localScale = new Vector3(0, 0, 0);

        FindAnyObjectByType<EnemySpawner>().OnStartSpawn += HideButtons;
    }

    public void ShowButtons(Waypoint point)
    {
        selectedPoint = point;
        
        Vector3 pos = point.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(pos);
        if (Show == false)
        {
            StartCoroutine(ShowButtonsCoroutine());
            transform.position = canvasPos;
        }
        else
        {
            transform.DOMove(canvasPos, animTime);
        }
    }

    public void HideButtons()
    {
        selectedPoint = null;
        StartCoroutine(HideButtonsCoroutine());
        Show = false;
    }

    void SpawnArcherTower()
    {
        if (selectedPoint == null) return;
        TowerCreator.instance.SpawnArcherTower(selectedPoint);
        HideButtons();
    }

    void SpawnMagicTower()
    {
        if (selectedPoint == null) return;
        TowerCreator.instance.SpawnMagicTower(selectedPoint);
        HideButtons();
    }

    void SpawnCanonTower()
    {
        if (selectedPoint == null) return;
        TowerCreator.instance.SpawnCanonTower(selectedPoint);
        HideButtons();
    }

    IEnumerator HideButtonsCoroutine()
    {
        archerButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), animTime);
        magicButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), animTime);
        canonButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), animTime);

        archerButton.transform.DOScale(0, animTime);
        magicButton.transform.DOScale(0, animTime);
        canonButton.transform.DOScale(0, animTime);

        yield return new WaitForSeconds(animTime);

        archerButton.gameObject.SetActive(false);
        magicButton.gameObject.SetActive(false);
        canonButton.gameObject.SetActive(false);
    }

    IEnumerator ShowButtonsCoroutine()
    {
        archerButton.gameObject.SetActive(true);
        magicButton.gameObject.SetActive(true);
        canonButton.gameObject.SetActive(true);

        archerButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 120), animTime);
        magicButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-120, 20), animTime);
        canonButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(120, 20), animTime);

        archerButton.transform.DOScale(1, animTime);
        magicButton.transform.DOScale(1, animTime);
        canonButton.transform.DOScale(1, animTime);

        yield return new WaitForSeconds(animTime);
        Show = true;
    }

}
