using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAngleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			settoangle(0);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			settoangle(90);	
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			settoangle(180);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			settoangle(270);			
		}
	}
	void settoangle(float angle)
	{
				switch((int)angle)
				{
					
					case 0: // problem case
						Debug.Log("trying to BROTATE" + angle.ToString());
						Quaternion rot = Quaternion.LookRotation((transform.position + (new Vector3(0,0,1))) - transform.position, Vector3.up);
						transform.rotation = rot;
						break;
					case 90:
							Debug.Log("trying to rotate" + angle.ToString());
							Quaternion rot90 = Quaternion.LookRotation((transform.position + (new Vector3(0,-1,0))) - transform.position, Vector3.up);
							transform.rotation = rot90;
						break;
					case 270:
						Debug.Log("trying to rotate" + angle.ToString());
						Quaternion rot270 = Quaternion.LookRotation((transform.position + (new Vector3(0,1,0))) - transform.position, Vector3.up);
						transform.rotation = rot270;
						break;
					case 180: // problem case this is a 180 case
						Debug.Log("trying to rotate" + angle.ToString());
						
							Quaternion rot180 = Quaternion.LookRotation((transform.position + (new Vector3(0,0,-1))) - transform.position, Vector3.down);
							transform.rotation = rot180;
						
						break;
					default:
						break;
				}
	}
}
