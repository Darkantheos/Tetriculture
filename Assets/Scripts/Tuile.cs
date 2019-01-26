using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tuile 
{
    public Vector2 CoordTuile;
    public string SoilTexture;

    public List<Bloc> blockList;

    public Case[,] CaseArray;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
