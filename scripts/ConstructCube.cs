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
	// [SerializeField] GameObject centerPiece;
	// [SerializeField] GameObject greenCenter;
	// [SerializeField] GameObject blueCenter;
	// [SerializeField] GameObject redCenter;
	// [SerializeField] GameObject orangeCenter;
	// [SerializeField] GameObject whiteCenter;
	// [SerializeField] GameObject yellowCenter;
	// [SerializeField] GameObject whiteToOrnage;
	// [SerializeField] GameObject whiteToRed;
	// [SerializeField] GameObject whiteToGreen;
	// [SerializeField] GameObject whiteToBlue;
	// [SerializeField] GameObject yellowToOrange;
	// [SerializeField] GameObject yellowToRed;
	// [SerializeField] GameObject yellowToGreen;
	// [SerializeField] GameObject yellowToBlue;
	// [SerializeField] GameObject whiteRedGreen;
	// [SerializeField] GameObject whiteOrangeGreen;
	// [SerializeField] GameObject whiteOrangeBlue;
	// [SerializeField] GameObject whiteRedBlue;
	// [SerializeField] GameObject yellowRedBlue;
	// [SerializeField] GameObject yellowRedGreen;
	// [SerializeField] GameObject yellowOrangeBlue;
	// [SerializeField] GameObject yellowOrangeGreen;
	// [SerializeField] GameObject redToBlue;
	// [SerializeField] GameObject redToGreen;
	// [SerializeField] GameObject orangeToBlue;
	// [SerializeField] GameObject orangeToGreen;
	// [SerializeField] GameObject ;
	List<List<GameObject>> centerRowsForward = new List<List<GameObject>>(); // blue/green sides
	List<List<GameObject>> centerRowsBackward = new List<List<GameObject>>(); // orange/red sides
	List<List<GameObject>> sideRows = new List<List<GameObject>>(); // for the 4 sides. 

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
	}
	public void ConstructCubeQuickly(int size)
	{
		cubeList.Clear();
		if (size%2 == 0)
		{
			BuildFrontFaceEven(size);
			// StartCoroutine(BuildFrontFaceEvenCo(size));
		}
		else
		{
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
}
