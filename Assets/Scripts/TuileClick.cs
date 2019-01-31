using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TuileClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (CameraHandler.camhandler.panActive == false)
            {
                if (!Master.mastersing.isTuilePanelActive)
                {

                    print(Master.mastersing.TuilesMap[transform.GetSiblingIndex()].CoordTuile);
                    Master.mastersing.OpenTuilePanel(transform.GetSiblingIndex());
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
