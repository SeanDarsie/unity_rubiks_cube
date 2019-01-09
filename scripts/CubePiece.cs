using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePiece : MonoBehaviour {
	public List<Transform> XRow = new List<Transform>();
	public List<Transform> YRow = new List<Transform>();
	public List<Transform> ZRow = new List<Transform>();
	public string pieceColor;
	public int size;
	enum Dir{x,y,z};
	[SerializeField] Dir myDir = Dir.x;
	public Vector3 originalPos;
	[SerializeField] GameObject myHighlight;
	ConstructCube constructCube;
	CubeControlGroup cubeControlx;
	CubeControlGroup cubeControly;
	CubeControlGroup cubeControlz;
	
	// colors????
	// Use this for initialization
	void Start () {
		constructCube = FindObjectOfType<ConstructCube>();
		cubeControlx = GameObject.FindWithTag("XGroup").GetComponent<CubeControlGroup>();
		cubeControly = GameObject.FindWithTag("YGroup").GetComponent<CubeControlGroup>();
		cubeControlz = GameObject.FindWithTag("ZGroup").GetComponent<CubeControlGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.S))
		// {
		// 	Debug.Log(transform.position.ToString());
		// }
	}
	/// <summary>
	/// OnMouseDown is called when the user has pressed the mouse button while
	/// over the GUIElement or Collider.
	/// </summary>
	void OnMouseDown()
	{
		// Debug.Log("I am called: "+name);
		// make either a vertical or horizontal list
		XRow.Clear();
		YRow.Clear();
		ZRow.Clear();
		// Debug.Log(transform.localPosition.y);
		foreach(GameObject x in constructCube.cubeList)
		{
			if (myDir == Dir.x)
			{
				if (Mathf.Abs(x.transform.localPosition.x - transform.localPosition.x) <= 0.1f)
				{
					XRow.Add(x.transform);
				// Debug.Log("XXXXXX");
				}	
			}
			else if (myDir == Dir.y)
			{
				if (Mathf.Abs(x.transform.localPosition.y - transform.localPosition.y) <= 0.1f)
				{
					YRow.Add(x.transform);
					// Debug.Log("YYYYY");
				}
			}
			else if (myDir == Dir.z)
			{
				if (Mathf.Abs(x.transform.localPosition.z - transform.localPosition.z) <= 0.1f)
				{
					ZRow.Add(x.transform);
					// Debug.Log("ZZZZZ");
				}
			}
		}
		XRow.Add(transform);
		YRow.Add(transform);
		ZRow.Add(transform);
		switch(myDir)
		{
			case Dir.x:
				cubeControlx.RemakeListOfKids(XRow);
				cubeControlx.column = transform.position.x;
				break;
			case Dir.y:
				cubeControly.RemakeListOfKids(YRow);
				cubeControly.column = transform.position.y;
				break;
			case Dir.z:
				cubeControlz.RemakeListOfKids(ZRow);
				cubeControlz.column = transform.position.z;
				break;
			default:
				cubeControlx.RemakeListOfKids(XRow);
				break;
		}
		if (myDir == Dir.x)       {myDir = Dir.y;}
		else if (myDir == Dir.y)  {myDir = Dir.z;}
		else                      {myDir = Dir.x;}
	}
	public void Highlight()
	{
		// Debug.Log("Highlight Me!");
		myHighlight.SetActive(true);
	}
	public void UnHighlight()
	{
  		myHighlight.SetActive(false);
	}
}
