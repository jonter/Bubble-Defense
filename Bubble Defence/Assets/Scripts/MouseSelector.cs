using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelector : MonoBehaviour
{
    Camera mycamera;

    BuildButtons bb;
    // Start is called before the first frame update
    void Start()
    {
        bb = FindAnyObjectByType<BuildButtons>();
        mycamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MakeRaycast();
        }
        
    }

    void MakeRaycast()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray r = mycamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(r, out hitInfo, 1000);
        if (hitInfo.transform == null) return;
        Waypoint wp = hitInfo.transform.GetComponent<Waypoint>();
        if(wp == null) return;
        bb.ShowButtons(wp);
        
    }
}
