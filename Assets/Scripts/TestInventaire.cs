using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventaire : MonoBehaviour
{
    [SerializeField] private List<Bloc> blocs;
    [SerializeField] private List<ContenuInventaire> contenus;

    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform content;

    [SerializeField] private TestGrille grille;

    private Bloc blocCourant = null;
    private Vector3 blocPosition;
    private Vector3 blocRotation;


    // Start is called before the first frame update
    private void Start()
    {
        blocPosition = new Vector3(0, 0, 0);
        blocRotation = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition);

        if((blocCourant) && (Input.GetMouseButton(0)))
        {
            blocPosition.Set(Input.mousePosition.x, Input.mousePosition.y, 2);
            blocPosition = Camera.main.ScreenToWorldPoint(blocPosition);
            blocPosition.Set(blocPosition.x, blocPosition.y, 1.9f);
            blocCourant.transform.position = blocPosition;
        }

        if ((blocCourant) && (Input.GetMouseButtonUp(0)))
        {
            grille.VerifierContenu(blocPosition, (int)blocRotation.z);

            DestroyImmediate(blocCourant.gameObject);
            blocCourant = null;

            blocPosition.Set(0, 0, 0);
            blocRotation.Set(0, 0, 0);
        }
    }


    public void CreerBlocL()
    {
        blocRotation.Set(0, 0, contenus[0].GetRotation());
        blocCourant = Instantiate(blocs[0], blocPosition, Quaternion.Euler(blocRotation), canvas.transform);
    }
}
