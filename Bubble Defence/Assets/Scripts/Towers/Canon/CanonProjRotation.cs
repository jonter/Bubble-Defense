using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjRotation : CanonProjectile
{
    Vector3 prevPos;

    // Update is called once per frame
    void Update()
    {
        if (launched == false) return;
        Vector3 dir =  transform.position - prevPos;
        transform.forward = dir;

        prevPos = transform.position;
    }
}
