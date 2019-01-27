using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Case 
{
    public bool isEmpty = true;

    //[SerializeField] public Vector2Int coordonnee;
    public Vector2 pivot;
    public int XD;
    public int YD;

    public int blockID;

    public Material Mat;

}
