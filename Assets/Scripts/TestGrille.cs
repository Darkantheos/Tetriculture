using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrille : MonoBehaviour
{
    public const int TAILLE = 5;

    [SerializeField] private Material rouge;

    private GameObject[,] quads;
    private bool[,] contenus;


    private void Start()
    {
        quads = new GameObject[TAILLE, TAILLE];
        contenus = new bool[TAILLE, TAILLE];

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

    public void VerifierContenu(BlocObject blocObject, Vector3 position, int rotation)
    {
        for (int i = 0; i < TAILLE; ++i)
        {
            for (int j = 0; j < TAILLE; ++j)
            {
                GameObject quad = quads[i, j];
                Collider collider = quad.GetComponent<Collider>();
                Renderer renderer = quad.GetComponent<Renderer>();

                /*if (i == 2 && j == 3)
                {
                    Debug.Log("i + blocObject.bloc.min.x = " + (i + blocObject.bloc.min.x));
                    Debug.Log("j + blocObject.bloc.min.y = " + (j + blocObject.bloc.min.y));
                    Debug.Log("i + blocObject.bloc.max.x = " + (i + blocObject.bloc.max.x));
                    Debug.Log("j + blocObject.bloc.max.y = " + (j + blocObject.bloc.max.y));
                    Debug.Log(collider.bounds.Contains(position));
                }*/

                if (collider.bounds.Contains(position) &&
                    (i + blocObject.bloc.min.x >= 0) && (j + blocObject.bloc.min.y >= 0) &&
                    (i + blocObject.bloc.max.x < TAILLE) && (j + blocObject.bloc.max.y < TAILLE))
                {
                    Debug.Log("Dedans");
                    foreach(Case c in blocObject.bloc.cases)
                    {
                        if(contenus[i - c.XD, j - c.YD])
                        {
                            return;
                        }
                    }

                    foreach (Case c in blocObject.bloc.cases)
                    {
                        quads[i - c.XD, j - c.YD].GetComponent<Renderer>().material = c.Mat;
                        contenus[i - c.XD, j - c.YD] = true;
                    }

                    return;
                }
                /*else
                {
                    Debug.Log("En dehors");
                }*/
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
}
