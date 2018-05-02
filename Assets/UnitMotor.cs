using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


public class UnitMotor : MonoBehaviour {

    public float speed = 2;
    public Vector3 target;
    public GameObject targetgm;
    public Text text;
    int score = 0;

    public string team;


    // Use this for initialization
    void Start () {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        target.y = transform.position.y;

        score = int.Parse(text.text);

        if (transform.position == target)
        {
            if (targetgm.GetComponent<NodeController>().team == team)
            {
                targetgm.GetComponent<NodeController>().score += score;
                Destroy(gameObject);
            }

            if (targetgm.GetComponent<NodeController>().team != team)
            {
                targetgm.GetComponent<NodeController>().battle = true;
                targetgm.GetComponent<NodeController>().opponentScore = score;
                targetgm.GetComponent<NodeController>().opponentTeam = team;
                Destroy(gameObject);
            }
        }
    }
}
