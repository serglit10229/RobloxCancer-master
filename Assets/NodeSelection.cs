using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSelection : MonoBehaviour {

    public GameObject last;
    public Camera cam;

    public string team = "team1";

    private int fingerID = -1;

    public bool nikitanusMode = true;

    private int select = 0;
    private int attack = 0;

    public bool turretAttack = false;

    private void Awake()
    {
    #if !UNITY_EDITOR
        fingerID = 0; 
    #endif
    
    }
	// Use this for initialization
	void Start () {
        if (nikitanusMode == true)
        {
            //select = 
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }

        if (Input.GetMouseButtonDown(1) && last != null && turretAttack == false)
        {
            Attack();
            Debug.Log("Attack");
        }

        if (Input.GetMouseButtonDown(1) && last != null && turretAttack == true)
        {
            Turret();
            Debug.Log("Turret");
        }
    }

    void Turret()
    {
        if (EventSystem.current.IsPointerOverGameObject(fingerID) == false)
        {
            RaycastHit hit3;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit3))
            {
                if (hit3.transform.GetComponent<NodeController>() != null && last.transform.GetComponent<NodeController>().OriginalTargets.Contains(hit3.transform.gameObject) && last.transform.GetComponent<NodeController>().team != team)
                {
                    Debug.Log(hit3.transform.position);
                    last.transform.GetComponent<NodeController>().UI.GetComponent<UIManager>().clone.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit3.transform.position - last.transform.GetComponent<NodeController>().UI.GetComponent<UIManager>().clone.transform.GetChild(2).position), Time.deltaTime);
                    last.transform.GetComponent<NodeController>().UI.GetComponent<UIManager>().attack = true;
                    last.transform.GetComponent<NodeController>().UI.GetComponent<UIManager>().turretTarget = hit3.transform.gameObject;
                }
            }
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