using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuileClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        print(Master.mastersing.TuilesMap[transform.GetSiblingIndex()].CoordTuile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
