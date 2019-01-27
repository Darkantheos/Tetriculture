using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenuInventaire : MonoBehaviour
{
    [SerializeField] private TestGrille grille;
    [SerializeField] private BlocObject blocObject;

    private BlocObject blocInstantie = null;
    private Vector3 blocPosition;
    private Vector3 blocRotation;


    private void Start()
    {
        blocPosition = new Vector3(0, 0, 0);
        blocRotation = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if ((blocInstantie) && (Input.GetMouseButton(0)))
        {
            blocPosition.Set(Input.mousePosition.x, Input.mousePosition.y, 2);
            blocPosition = Camera.main.ScreenToWorldPoint(blocPosition);
            blocPosition.Set(blocPosition.x, blocPosition.y, 1.9f);
            blocInstantie.transform.position = blocPosition;
        }

        if ((blocInstantie) && (Input.GetMouseButtonUp(0)))
        {
            grille.VerifierContenu(blocPosition, (int)blocRotation.z);

            DestroyImmediate(blocInstantie.gameObject);
            blocInstantie = null;

            blocPosition.Set(0, 0, 0);
            blocRotation.Set(0, 0, 0);
        }
    }


    public float GetRotation()
    {
        return blocRotation.z;
    }

    public void TournerGauche()
    {
        Tourner(blocRotation.z + 90);
    }

    public void TournerDroite()
    {
        Tourner(blocRotation.z - 90);
    }


    private void Tourner(float rotation)
    {
        blocRotation.Set(blocRotation.x, blocRotation.y, rotation);
        blocObject.transform.localEulerAngles = blocRotation;
    }
}
