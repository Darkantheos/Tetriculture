using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrille : MonoBehaviour
{
    public const int TAILLE = 5;

    [SerializeField] private Material rouge;

    private GameObject[,] quads = new GameObject[TAILLE, TAILLE];
    private bool[,] contenus = new bool[TAILLE, TAILLE];


    private void StartGrille()
    {
        int i = 0;
        int j = 0;

        foreach(Transform t in transform)
        {
            quads[i, j] = t.gameObject;

            ++j;

            if(j == TAILLE)
            {
                j = 0;
                ++i;
            }
        }

        for (i = 0; i < TAILLE; ++i)
        {
            for (j = 0; j < TAILLE; ++j)
            {
                contenus[i, j] = false;
            }
        }
    }

    public Material AubergineCase;

    public void ResetTableau()
    {
        StartGrille();

       int ID = Master.mastersing.currentTuileActive;
       // Master.mastersing.TuilesMap[ID].CaseArray

       for (int i = 0; i < TAILLE; ++i)
                {
            for (int j = 0; j < TAILLE; ++j)
            {
                print(Master.mastersing.TuilesMap[ID].CaseArray[i, j].isEmpty);
                print(contenus);
                contenus[i, j] = Master.mastersing.TuilesMap[ID].CaseArray[i,j].isEmpty;
                
                if (!contenus[i, j])
                {
                    switch (Master.mastersing.TuilesMap[ID].blockList[Master.mastersing.TuilesMap[ID].CaseArray[i, j].blockID].plante.planteName)
                    {
                        case "Aubergine":
                    quads[i, j].GetComponent<Renderer>().material = AubergineCase;

                            break;
                     }
                }
                else
                {
                    quads[i, j].GetComponent<Renderer>().material = rouge;
                }
            }
        }
    }


    public void VerifierContenu(BlocObject blocObject, Vector3 position, int rotation)
    {
        Debug.Log("Rotation = " + rotation);

        for (int i = 0; i < TAILLE; ++i)
        {
            for (int j = 0; j < TAILLE; ++j)
            {
                GameObject quad = quads[i, j];
                Collider collider = quad.GetComponent<Collider>();
                Renderer renderer = quad.GetComponent<Renderer>();

                Vector2Int min = Vector2Int.zero;
                Vector2Int max = Vector2Int.zero;
                int deltaX = 0;
                int deltaY = 0;

                switch (rotation)
                {
                    case 0:
                        min.x = i + blocObject.bloc.min.x;
                        min.y = j + blocObject.bloc.min.y;
                        max.x = i + blocObject.bloc.max.x;
                        max.y = j + blocObject.bloc.max.y;
                        break;

                    case 90:
                        min.x = i - blocObject.bloc.max.y;
                        min.y = j + blocObject.bloc.min.x;
                        max.x = i - blocObject.bloc.min.y;
                        max.y = j + blocObject.bloc.max.x;
                        break;

                    case 180:
                        break;

                    case 270:
                        break;
                }

                if (collider.bounds.Contains(position) &&
                    (min.x >= 0) && (min.y >= 0) &&
                    (max.x < TAILLE) && (max.y < TAILLE))
                {
                    foreach(Case c in blocObject.bloc.cases)
                    {
                        switch(rotation)
                        {
                            case 0:
                                if (!contenus[i - c.XD, j - c.YD])
                                {
                                    return;
                                }
                                break;
                            case 90:
                                if (!contenus[i - c.XD, j - c.YD])
                                {
                                    return;
                                }
                                break;
                            case 180:
                                if (!contenus[i - c.XD, j - c.YD])
                                {
                                    return;
                                }
                                break;
                            case 270:
                                if (!contenus[i - c.XD, j - c.YD])
                                {
                                    return;
                                }
                                break;
                        }

                        if(!contenus[i - c.XD, j - c.YD])
                        {
                            return;
                        }
                    }
                    int ID = Master.mastersing.currentTuileActive;

                    Bloc blocTuile = blocObject.bloc;


                    foreach (Case c in blocObject.bloc.cases)
                    {
                        
                        quads[i - c.XD, j - c.YD].GetComponent<Renderer>().material = c.Mat;
                        contenus[i - c.XD, j - c.YD] = false;
                        quads[i - c.XD, j - c.YD].GetComponent<CaseBlock>().caseblock = c;
                        

                    }
                    Master.mastersing.TuilesMap[ID].blockList.Add(blocTuile);
                    return;
                }
            }
        }


        

        /*foreach (Case c in blocObject.bloc.cases)
        {
            c.
        }*/

        /*switch (rotation)
        {
            case 0:
                for (int i = 0; i < TAILLE; ++i)
                {
                    for (int j = 0; j < TAILLE; ++j)
                    {
                        GameObject quad = quads[i, j];
                        Collider collider = quad.GetComponent<Collider>();
                        Renderer renderer = quad.GetComponent<Renderer>();

                        if (collider.bounds.Contains(position) &&
                            (i + 1 < TAILLE) && (j + 2 < TAILLE) &&
                            !contenus[i, j] && !contenus[i, j + 1] && !contenus[i + 1, j] && !contenus[i, j + 2])
                        {
                            renderer.material = rouge;
                            quads[i, j + 1].GetComponent<Renderer>().material = rouge;
                            quads[i + 1, j].GetComponent<Renderer>().material = rouge;
                            quads[i, j + 2].GetComponent<Renderer>().material = rouge;

                            contenus[i, j] = true;
                            contenus[i, j + 1] = true;
                            contenus[i + 1, j] = true;
                            contenus[i, j + 2] = true;
                        }
                    }
                }
                break;
        }*/
    }

    public void validate()
    {
        int ID = Master.mastersing.currentTuileActive;
        for (int i = 0; i < TAILLE; ++i)
        {
            for (int j = 0; j < TAILLE; ++j)
            {
                GameObject quad = quads[i, j];
                Master.mastersing.TuilesMap[ID].CaseArray[i, j] = quad.GetComponent<CaseBlock>().caseblock;
                
            }
        }

        Master.mastersing.UpdateTuile(Master.mastersing.currentTuileActive);
        Master.mastersing.closeTuilePanel();
    }


}
