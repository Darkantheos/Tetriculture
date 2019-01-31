using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{

    public List<Tuile> TuilesMap = new List<Tuile>();

    public Dictionary<string, Sprite> TexturesTuile;

    public GameObject TuilePrefab;
    public GameObject TuileParent;
    public Vector3 vectorTemp = new Vector3(0, 0, 0);
    // Start is called before the first frame update

    public GameObject LinePrefab;
    public Transform GridParent;

    public GameObject auberginePrefab0;
    public GameObject auberginePrefab1;
    public GameObject auberginePrefab2;
    public GameObject temporalObject;
    public int gridSize = 10;

    public static Master mastersing;

    public Color morningSky;
    public Color middaysky;
    public Color nightsky;
    Gradient gradSky;
    GradientColorKey[] morningSkykey;
    GradientAlphaKey[] alphaKey;

    public int currentTuileActive = 0;
    public bool isTuilePanelActive = false;
    public GameObject CanvasTuilePanel;

    public TestGrille Tstgrille;

    public void OpenTuilePanel(int ID)
    {

        currentTuileActive = ID;
        isTuilePanelActive = true;
        CanvasTuilePanel.SetActive(true);
        Tstgrille.ResetTableau();
    }
    public void closeTuilePanel()
    {
        isTuilePanelActive = false;
        CanvasTuilePanel.SetActive(false);
    }

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

        gradSky = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        morningSkykey = new GradientColorKey[2];
        morningSkykey[0].color = morningSky;
        morningSkykey[0].time = 0.0f;
        morningSkykey[1].color = middaysky;
        morningSkykey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradSky.SetKeys(morningSkykey, alphaKey);

        updateHorloge();



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

                /*
                tuile.CaseArray[2, 2].blockID = 0;
                tuile.CaseArray[2, 2].pivot.Set(2, 2);
                tuile.CaseArray[2, 2].XD = 0;
                tuile.CaseArray[2, 2].YD = 0;
                tuile.CaseArray[2, 2].isEmpty = false;
                */
                //aubergine.cases.Add(tuile.CaseArray[2, 2]);
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

    public Button nextActionButton;
    public Button BedButton;
    public RectTransform WheelDay;
    public Text ActionText;
    public GameObject sun;
    public Light sunlight;

    public void NextAction()
    {
        
        if (currentAction<ActionNb-1)
        {
            currentAction++;
            //entre 50 et -135
            updateHorloge();
        }
else
        {
            //change le sprite de l'horloge, et activer le bouton lit (NextDay)
            BedButton.gameObject.SetActive(true);
            WheelDay.rotation = Quaternion.Euler(0f, 0f, -185);
            ActionText.text = "0";
            sun.transform.rotation = Quaternion.Euler(0, 0, 250);
            sunlight.intensity = 0;
            RenderSettings.ambientSkyColor = nightsky;
            RenderSettings.ambientEquatorColor = nightsky;

        }
    }

   

    public void updateHorloge()
    {
        //sun -20 160
        float rad = 50 - currentAction * (185 / ActionNb);
        WheelDay.rotation = Quaternion.Euler(0f, 0f, rad);
        ActionText.text = (ActionNb - currentAction).ToString();
        float sunRad = (-20 + currentAction * (180 / ActionNb));
        sun.transform.rotation = Quaternion.Euler(0, 0, sunRad);
        sunlight.intensity = 1;
        if(currentAction < ActionNb/2)
        {
            Color col = gradSky.Evaluate(1/(float)ActionNb*(float)currentAction*1.5f);
            print(1 / (float)ActionNb * (float)currentAction * 1.7f);
            RenderSettings.ambientSkyColor = col;
            RenderSettings.ambientEquatorColor = col;
        }
        else
        {
            Color col = gradSky.Evaluate(1 - (1 / (float)ActionNb * ((float)currentAction-5) *1.5f));
            print(1 - (1 / (float)ActionNb * ((float)currentAction - 5) * 1.7f));
            RenderSettings.ambientSkyColor = col;
            RenderSettings.ambientEquatorColor = col;
        }
    }

    public void NextDay()
    {
        //print("on passe au jour suivant");
        currentDay++;
        currentAction = 0;
        updateHorloge();
        BedButton.gameObject.SetActive(false);
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
