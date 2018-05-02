using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject node;

    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;
    public GameObject image;
    public GameObject panel;
    public GameObject line;

    Vector3 offset = new Vector3(-0.4f,0.2f,-0.6f);
    public GameObject clone;

    public GameObject fn1;
    public GameObject fn2;
    public GameObject fn3;
    public GameObject fn4;


    void Start () {

    }

    public void Function1()
    {
        if(clone == null)
        {
            clone = Instantiate(fn1, node.transform.position + offset, Quaternion.Euler(-90,0,0));
            clone.transform.parent = node.transform;
            node.GetComponent<NodeController>().hasReactor = true;

            foreach (NodeController g in FindObjectsOfType<NodeController>())
            {
                if (g.team == node.GetComponent<NodeController>().team)
                {
                    g.interval -= 0.05f;
                }
            }
        }
    
    }

    public void Function2()
    {
        if (clone == null)
        {
            clone = Instantiate(fn2, node.transform.position + (new Vector3(0,0.25f,0)), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            clone.transform.parent = node.transform;
            node.GetComponent<NodeController>().hasFactory = true;
        }
    }

    public void Function3()
    {
        if (clone == null)
        {
            clone = Instantiate(fn3, node.transform.position + (new Vector3(0, 0.25f, 0)), Quaternion.Euler(0, 0, 0));
            clone.transform.parent = node.transform;
            node.GetComponent<NodeController>().hasFort = true;
            clone.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0); 
        }
    }

    public void Function4()
    {
        if (clone == null)
        {
            clone = Instantiate(fn4, node.transform.position + (new Vector3(0, 0.25f, 0)), Quaternion.Euler(0, 0, 0));
            clone.transform.parent = node.transform;
        }
    }
}
