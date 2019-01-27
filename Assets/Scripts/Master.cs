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

    public GameObject auberginePrefab0;
    public GameObject auberginePrefab1;
    public GameObject auberginePrefab2;
    public GameObject temporalObject;
    public int gridSize = 10;

    public static Master mastersing; 

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
        if (mastersing)
        {

        }
        else
        {
            mastersing = this;
        }
        DrawGrid();
        TuilesMap = new List<Tuile>();
        FillTuiles();
        InstantiateTuiles();
        for (int i = 0; i < TuilesMap.Count; i++)
        {
            UpdateTuile(i);
        }
    }

    public void FillTuiles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Tuile tuile = new Tuile();
                tuile.CoordTuile = new Vector2(i*5, j*5);
                //print(tuile.CoordTuile);
                TuilesMap.Add(tuile);



                Bloc aubergine = new Bloc();
                aubergine.DaysGrow = 0;

                aubergine.plante = new Plante();
                aubergine.plante.planteName = "Aubergine";
                aubergine.plante.SeasonGrow = 0;
                aubergine.plante.Stade2Day = 1;
                aubergine.plante.Stade3Day = 2;
                aubergine.plante.stadesPlante = new List<GameObject>();
                aubergine.plante.stadesPlante.Add(auberginePrefab0);
                aubergine.plante.stadesPlante.Add(auberginePrefab1);
                aubergine.plante.stadesPlante.Add(auberginePrefab2);

                for (int k = 0; k < 5; k++)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        tuile.CaseArray[k, l] = new Case();
                        tuile.CaseArray[k, l].isEmpty = true;
                    }
                }

                tuile.CaseArray[2, 2].blockID = 0;
                tuile.CaseArray[2, 2].pivot.Set(2, 2);
                tuile.CaseArray[2, 2].XD = 0;
                tuile.CaseArray[2, 2].YD = 0;
                tuile.CaseArray[2, 2].isEmpty = false;

                aubergine.cases.Add(tuile.CaseArray[2, 2]);
                tuile.blockList.Add(aubergine);

                
              
            }
        }
    }

    public void UpdateTuile(int ID)
    {

        //print(ID);
      foreach (Transform child in TuileParent.transform.GetChild(ID))
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(TuilesMap[ID].CaseArray[i,j].isEmpty == false)
                {
                    //print("je place une aubergine");
                    //Check l'avancement de la plante
                    GameObject item = temporalObject;
                    if (TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].DaysGrow < TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].plante.Stade2Day)
                    {
                        item = Instantiate(TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].plante.stadesPlante[0]);
                    }
                    else if (TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].DaysGrow < TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].plante.Stade3Day)
                    {
                        item = Instantiate(TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].plante.stadesPlante[1]);
                    }
                    else
                    {
                        item = Instantiate(TuilesMap[ID].blockList[TuilesMap[ID].CaseArray[i, j].blockID].plante.stadesPlante[2]);
                    }

                     
                    
                    Vector3 kiki = new Vector3(TuilesMap[ID].CoordTuile.x- 2 + i, 0, TuilesMap[ID].CoordTuile.y- 2 +j);
                    item.transform.position = kiki;
                    //print(TuilesMap[ID].CoordTuile.x);
                    item.transform.parent = TuileParent.transform.GetChild(ID);
                }
            }
        }

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
        //print("on passe au jour suivant");
        currentDay++;
        currentAction = 0;
        if(currentDay>= seasonDays)
        {
            newSeason();
        }

        //Update les jours des cases
        for (int i = 0; i < TuilesMap.Count; i++)
        {
            for (int j = 0; j < TuilesMap[i].blockList.Count; j++)
            {
                TuilesMap[i].blockList[j].DaysGrow++;
            }
        }

        for (int i = 0; i < TuilesMap.Count; i++)
        {
            UpdateTuile(i);
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
