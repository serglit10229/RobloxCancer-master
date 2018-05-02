using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSelection : MonoBehaviour {

    public GameObject last;
    public Camera cam;

    public string team = "team1";

    private int fingerID = -1;
    
    private void Awake()
    {
    #if !UNITY_EDITOR
        fingerID = 0; 
    #endif
    
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }

        if (Input.GetMouseButtonDown(1) && last != null)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (EventSystem.current.IsPointerOverGameObject(fingerID) == false)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<NodeController>() != null)
                {
                    Debug.Log(hit.transform.position);
                    last.transform.GetComponent<NodeController>().Spawn(hit.transform.gameObject);
                }
            }
        }
    }

    void Ray() {
        if (EventSystem.current.IsPointerOverGameObject(fingerID) == false)
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (last == null)
                {
                    last = hit.transform.gameObject;
                    if (hit.transform.GetComponent<NodeController>().team == team)
                        hit.transform.GetComponent<NodeController>().selected = true;
                }
                if (hit.transform.GetComponent<NodeController>() == null)
                {
                    last.GetComponent<NodeController>().selected = false;
                    last = null;
                }
                if (last != hit.transform)
                {
                    last.GetComponent<NodeController>().selected = false;
                    if (hit.transform.GetComponent<NodeController>().team == team)
                        hit.transform.GetComponent<NodeController>().selected = true;
                    last = hit.transform.gameObject;
                }
                else
                {
                    if(hit.transform.GetComponent<NodeController>().team == team)
                        hit.transform.GetComponent<NodeController>().selected = true;
                }
            }
        }
        
    }
}