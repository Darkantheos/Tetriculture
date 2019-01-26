using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenuInventaire : MonoBehaviour
{
    [SerializeField] private Bloc bloc;

    private Vector3 currentRotation;


    private void Start()
    {
        currentRotation = new Vector3(0, 0, 0);
    }


    public float GetRotation()
    {
        return currentRotation.z;
    }

    public void TournerGauche()
    {
        Tourner(currentRotation.z + 90);
    }

    public void TournerDroite()
    {
        Tourner(currentRotation.z - 90);
    }


    private void Tourner(float rotation)
    {
        currentRotation.Set(currentRotation.x, currentRotation.y, rotation);
        bloc.transform.localEulerAngles = currentRotation;
    }
}
