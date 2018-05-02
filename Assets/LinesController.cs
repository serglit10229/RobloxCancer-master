using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesController : MonoBehaviour {

	public List<GameObject> nodes;

	void Update()
	{
		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity);
		foreach (Collider co in hitColliders)
		{
			if(!nodes.Contains(co.gameObject))
				nodes.Add(co.gameObject);	
		}
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    /* 
	void Update()
	{
		


		if(nodes[0].GetComponent<NodeController>().team1 == nodes[1].GetComponent<NodeController>().team1)
		{
			Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.blue);
		}
		if(nodes[0].GetComponent<NodeController>().team2 == nodes[1].GetComponent<NodeController>().team2)
		{
			Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.green);
		}
	}
	*/
}
