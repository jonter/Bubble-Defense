using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        rot = Quaternion.Euler(60, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = rot;
    }
}
