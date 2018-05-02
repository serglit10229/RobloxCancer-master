using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NodeController : MonoBehaviour {

    public bool selected = false;
    public GameObject Unit;
    public List<GameObject> Targets;
    public int score = 10;
    public Text textObject;
    public float interval = 1;

    public bool hasFactory = false;
    public bool hasReactor = false;
    public bool hasFort = false;

    public GameObject UI;
    public float time = 0;
    public float boostNum = 0;
    public int fortNum = 0;
    public bool changeSide = false;

    //Multiplayer
    public bool team1 = false;
    public bool team2 = false;
    //public bool team3 = false;
    //public bool Idle = true;

    public string team;
    public bool battle = false;
    public int opponentScore = 0;
    public string opponentTeam;

    public Color team1Color = Color.blue;
    public Color team2Color = Color.red;

    public TeamScore scoreUI;
    //public Color team3Color = Color.red;


    // Use this for initialization
    void Start () {
        UI = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        UI.GetComponent<UIManager>().node = gameObject;
        scoreUI = Object.FindObjectOfType<TeamScore>();
        if (team1 == true)
        {
            team = "team1";
        }
        if (team2 == true)
        {
            team = "team2";
        }

        
        if (team == "team1")
        {
            scoreUI.t1 += score;
        }
        if (team == "team2")
        {
            scoreUI.t2 += score;
        }

    }

    // Update is called once per frame
    void Update()
    {
        /* 
        if(Idle == true)
        {
            team1 = false;
            team2 = false;
            team3 = false;
        }
        
        if(team1 == true || team2 == true || team3 == true)
        {
            Idle = false;
        }
        */
        
        if (selected == true)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.white);
            if (!UI.gameObject.activeSelf)
                UI.gameObject.SetActive(true);
        }
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            
            if(team1 == true)
                rend.material.SetColor("_Color", team1Color);
            if(team2 == true)
                rend.material.SetColor("_Color", team2Color);
            if(team1 == false && team2 == false)
                rend.material.SetColor("_Color", Color.gray);
            if (UI.gameObject.activeSelf)
                UI.gameObject.SetActive(false);
        }

        textObject.text = score.ToString();




        if (battle == true)
        {
            changeSide = true;
            if (opponentScore > 0 || score > 0)
            {
                time += Time.deltaTime;
                if (time >= 0.5f)
                {
                    if (hasFort == true)
                    {
                        fortNum++;
                        if (fortNum < 5)
                        {
                            score--;
                            opponentScore--;

                            scoreUI.t1--;
                            scoreUI.t2--;
                        }
                        if (fortNum == 5)
                        {
                            opponentScore--;
                            fortNum = 0;
                            if (team == "team1")
                            {
                                scoreUI.t2--;
                            }
                            else
                            {
                                scoreUI.t1--;
                            }
                        }
                        time = 0;
                    }
                    else
                    {
                        score--;
                        opponentScore--;

                        scoreUI.t1--;
                        scoreUI.t2--;

                        time = 0;
                    }
                }
            }

            if (opponentScore == 0)
            {
                battle = false;
                changeSide = false;
            }
            if (score == 0)
            {
                score = opponentScore;

                if(hasReactor == true && changeSide == true)
                {
                    foreach (NodeController g in FindObjectsOfType<NodeController>())
                    {
                        if (g.team == team)
                        {
                            g.interval += 0.05f;
                            changeSide = false;
                        }
                        if (g.team != team)
                        {
                            g.interval -= 0.05f;
                            changeSide = false;
                        }
                    }
                }
                if (opponentTeam == "team1")
                {
                    team2 = false;
                    team1 = true;
                    team = "team1";
                }
                if (opponentTeam == "team2")
                {
                    team2 = true;
                    team1 = false;
                    team = "team2";
                }
                changeSide = false;
                battle = false;
            }
            //textObject.text = score.ToString() + "  :  " + opponentScore.ToString();
            textObject.text = "Team 1:" + score.ToString() + "  vs.  " + "Team 2:"+ opponentScore.ToString();
        }
        else
        {
            textObject.text = score.ToString();
            if (hasFactory == true)
            {
                time += Time.deltaTime;
                if (time >= interval)
                {
                    score++;
                    time = 0;
                    if (team == "team1")
                    {
                        scoreUI.t1++;
                    }
                    if (team == "team2")
                    {
                        scoreUI.t2 ++;
                    }
                }
            }
        }
    }

    public void Spawn(GameObject target)
    {
        if (Targets.Contains(target) && score > 0)
        {
            if (battle == true)
            {
                if (opponentTeam == target.GetComponent<NodeController>().team)
                {
                    Vector3 offset = new Vector3(0, 0.5f, 0);
                    Vector3 relativePos = target.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    GameObject spawnedUnit = Instantiate(Unit, transform.position + offset, rotation);
                    spawnedUnit.GetComponent<UnitMotor>().target = target.transform.position;
                    spawnedUnit.GetComponent<UnitMotor>().targetgm = target;
                    spawnedUnit.GetComponent<UnitMotor>().team = opponentTeam;
                    spawnedUnit.GetComponent<UnitMotor>().text.text = score.ToString();
                    score = 0;
                }
            }

            if(battle == false)
            {
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 relativePos = target.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                GameObject spawnedUnit = Instantiate(Unit, transform.position + offset, rotation);
                spawnedUnit.GetComponent<UnitMotor>().target = target.transform.position;
                spawnedUnit.GetComponent<UnitMotor>().targetgm = target;
                spawnedUnit.GetComponent<UnitMotor>().team = team;
                spawnedUnit.GetComponent<UnitMotor>().text.text = score.ToString();
                score = 0;
            }
        }
    }

    void Generate()
    {
        score += 1;
    }
}
