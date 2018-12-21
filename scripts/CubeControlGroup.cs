using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControlGroup : MonoBehaviour {
	List<Transform> kids = new List<Transform>();
	bool groupSelected = false;
	public enum Direction{X,Y,Z};
	public Direction direction;
	Vector3 previousMousePos = new Vector3();
	[SerializeField] Transform cubeParent;

	/// <Debuggery>
	[SerializeField] Transform rotationVisualizer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (groupSelected == true)
		{
			RotatePlease();
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (groupSelected == true)
			{
				AlignToClosestDir();
				// Invoke("AlignToClosestDir",0.5f);
			}

		}
		previousMousePos = Input.mousePosition;
	}
	public void MakeKidsChildren()
	{
		foreach (Transform x in kids)
		{
			x.SetParent(transform);
		}
	}
	public void RemoveChildren()
	{
		// for (int i = 0; i < transform.childCount ; i++)
		// {
		// 	transform.GetChild(i).SetParent(transform.parent);
		// }
		foreach (Transform x in kids)
		{
			x.SetParent(cubeParent);
		}
		kids.Clear();
	}

	public void RemakeListOfKids(List<Transform> blocks)
	{
		RemoveChildren();
		// kids.Clear();
		foreach (Transform x in blocks)
		{
			kids.Add(x);
		}
		MakeKidsChildren();
		HighlightAllKids();
		groupSelected = true;
	}
	public void HighlightAllKids()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).GetComponent<CubePiece>().Highlight();
		}
	}
	public void AlignToClosestDir()
	{
		float angle = ClosestAngle();
		// angle *= (Mathf.PI/ 180);
		Debug.Log("Angle: " + angle);
		switch (direction)
		{
			case Direction.X:
				switch((int)angle)
				{
					case 0: // problem case
						if (transform.eulerAngles.y == 0)
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
							transform.rotation = rot;
						}
						else
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,-1))) - transform.position, Vector3.down);
							transform.rotation = rot;
						}
						break;
					case 90:
							Quaternion rot90 = Quaternion.LookRotation((transform.position + (new Vector3(0,-1,0))) - transform.position, Vector3.up);
							transform.rotation = rot90;
						break;
					case 270:
						Quaternion rot270 = Quaternion.LookRotation((transform.position + (new Vector3(0,1,0))) - transform.position, Vector3.up);
						transform.rotation = rot270;
						break;
					case 360: // problem case this is a 180 case
						if (transform.eulerAngles.y == 0)
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
							transform.rotation = rot;
						}
						else
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,-1))) - transform.position, Vector3.down);
							transform.rotation = rot;
						}
						break;
					default:
						if (transform.eulerAngles.y == 0)
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
							transform.rotation = rot;
						}
						else
						{
							Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,-1))) - transform.position, Vector3.down);
							transform.rotation = rot;
						}
						break;
				}
				
				break;
			case Direction.Y:
				switch((int)angle)
				{
					case 0:
						Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rot;
						break;
					case 90:
						Quaternion rot90 = Quaternion.LookRotation((transform.position + (new Vector3(1,0,0))) - transform.position, Vector3.up);
						transform.rotation = rot90;
						break;
					case 270:
						Quaternion rotNeg90 = Quaternion.LookRotation((transform.position + (new Vector3(-1,0,0))) - transform.position, Vector3.up);
						transform.rotation = rotNeg90;
						break;
					case 180:
						Quaternion rot180 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,-1))) - transform.position, Vector3.up);
						transform.rotation = rot180;
						break;
					case 360:
						Quaternion rot360 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rot360;
						break;
					default:
						Quaternion rotDefault = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rotDefault;
						break;
				}
				// transform.rotation = new Quaternion(0,angle,0,transform.rotation.w);
				// rotationVisualizer.rotation = new Quaternion(0,angle,0,transform.rotation.w);
				break;
			case Direction.Z:
				switch((int)angle)
				{
					case 0:
						Quaternion rot0 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rot0;
						break;
					case 90:
						Quaternion rot90 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, -Vector3.right);
						transform.rotation = rot90;
						break;
					case 270:
						Quaternion rot270 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.right);
						transform.rotation = rot270;
						break;
					case 180:
						Quaternion rot180Z = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.down);
						transform.rotation = rot180Z;
						break;
					case 360:
						Quaternion rot360 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rot360;
						break;
					default:
						Quaternion rotDefault = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rotDefault;
						break;
				}
				break;
			default:
				break;
		}
		groupSelected = false;  
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).GetComponent<CubePiece>().UnHighlight();
		}
		RemoveChildren();
		// for (int i = 0; i < transform.childCount; i++)
		// {
		// 	transform.GetChild(i).GetComponent<CubePiece>().UnHighlight();
		// }
	}
	public void RotatePlease()
	{
		float go = (Input.mousePosition - previousMousePos).magnitude;
		go = (Input.mousePosition.magnitude - previousMousePos.magnitude);
		switch(direction)
		{
			case Direction.X:
			
				// Debug.Log("rotation.x Degree: " + transform.rotation.x *( 180f/Mathf.PI));
				// Debug.Log("rotation.x Radians:" + transform.rotation.ToString());
				Debug.Log("rotation.x Eulers: " + transform.eulerAngles.ToString());
				
				transform.Rotate(transform.right * go);
				break;
			case Direction.Y:
				// Debug.Log("rotation.Y Eulers: " + transform.eulerAngles.y);
				transform.Rotate(transform.up * go);
				break;
			case Direction.Z:
				// Debug.Log("rotation.x Eulers: " + transform.eulerAngles.ToString());
				transform.Rotate(transform.forward * go, Space.World);
				break;
			default:
				break;
		}
	}
	float ClosestAngle()
	{
		switch (direction)
		{
			case Direction.X:
				// find closest multiple of 90
				float currentXAngle = transform.eulerAngles.x;
				// Debug.Log(currentXAngle);
				float angle = 0f;
				while (angle < Mathf.Abs(currentXAngle))
				{
					
					angle += 90f;
					// Debug.Log("Angle -> " + angle);
					// Debug.Log("CurrentXangle ->" + currentXAngle);
				}
				if (currentXAngle > 0)
				{
					// Debug.Log("angle - currentXangle: " + (angle - currentXAngle));
					if (angle - currentXAngle < 45)
					{
						return angle;
					}
					else
						return (angle - 90f);
				}
				else
				{
					if (angle + currentXAngle > 45)
					{
						return 360 - (angle - 90f);
					}
					else 
						return (360 - angle);
				}
				break;
			case Direction.Y:
				float currentYAngle = transform.eulerAngles.y;
				float angley = 0f;
				while (angley < Mathf.Abs(currentYAngle) && angley < 360)
				{
					angley += 90f;
				}
				if (currentYAngle > 0)
				{
					if (angley - currentYAngle > 45f)
					{
						return (angley - 90f);
					}
					else
						return angley;
				}
				else
				{
					if (angley + currentYAngle > 45)
					{
						return (360 - (angley - 90f));
					}
					else 
						return 360 - angley;
				}
				break;
			case Direction.Z:
				float currentZAngle = transform.eulerAngles.z;
				float angleZ = 0f;
				while (angleZ < Mathf.Abs(currentZAngle) && angleZ <= 360)
				{
					angleZ += 90f;
				}
				if (currentZAngle > 0)
				{
					if (angleZ - currentZAngle > 45f)
					{
						return (angleZ - 90f);
					}
					else
						return angleZ;
				}
				else
				{
					if (angleZ + currentZAngle > 45)
					{
						return (360 - (angleZ - 90f));
					}
					else 
						return 360 - angleZ;
				}
				break;
			default:
				break;
		}
		return 0;
	}
}
