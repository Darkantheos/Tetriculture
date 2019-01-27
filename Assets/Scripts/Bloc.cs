using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Bloc
{
    // Une case en L est definie par 4 cases avec ces positions :
    // (0,0) / (1,0) / (0,1) / (0,2)
    [SerializeField] public List<Case> cases = new List<Case>();

    [SerializeField] public Vector2Int pivot;
    [SerializeField] public Vector2Int min;
    [SerializeField] public Vector2Int max;

    public Plante plante;
    public int DaysGrow;
}
