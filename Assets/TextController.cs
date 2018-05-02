using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour {

    public int scoreText = 10;

	void Update () {
        GetComponent<Text>().text = scoreText.ToString();
	}
}
