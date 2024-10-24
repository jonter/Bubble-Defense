using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelector : MonoBehaviour
{
    Camera mycamera;

    // Start is called before the first frame update
    void Start()
    {
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
        if (hitInfo.transform == null)
        {
            TowerCreator.instance.HideBuildButtons();
            return;
        }
        Waypoint wp = hitInfo.transform.GetComponent<Waypoint>();
        if(wp == null)
        {
            TowerCreator.instance.HideBuildButtons();
            return;
        }
        TowerCreator.instance.ShowBuildButtons(wp);
        
    }

}
