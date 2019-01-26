using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    // Une case en L est definie par 4 cases avec ces positions :
    // (0,0) / (1,0) / (0,1) / (0,2)
    [SerializeField] private List<Case> cases;

    [SerializeField] private Vector2Int pivot;

    private Plante plante;
}
