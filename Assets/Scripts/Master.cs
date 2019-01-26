using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{

    public List<Tuile> TuilesMap = new List<Tuile>();

    public Dictionary<string, Sprite> TexturesTuile;

    public GameObject TuilePrefab;
    public GameObject TuileParent;
    public Vector3 vectorTemp = new Vector3(0,0,0);
    // Start is called before the first frame update

    public GameObject LinePrefab;
    public Transform GridParent; 

    public int gridSize = 10;
        public void DrawGrid()
    {
        //Dessinne la grille en lines renderer
        for (int i = 0; i < gridSize +1; i++)
        {
            GameObject Line = Instantiate(LinePrefab, GridParent);
            vectorTemp.Set(i*5 - 2.5f, 0.01f, -2.5f);
            Line.GetComponent<LineRenderer>().SetPosition(0, vectorTemp);
            vectorTemp.Set(i * 5 - 2.5f, 0.01f, 47.5f);
            Line.GetComponent<LineRenderer>().SetPosition(1, vectorTemp);
        }
        for (int j = 0; j < gridSize + 1; j++)
        {
            GameObject Line = Instantiate(LinePrefab, GridParent);
            vectorTemp.Set(- 2.5f, 0.01f, j*5 -2.5f);
            Line.GetComponent<LineRenderer>().SetPosition(0, vectorTemp);
            vectorTemp.Set(47.5f, 0.01f,j*5 -2.5f);
            Line.GetComponent<LineRenderer>().SetPosition(1, vectorTemp);
        }
    }



    void Start()
    {
        DrawGrid();
        TuilesMap = new List<Tuile>();
        FillTuiles();
        InstantiateTuiles();
    }

    public void FillTuiles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Tuile tuile = new Tuile();
                tuile.CoordTuile = new Vector2(i*5, j*5);
                TuilesMap.Add(tuile);

                tuile.BlockList = new Bloc[5, 5];
                Bloc bloc0 = new Bloc();
                bloc0.cases = new List<Case>(); 
              
            }
        }
    }

    public void UpdateTuile(int x, int y)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateTuiles()
    {
        for (int i = 0; i < TuilesMap.Count; i++)
        {
            vectorTemp.Set(TuilesMap[i].CoordTuile.x, 0, TuilesMap[i].CoordTuile.y);
            
            GameObject tuile = Instantiate(TuilePrefab, vectorTemp, Quaternion.identity ,TuileParent.transform);
              
        }
    }

    public int currentYear = 0;
    public int currentSeason = 0;
    private int seasonDays = 10;
    public int currentDay = 0;

    private int ActionNb = 10;
    private int currentAction = 0;



    public void NextAction()
    {
        currentAction++;
if (currentAction<ActionNb)
        {

        }
else
        {
           //change le sprite de l'horloge, et activer le bouton lit (NextDay) 
        }
    }

    public void NextDay()
    {
        print("on passe au jour suivant");
        currentDay++;
        currentAction = 0;
        if(currentDay>= seasonDays)
        {
            newSeason();
        }
    }

    public void newSeason()
    {
        currentDay = 0;
        currentSeason++;
        if (currentSeason > 3)
        {
           
        }
    }

    public void newYear()
    {
        currentSeason = 0;
        currentYear++;
    }
}
