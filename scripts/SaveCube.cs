using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCube : MonoBehaviour {

	ConstructCube cubeConstructor;
	int size;
	string infoString = "";
	// Use this for initialization
	void Start () {
		cubeConstructor = FindObjectOfType<ConstructCube>(); // this object contains the list of all the cubes 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SaveCurrentStatus()
	{
		if (infoString != "")
			infoString = "";
		Debug.Log("Save The Cube");
		infoString += cubeConstructor.cubeList[0].GetComponent<CubePiece>().size.ToString() + " ";
		foreach(GameObject x in cubeConstructor.cubeList)
		{
			CubePiece cube = x.GetComponent<CubePiece>();
			infoString += cube.pieceColor + " ";
			if (x.transform.localPosition.x > -0.1f && x.transform.localPosition.x < 0.1f)
				infoString += "0 ";
			else
				infoString += x.transform.localPosition.x.ToString() + " "; 
			if (x.transform.localPosition.y > -0.1f && x.transform.localPosition.y < 0.1f)
				infoString += "0 ";
			else
				infoString += x.transform.localPosition.y.ToString() + " ";
			if (x.transform.localPosition.z > -0.1f && x.transform.localPosition.z < 0.1f)
				infoString += "0 ";
			else
				infoString += x.transform.localPosition.z.ToString() + " ";
			
			infoString += x.transform.forward.x.ToString() + " ";
			infoString += x.transform.forward.y.ToString() + " ";
			infoString += x.transform.forward.z.ToString() + " ";			
			
		}
		PlayerPrefs.SetString("savedCube",infoString);
	}
}
