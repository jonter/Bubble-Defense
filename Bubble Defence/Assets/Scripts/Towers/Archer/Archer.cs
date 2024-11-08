using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Archer : MonoBehaviour
{
    Arrow currentArrow;
    [SerializeField] GameObject arrowPrefab;
    Vector3 startPos;
    Quaternion startRot;
    Vector3 startScale;

    bool isReloaded = true;

    // Start is called before the first frame update
    void Awake()
    {
        currentArrow = GetComponentInChildren<Arrow>();
        startPos = currentArrow.transform.localPosition;
        startRot = currentArrow.transform.localRotation;
        startScale = currentArrow.transform.localScale;
    }

    public void Shoot(EnemyHealth enemy, float damage, float reloadTime)
    {
        RotateToEnemy(enemy);
        if (isReloaded == false) return;
        currentArrow.Launch(enemy, damage);
        StartCoroutine(ReloadCoroutine(reloadTime));
    }

    void RotateToEnemy(EnemyHealth enemy)
    {
        transform.LookAt(enemy.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    IEnumerator ReloadCoroutine(float reloadTime)
    {
        isReloaded = false;
        yield return new WaitForSeconds(reloadTime);
        GameObject newArrow = Instantiate(arrowPrefab, transform);
        newArrow.transform.localPosition = startPos;
        newArrow.transform.localRotation = startRot;
        newArrow.transform.localScale = startScale;
        currentArrow = newArrow.GetComponent<Arrow>();
        isReloaded = true;
    }
    
}
