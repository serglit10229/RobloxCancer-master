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

    public GameObject delPanel;
    public GameObject turretAttack;
    public GameObject turretTarget;
    public bool attack = false;

    Vector3 offset = new Vector3(-0.4f,0.2f,-0.6f);
    public GameObject clone;

    public GameObject fn1;
    public GameObject fn2;
    public GameObject fn3;
    public GameObject fn4;

    bool afterDelay = false;
    public float time = 0;
    private void Awake()
    {
        delPanel = transform.GetChild(6).gameObject;
        line = transform.GetChild(7).gameObject;
        turretAttack = transform.GetChild(8).gameObject;
    }

    void Update()
    {
        if (clone != null)
        {
            btn1.SetActive(false);
            btn2.SetActive(false);
            btn3.SetActive(false);
            btn4.SetActive(false);
            image.SetActive(false);
            panel.SetActive(false);
            line.SetActive(true);
            if (node.GetComponent<NodeController>().hasTurret)
            {
                turretAttack.SetActive(true);
            }
            delPanel.SetActive(true);
        }
        if (clone == null)
        {
            btn1.SetActive(true);
            btn2.SetActive(true);
            btn3.SetActive(true);
            btn4.SetActive(true);
            image.SetActive(true);
            panel.SetActive(true);
            line.SetActive(true);
            if (node.GetComponent<NodeController>().hasTurret)
            {
                turretAttack.SetActive(false);
            }
            delPanel.SetActive(false);
        }
        if (line != null)
        {
            line.GetComponent<LineRenderer>().SetPosition(0, line.transform.position);
            Vector3 offset = node.transform.up * (node.transform.localScale.z) * -1f;
            line.GetComponent<LineRenderer>().SetPosition(1, node.transform.position + offset);
        }

        if (attack == true)
        {
            time += Time.deltaTime;
            if (time >= 5f)
            {
                Fire();
                time = 0;
            }
        }
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

            node.GetComponent<NodeController>().hasTurret = true;
        }
    }

    public void Delete()
    {
        if (node.GetComponent<NodeController>().hasReactor == true)
        {
            node.GetComponent<NodeController>().hasReactor = false;

            foreach (NodeController g in FindObjectsOfType<NodeController>())
            {
                if (g.team == node.GetComponent<NodeController>().team)
                {
                    g.interval += 0.05f;
                }
            }
            Destroy(clone);
            clone = null;
        }

        if (node.GetComponent<NodeController>().hasFactory == true)
        {
            node.GetComponent<NodeController>().hasFactory = false;

            Destroy(clone);
            clone = null;
        }

        if (node.GetComponent<NodeController>().hasFort == true)
        {
            node.GetComponent<NodeController>().hasFort = false;

            Destroy(clone);
            clone = null;
        }

        if (node.GetComponent<NodeController>().hasTurret == true)
        {
            node.GetComponent<NodeController>().hasTurret = false;

            Destroy(clone);
            clone = null;
        }
    }

    public void TurretAttack()
    {
        Camera.main.GetComponent<NodeSelection>().turretAttack = true;
    }

    public void Fire()
    {
        clone.transform.GetChild(1).gameObject.SetActive(true);
        turretTarget.GetComponent<NodeController>().score--;
        StartCoroutine("delay");
        if (afterDelay == true)
        {
            clone.transform.GetChild(7).gameObject.SetActive(false);
            afterDelay = false;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        afterDelay = true;
    }
}
