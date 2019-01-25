using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{

    public List<Tuile> TuilesMap;

    public Dictionary<string, Sprite> TexturesTuile;

    public GameObject TuilePrefab;
    public GameObject TuileParent;
    public Vector3 vectorTemp = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        TuilesMap = new List<Tuile>();
        FillTuiles();
        InstantiateTuiles();
    }

    public void FillTuiles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Tuile tuile = new Tuile();
                tuile.CoordTuile = new Vector2(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateTuiles()
    {
        for (int i = 0; i < TuilesMap.Count; i++)
        {
            vectorTemp.Set(TuilesMap[i].CoordTuile.x, TuilesMap[i].CoordTuile.y, 0);
            GameObject tuile = Instantiate(TuilePrefab, vectorTemp, Quaternion.identity ,TuileParent.transform);
               
        }
    }
}
