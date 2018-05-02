using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFace : MonoBehaviour {

    public float damping = 5f;


    void FixedUpdate()
    {
        Vector3 lookPos = transform.position - Camera.main.transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //rotation.z = Camera.main.transform.rotation.z;
        rotation.x = Camera.main.transform.rotation.x;
        transform.rotation = Camera.main.transform.rotation;
    }
}
