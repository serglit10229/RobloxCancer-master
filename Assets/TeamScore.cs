using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TeamScore : MonoBehaviour {

    public int t1;
    public int t2;


    public Text tt1;
    public Text tt2;

    private void Update()
    {
        tt1.text = "Team 1" + t1.ToString();
        tt2.text = "Team 2" + t2.ToString();
    }
}
