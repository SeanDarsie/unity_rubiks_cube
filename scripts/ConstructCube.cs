using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConstructCube : MonoBehaviour {
	public List<GameObject> cubeList = new List<GameObject>();
	[Range(3,100)]public int size; // force it to be odd?
	public float speedOfCubeCreation = 0.5f;
	public Dictionary<string,GameObject> allCubes = new Dictionary<string,GameObject>();
	string[] colors = new string[6];
	public string[] names;
	public GameObject[] cubePrefabs;
	[SerializeField] Transform mainObject; // the object under which all of the cubes will be childed
	List<List<GameObject>> centerRowsForward = new List<List<GameObject>>(); // blue/green sides
	List<List<GameObject>> centerRowsBackward = new List<List<GameObject>>(); // orange/red sides
	List<List<GameObject>> sideRows = new List<List<GameObject>>(); // for the 4 sides. 
	Vector3 forwardVector = new Vector3();
	bool fromSavedCube = false;
	[HideInInspector] public int sizeOfCurrentCube;
	[SerializeField] Transform controlX;
	[SerializeField] Transform controlY;
	[SerializeField] Transform controlZ;
	List<Transform> XRow = new List<Transform>();
	List<Transform> YRow = new List<Transform>();
	List<Transform> ZRow = new List<Transform>();
	void Start () {
		InitializeCubeDictionary();
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
		// 	ConstructBox(size);
		// 	size+=1;
		// }
		// if (Input.GetKeyDown(KeyCode.Return))
		// {
		// 	for (int i = 3; i < 15;i += 1)
		// 	{
		// 		ConstructBox(i);
		// 	}
		// }
		if (Input.GetKeyDown(KeyCode.L))
		{
			BuildSavedCube();
		}
	}
	public void ConstructCubeQuickly(int size)
	{
		sizeOfCurrentCube = size;
		controlX.rotation = Quaternion.identity;
		controlY.rotation = Quaternion.identity;
		controlZ.rotation = Quaternion.identity;
		cubeList.Clear();
		if (size%2 == 0)
		{
			if (fromSavedCube == false)
				{
					PlayerPrefs.SetString("savedCube", "");
					FindObjectOfType<Moves>().moves.Clear();
				}
			fromSavedCube = false;
			BuildFrontFaceEven(size);
			// StartCoroutine(BuildFrontFaceEvenCo(size));
		}
		else
		{
			if (fromSavedCube == false)
			{
				PlayerPrefs.SetString("savedCube", "");
				FindObjectOfType<Moves>().moves.Clear();
			}
			fromSavedCube = false;
			BuildFrontFaceOdd(size);
			// StartCoroutine(BuildFrontFaceOddCo(size));
		}

	}
	public void ConstructCubeSlowly(int size)
	{
		cubeList.Clear();
		if (size%2 == 0)
		{
			// BuildFrontFaceEven(size);
			StartCoroutine(BuildFrontFaceEvenCo(size));
		}
		else
		{
			// BuildFrontFaceOdd(size);
			StartCoroutine(BuildFrontFaceOddCo(size));
		}
	}
	void BuildFrontFaceOdd(int size)
	{
		// so let's assume it's odd. That means the center center cube will be at (size -2)/2 + 0.5
		// how do we get the rest of the pieces?
		for (int i = 0; i < colors.Length; i++)
		{
			for (int y = (size / 2); y >= (-size/2); y--) //((size / 2) + 1)
			{
				for(int x = (size / 2); x >= -(size/2); x--)
				{
					// check for edge cases. literally. there are 8 or them!!!! shit. or literally 1;
					// if (y == Mathf.Abs(size/2) || x == Mathf.Abs(size/2))
					// {
						// send the info to the edge case handling function
						HandleEdgeCaseOdd(x,y,size,colors[i]);
					// }
					// yield return new WaitForSeconds(speedOfCubeCreation);
				}
			}
		}
		// yield return null;

	}
	IEnumerator BuildFrontFaceOddCo(int size)
	{
		// so let's assume it's odd. That means the center center cube will be at (size -2)/2 + 0.5
		// how do we get the rest of the pieces?
		for (int i = 0; i < colors.Length; i++)
		{
			for (int y = (size / 2); y >= (-size/2); y--) //((size / 2) + 1)
			{
				for(int x = (size / 2); x >= -(size/2); x--)
				{
					// check for edge cases. literally. there are 8 or them!!!! shit. or literally 1;
					// if (y == Mathf.Abs(size/2) || x == Mathf.Abs(size/2))
					// {
						// send the info to the edge case handling function
						HandleEdgeCaseOdd(x,y,size,colors[i]);
					// }
					yield return new WaitForSeconds(speedOfCubeCreation);
				}
			}
		}
		yield return null;

	}
	void BuildFrontFaceEven(int size)
	{
		// so let's assume it's odd. That means the center center cube will be at (size -2)/2 + 0.5
		// how do we get the rest of the pieces?
		for (int i = 0; i < colors.Length; i++)
		{
			for (int y = (size / 2); y >= (-size/2); y--) //((size / 2) + 1)
			{
				if (y == 0)
					continue;
				for(int x = (size / 2); x >= -(size/2); x--)
				{
					if (x == 0)
						continue;
					// check for edge cases. literally. there are 8 or them!!!! shit. or literally 1;
					// if (y == Mathf.Abs(size/2) || x == Mathf.Abs(size/2))
					// {
						// send the info to the edge case handling function
						HandleEdgeCaseEven((float)x,(float)y,size,colors[i]);
					// }
					// yield return new WaitForSeconds(speedOfCubeCreation);
				}
			}
		}
		// yield return null;

	}
	IEnumerator BuildFrontFaceEvenCo(int size)
	{
		// so let's assume it's odd. That means the center center cube will be at (size -2)/2 + 0.5
		// how do we get the rest of the pieces?
		for (int i = 0; i < colors.Length; i++)
		{
			for (int y = (size / 2); y >= (-size/2); y--) //((size / 2) + 1)
			{
				if (y == 0)
					continue;
				for(int x = (size / 2); x >= -(size/2); x--)
				{
					if (x == 0)
						continue;
					// check for edge cases. literally. there are 8 or them!!!! shit. or literally 1;
					// if (y == Mathf.Abs(size/2) || x == Mathf.Abs(size/2))
					// {
						// send the info to the edge case handling function
						HandleEdgeCaseEven((float)x,(float)y,size,colors[i]);
					// }
					yield return new WaitForSeconds(speedOfCubeCreation);
				}
			}
		}
		yield return null;

	}
	public void PlaceCube(GameObject cube, Vector3 position,int size)
	{
		GameObject theCube = Instantiate(cube, position,Quaternion.identity);
		theCube.transform.parent = mainObject;
		theCube.transform.localPosition = position;
		theCube.GetComponent<CubePiece>().originalPos = theCube.transform.localPosition;
		theCube.GetComponent<CubePiece>().size = size;
		if (fromSavedCube == true)
		{
			Quaternion rot = Quaternion.LookRotation((transform.position + forwardVector) - transform.position,Vector3.right);
			theCube.transform.rotation = rot;
		}
		cubeList.Add(theCube);
		// theCube.transform.rotation = mainObject.rotation;
	}
	/// <CASE:ODD>
	void HandleEdgeCaseOdd(int x, int y, int size, string side) // side will be based on color. 
	{
		switch(side)
		{
			case "green": // handles all corners and sides.
				if (x == (size / 2) && y == (size / 2))
				{
					// top left
					// allCubes["WRG"].SetActive(true);
					PlaceCube(allCubes["WGO"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == (size / 2) && y == -(size / 2))
				{
					// bottom Left corner YRG
					PlaceCube(allCubes["YGO"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && y == -(size / 2))
				{
					// bottom right corner YGO
					PlaceCube(allCubes["YRG"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && y == (size / 2))
				{
					// top right corner WGO
					PlaceCube(allCubes["WRG"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == (size / 2) && Mathf.Abs(y) < (size / 2))
				{
					//left edge RTG
					PlaceCube(allCubes["GTO"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2))
				{
					// right edge GTO
					PlaceCube(allCubes["RTG"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2))
				{
					// top edge WTG
					PlaceCube(allCubes["WTG"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2))
				{
					// bottom edge YTG
					PlaceCube(allCubes["YTG"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else
				{
					PlaceCube(allCubes["G"], new Vector3(x,y,(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				break;
			case "orange": // x = Z and y = y and z = x
				if (x == (size / 2)) // is on the left edge and should already have been placed
					break;
				else if (x == -(size / 2) && y == (size / 2)) // top right
				{
					PlaceCube(allCubes["WOB"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				else if (x == -(size / 2) && y == -(size / 2)) // bottom right
				{
					PlaceCube(allCubes["YOB"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2)) // right edge
				{
					PlaceCube(allCubes["OTB"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2)) // top edge
				{
					PlaceCube(allCubes["WTO"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2)) // bottom edge
				{
					PlaceCube(allCubes["YTO"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				else
				{
					PlaceCube(allCubes["O"],new Vector3((float)(((float)size - 2) / 2) + 0.5f,y,x),size);
				}
				break;
			case "blue": // x = -x y = y z = -z
				if (x == (size / 2)) // whole left edge
					break;
				else if (x == -(size / 2) && y == (size / 2)) // top right corner
				{
					PlaceCube(allCubes["WBR"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (x == -(size / 2) && y == -(size / 2))// bottom right corner
				{
					PlaceCube(allCubes["YBR"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2)) // right edge
				{
					PlaceCube(allCubes["BTR"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2)) // top edge
				{
					PlaceCube(allCubes["WTB"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2)) // bottom edge
				{
					PlaceCube(allCubes["YTB"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else
				{
					PlaceCube(allCubes["B"], new Vector3(x,y,-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				break;
			case "red": // x = z and z = x
				if (Mathf.Abs(x) == (size / 2))
				{
					break;
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2)) // top edge
				{
					PlaceCube(allCubes["WTR"],new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y,x),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2))
				{
					PlaceCube(allCubes["YTR"],new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y,x),size);
				}
				else // center piece
				{
					PlaceCube(allCubes["R"],new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y,x),size);
				}
				break;
			case "white": // y = z and x = x and z = y
				if (Mathf.Abs(x) == (size / 2) || Mathf.Abs(y) == (size / 2))
				{
					break;
				}
				else
				{
					PlaceCube(allCubes["W"],new Vector3(y,(float)(((float)size - 2) / 2) + 0.5f,x),size);
				}
				break;
			case "yellow": // y = z and x = x and z = y
				if (Mathf.Abs(x) == (size / 2) || Mathf.Abs(y) == (size / 2))
				{
					break;
				}
				else
				{
					PlaceCube(allCubes["Y"],new Vector3(y,-(float)(((float)size - 2) / 2) - 0.5f,x),size);
				}
				break;
			default:
				break;
		}
	}

	/// <CASE:EVEN>
	void HandleEdgeCaseEven(float x, float y, int size, string side) // side will be based on color. 
	{
		switch(side)
		{
			case "green": // handles all corners and sides.
				if (x == (size / 2) && y == (size / 2))
				{
					// top left
					// allCubes["WRG"].SetActive(true);
					PlaceCube(allCubes["WGO"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == (size / 2) && y == -(size / 2))
				{
					// bottom Left corner YRG
					PlaceCube(allCubes["YGO"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && y == -(size / 2))
				{
					// bottom right corner YGO
					PlaceCube(allCubes["YRG"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && y == (size / 2))
				{
					// top right corner WGO
					PlaceCube(allCubes["WRG"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == (size / 2) && Mathf.Abs(y) < (size / 2))
				{
					//left edge RTG
					PlaceCube(allCubes["GTO"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2))
				{
					// right edge GTO
					PlaceCube(allCubes["RTG"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2))
				{
					// top edge WTG
					PlaceCube(allCubes["WTG"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2))
				{
					// bottom edge YTG
					PlaceCube(allCubes["YTG"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				else
				{
					PlaceCube(allCubes["G"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f),size);
				}
				break;
			case "orange": // x = z  and z = x
				if (x == (size / 2))
					break;
				else if (x == -(size / 2) && y == (size / 2)) // top right
				{
					PlaceCube(allCubes["WOB"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else if (x == -(size / 2) && y == -(size / 2))// bottom right
				{
					PlaceCube(allCubes["YOB"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2)) // right edge
				{
					PlaceCube(allCubes["OTB"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2)) // top edge
				{
					PlaceCube(allCubes["WTO"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2)) // bottom
				{
					PlaceCube(allCubes["YTO"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else //center
				{
					PlaceCube(allCubes["O"], new Vector3((float)(((float)size - 2) / 2) + 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				break;
			case "blue": // z = -z and x  = -x
				if (x == (size / 2)) // left edge
					break;
				else if (x == -(size / 2) && y == (size / 2)) // top right
				{
					PlaceCube(allCubes["WBR"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (x == -(size / 2) && y == -(size / 2)) // bottom right
				{
					PlaceCube(allCubes["YBR"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (x == -(size / 2) && Mathf.Abs(y) < (size / 2)) //right edge
				{
					PlaceCube(allCubes["BTR"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size/2)) // top edge
				{
					PlaceCube(allCubes["WTB"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size/2)) // bottom edge
				{
					PlaceCube(allCubes["YTB"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				else // center
				{
					PlaceCube(allCubes["B"], new Vector3(x + ((x/Mathf.Abs(x)) * -0.5f),y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f),size);
				}
				break;
			case "red": // z = x and x = z
				if (x == (size / 2) || x == - (size / 2)) // right and left sides
				{
					break;
				}
				else if (y == (size / 2) && Mathf.Abs(x) < (size / 2)) // top edge
				{
					PlaceCube(allCubes["WTR"], new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else if (y == -(size / 2) && Mathf.Abs(x) < (size / 2)) // bottom edge
				{
					PlaceCube(allCubes["YTR"], new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				else // center
				{
					PlaceCube(allCubes["R"], new Vector3(-(float)(((float)size - 2) / 2) - 0.5f,y + ((y/Mathf.Abs(y)) * -0.5f),x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				break;
			case "white":
				if (Mathf.Abs(x) == (size / 2) || Mathf.Abs(y) == (size / 2))
				{
					break;
				}
				else
				{
					PlaceCube(allCubes["W"], new Vector3(y + ((y/Mathf.Abs(y)) * -0.5f),(float)(((float)size - 2) / 2) + 0.5f,x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				break;
			case "yellow":
				if (Mathf.Abs(x) == (size / 2) || Mathf.Abs(y) == (size / 2))
				{
					break;
				}
				else
				{
					PlaceCube(allCubes["Y"], new Vector3(y + ((y/Mathf.Abs(y)) * -0.5f),-(float)(((float)size - 2) / 2) - 0.5f,x + ((x/Mathf.Abs(x)) * -0.5f)),size);
				}
				break;
			default:
				break;
		}
	}

	public void BuildSavedCube()
	{
		fromSavedCube = true;
		// retrieve info from player settings
		string infoString = PlayerPrefs.GetString("savedCube");
		if (infoString == "")
			{
				Debug.Log("No saved data for the cube");
				return;
			}
		Debug.Log(PlayerPrefs.GetString("savedCube"));
		char[] cubeData = infoString.ToCharArray();
		int index = 0;
		int size = 0;
		string number = "";

		List<string> directions = new List<string>();
		List<float> columns = new List<float>();
		List<float> angles = new List<float>();

		foreach(char c in cubeData)
		{
			if (c == 'x' || c == 'y' || c == 'z')
			{
				string dir = "" + c;
				directions.Add(dir);
			}
			// index++;
		}
		// Debug.Log("finished reading cube data");
		index = 0;
		while (cubeData[index] != ' ')
		{	
			number += cubeData[index];
			index++;
		}
		int.TryParse(number,out size);
		string column = "";
		string angle = "";
		// float currentColumn = 0;
		// float currentAngle = 0;
		bool angleColumnSwitch = true;

		for (int i = index; i < cubeData.Length; i++)
		{
			if (cubeData[i] != ' ' && cubeData[i] != 'x' && cubeData[i] != 'y' && cubeData[i] != 'z')
			{
				if (angleColumnSwitch == true)
					column += cubeData[i];
				else
					angle += cubeData[i];
				if (cubeData[i + 1] == ' ')
				{
					if (angleColumnSwitch == true)
					{
						float newColumn = 0;
						float.TryParse(column,out newColumn);
						columns.Add(newColumn);
					}
					else
					{
						float newAngle = 0;
						float.TryParse(angle,out newAngle);
						angles.Add(newAngle);
						
					}
					angleColumnSwitch = !angleColumnSwitch;
				}	
			}
			else
			{
				column = "";
				angle = "";
			}

		}
		// Debug.Log("There are "+directions.Count+" directions");
		// for (int k = 0; k < directions.Count; k++)
		// {
		// 	Debug.Log(directions[k]);
			
		// }
		// Debug.Log("There are "+columns.Count+" Columns");
		// for (int k = 0; k < columns.Count; k++)
		// {
		// 	Debug.Log(columns[k].ToString() + " ");
			
		// }
		// Debug.Log("There are "+angles.Count+" angles");
		// for (int k = 0; k < angles.Count; k++)
		// {
		// 	Debug.Log(angles[k].ToString() + " ");
			
		// }
		DestroyAllCubes();
		FindObjectOfType<RotateMe>().PerfectDist((float)size);
		FindObjectOfType<RotateMe>().upSpeed = 7 * (float)size;
		StopAllCoroutines();
		ConstructCubeQuickly(size);
		controlX.rotation = Quaternion.identity;
		controlY.rotation = Quaternion.identity;
		controlZ.rotation = Quaternion.identity;
		StartCoroutine(RepeatMoves(directions,columns,angles));
	}

	void InitializeCubeDictionary()
	{
		for (int i = 0; i < names.Length; i++)
		{
			allCubes[names[i]] = cubePrefabs[i];
		}
		colors[0] = "green";
		colors[1] = "orange";
		colors[2] = "blue";
		colors[3] = "red";
		colors[4] = "white";
		colors[5] = "yellow";
	}
	IEnumerator RepeatMoves(List<string> directions, List<float> columns,List<float> angles)
	{
		
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < directions.Count; i++)
		{
			Debug.Log("Direction: "+directions[i]+" Column: "+columns[i]+" Angle: "+angles[i]);
			AssignChildren(directions[i],columns[i],angles[i]);
			yield return new WaitForEndOfFrame();
			DoAMove(directions[i],columns[i], angles[i]);
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
	public void DestroyAllCubes()
	{
		CubePiece[] allLitteCubes = FindObjectsOfType<CubePiece>();
		foreach(CubePiece x in allLitteCubes)
		{
			Destroy(x.gameObject);
		}
	}

	void AssignChildren(string axis,float column,float angle)
	{
		// controlX.rotation = Quaternion.identity;
		// controlY.rotation = Quaternion.identity;
		// controlZ.rotation = Quaternion.identity;

		XRow.Clear();
		YRow.Clear();
		ZRow.Clear();
		// Debug.Log(transform.localPosition.y);
		foreach(GameObject x in cubeList)
		{
			if (axis == "x")
			{
				// Debug.Log(column.ToString());
				if (Mathf.Abs(x.transform.localPosition.x - column) <= 0.1f)
				{
					// Debug.Log(column.ToString());
					// Debug.Log(axis);
					XRow.Add(x.transform);
				Debug.Log("XXXXXX");
				}	
			}
			else if (axis == "y")
			{
				if (Mathf.Abs(x.transform.localPosition.y - column) <= 0.1f)
				{
					YRow.Add(x.transform);
					Debug.Log("YYYYY");
				}
			}
			else if (axis == "z")
			{
				if (Mathf.Abs(x.transform.localPosition.z - column) <= 0.1f)
				{
					ZRow.Add(x.transform);
					Debug.Log("ZZZZZ");
				}
			}
		}
	}
	void DoAMove(string axis, float column,float angle)
	{
		// controlX.rotation = Quaternion.identity;
		// controlY.rotation = Quaternion.identity;
		// controlZ.rotation = Quaternion.identity;

		// XRow.Clear();
		// YRow.Clear();
		// ZRow.Clear();
		// // Debug.Log(transform.localPosition.y);
		// foreach(GameObject x in cubeList)
		// {
		// 	if (axis == "x")
		// 	{
		// 		// Debug.Log(column.ToString());
		// 		if (Mathf.Abs(x.transform.localPosition.x - column) <= 0.1f)
		// 		{
		// 			// Debug.Log(column.ToString());
		// 			// Debug.Log(axis);
		// 			XRow.Add(x.transform);
		// 		Debug.Log("XXXXXX");
		// 		}	
		// 	}
		// 	else if (axis == "y")
		// 	{
		// 		if (Mathf.Abs(x.transform.localPosition.y - column) <= 0.1f)
		// 		{
		// 			YRow.Add(x.transform);
		// 			Debug.Log("YYYYY");
		// 		}
		// 	}
		// 	else if (axis == "z")
		// 	{
		// 		if (Mathf.Abs(x.transform.localPosition.z - column) <= 0.1f)
		// 		{
		// 			ZRow.Add(x.transform);
		// 			Debug.Log("ZZZZZ");
		// 		}
		// 	}
		// }
		// Debug.Log("Assigned "+XRow.Count.ToString() + " cubes to move");
		// Debug.Log("Assigned "+YRow.Count.ToString() + " cubes to move");

		// XRow.Add(transform);
		// YRow.Add(transform);
		// ZRow.Add(transform);
		// switch(axis)
		// {
		// 	case "x":
		// 		controlX.GetComponent<CubeControlGroup>().RemakeListOfKids(XRow);
		// 		// cubeControlx.column = transform.position.x;
		// 		break;
		// 	case "y":
		// 		controlY.GetComponent<CubeControlGroup>().RemakeListOfKids(YRow);
		// 		// cubeControlx.column = transform.position.y;
		// 		break;
		// 	case "z":
		// 		controlZ.GetComponent<CubeControlGroup>().RemakeListOfKids(ZRow);
		// 		// cubeControlx.column = transform.position.z;
		// 		break;
		// 	default:
		// 		// cubeControlx.RemakeListOfKids(XRow);
		// 		break;
		// }
		switch (axis)
		{
			case "x":
			foreach(Transform x in XRow)
				{
					x.SetParent(controlX);
					// Debug.Log("banana");
				}
			// controlX.GetComponent<CubeControlGroup>().RemakeListOfKids(XRow);
			// moves.AddToMovesList("x",column,angle);
				switch((int)angle)
				{
					
					case 0: // problem case
						Debug.Log("X trying to BROTATE " + angle.ToString()+" degrees");
						Quaternion rot = Quaternion.LookRotation((controlX.position + (new Vector3(0,0,1))) - controlX.position, Vector3.up);
						controlX.rotation = rot;
						break;
					case 90:
							Debug.Log("X trying to rotate " + angle.ToString()+" degrees");
							Quaternion rot90 = Quaternion.LookRotation((controlX.position + (new Vector3(0,-1,0))) - controlX.position, Vector3.up);
							controlX.rotation = rot90;
						break;
					case 270:
						Debug.Log("X trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot270 = Quaternion.LookRotation((controlX.position + (new Vector3(0,1,0))) - controlX.position, Vector3.up);
						controlX.rotation = rot270;
						break;
					case 180: // problem case this is a 180 case
						Debug.Log("X trying to rotate " + angle.ToString()+" degrees");
						
							Quaternion rot180 = Quaternion.LookRotation((controlX.position + (new Vector3(0,0,-1))) - controlX.position, Vector3.down);
							controlX.rotation = rot180;
						
						break;
					default:
						Debug.Log("default X rotation");
						break;
				}
				break;
			case "y":
			foreach(Transform x in YRow)
				x.SetParent(controlY);
			// controlY.GetComponent<CubeControlGroup>().RemakeListOfKids(YRow);
			// moves.AddToMovesList("y",column,angle);
				switch((int)angle)
				{
					case 0:
						Debug.Log("Y trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot = Quaternion.LookRotation((controlY.position + (new Vector3(0,0,1))) - controlY.position, Vector3.up);
						controlY.rotation = rot;
						break;
					case 90:
						Debug.Log("Y trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot90 = Quaternion.LookRotation((controlY.position + (new Vector3(1,0,0))) - controlY.position, Vector3.up);
						controlY.rotation = rot90;
						break;
					case 270:
						Debug.Log("Y trying to rotate " + angle.ToString()+" degrees");
						Quaternion rotNeg90 = Quaternion.LookRotation((controlY.position + (new Vector3(-1,0,0))) - controlY.position, Vector3.up);
						controlY.rotation = rotNeg90;
						break;
					case 180:
						Debug.Log("Y trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot180 = Quaternion.LookRotation((controlY.position + (new Vector3(0,0,-1))) - controlY.position, Vector3.up);
						controlY.rotation = rot180;
						break;
					case 360:
						Debug.Log("Y trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot360 = Quaternion.LookRotation((controlY.position + (new Vector3(0,0,1))) - controlY.position, Vector3.up);
						controlY.rotation = rot360;
						break;
					default:
						Debug.Log("default Y rotation");
						// Quaternion rotDefault = Quaternion.LookRotation((controlY.position + (new Vector3(0,0,1))) - controlY.position, Vector3.up);
						// controlY.rotation = rotDefault;
						break;
				}
				// transform.rotation = new Quaternion(0,angle,0,transform.rotation.w);
				// rotationVisualizer.rotation = new Quaternion(0,angle,0,transform.rotation.w);
				break;
			case "z":
			foreach(Transform x in ZRow)
				x.SetParent(controlZ);
			// controlZ.GetComponent<CubeControlGroup>().RemakeListOfKids(ZRow);
			// moves.AddToMovesList("z",column,angle);
				switch((int)angle)
				{
					case 0:
						Debug.Log("Z trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot0 = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, Vector3.up);
						controlZ.rotation = rot0;
						break;
					case 90:
						Debug.Log("Z trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot90 = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, -Vector3.right);
						controlZ.rotation = rot90;
						break;
					case 270:
						Debug.Log("Z trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot270 = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, Vector3.right);
						controlZ.rotation = rot270;
						break;
					case 180:
						Debug.Log("Z trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot180Z = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, Vector3.down);
						controlZ.rotation = rot180Z;
						break;
					case 360:
						Debug.Log("Z trying to rotate " + angle.ToString()+" degrees");
						Quaternion rot360 = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, Vector3.up);
						controlZ.rotation = rot360;
						break;
					default:
						Debug.Log("default Z rotation");
						// Quaternion rotDefault = Quaternion.LookRotation((controlZ.position + (new Vector3(0,0,1))) - controlZ.position, Vector3.up);
						// controlZ.rotation = rotDefault;
						break;
				}
				break;
			default:
				break;
		}
		foreach(Transform x in XRow)
			x.SetParent(mainObject);
		foreach(Transform x in YRow)
			x.SetParent(mainObject);
		foreach(Transform x in ZRow)
			x.SetParent(mainObject);
	}
}