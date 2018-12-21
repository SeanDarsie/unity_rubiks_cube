using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSolving : MonoBehaviour {
	ConstructCube constructCube;
	// Use this for initialization
	void Start () {
		constructCube = FindObjectOfType<ConstructCube>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public bool CheckIfSolved()
	{
		foreach(GameObject x in constructCube.cubeList)
		{
			if (x.transform.localPosition != x.GetComponent<CubePiece>().originalPos)
			{
				return false;
			}
		}
		return true;
	}

}
