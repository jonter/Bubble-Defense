using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class EditorCube : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        ShowTextInfo();
    }

    void SnapToGrid()
    {
        float roundX = Mathf.RoundToInt(transform.position.x);
        float roundZ = Mathf.RoundToInt(transform.position.z);

        transform.position = new Vector3(roundX, 0, roundZ);
    }

    void ShowTextInfo()
    {
        float roundX = Mathf.RoundToInt(transform.position.x);
        float roundZ = Mathf.RoundToInt(transform.position.z);

        string s = $"[{roundX},{roundZ}]";
        name = "Cube " + s;
        //TMP_Text mytext = GetComponentInChildren<TMP_Text>();
        //mytext.text = s;
    }


}
