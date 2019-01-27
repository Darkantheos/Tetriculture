using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenuInventaire : MonoBehaviour
{
    [SerializeField] private TestGrille grille;
    [SerializeField] private BlocObject blocObject;
    [SerializeField] private Canvas canvas;

    private BlocObject blocInstantie = null;
    private Vector3 blocPosition;
    private float blocRotation;
    private Vector3 blocScale;


    private void Start()
    {
        blocPosition = new Vector3(0, 0, 0);
        blocRotation = 0;
        float scale = 46.59222f;
        blocScale = new Vector3(scale, scale, scale);
    }

    private void Update()
    {
        if ((blocInstantie) && (Input.GetMouseButton(0)))
        {
            blocPosition.Set(Input.mousePosition.x, Input.mousePosition.y, 5);
            blocPosition = Camera.main.ScreenToWorldPoint(blocPosition);
            blocPosition.Set(blocPosition.x, blocPosition.y, 4.9f);
            blocInstantie.transform.position = blocPosition;
            blocInstantie.transform.localScale = blocScale;
        }

        if ((blocInstantie) && (Input.GetMouseButtonUp(0)))
        {
            grille.VerifierContenu(blocInstantie, blocPosition, (int)blocRotation);

            DestroyImmediate(blocInstantie.gameObject);
            blocInstantie = null;

            blocPosition.Set(0, 0, 0);
            //blocRotation.Set(0, 0, 0);
        }
    }


    public void CreerBloc()
    {
        blocInstantie = Instantiate(blocObject, canvas.transform);
    }

    public void TournerGauche()
    {
        Tourner(-90);
    }

    public void TournerDroite()
    {
        Tourner(90);
    }


    private void Tourner(float rotation)
    {
        //blocRotation.Set(rotation, blocRotation.y, blocRotation.z);
        //blocObject.transform.localEulerAngles = blocRotation;
        blocRotation += rotation;
        if (blocRotation == 360)
            blocRotation = 0;
        else if (blocRotation == -90)
            blocRotation = 270;

        Debug.Log(blocRotation);

        blocObject.transform.Rotate(0, rotation, 0);
    }
}
