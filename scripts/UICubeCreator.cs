using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICubeCreator : MonoBehaviour {

	[SerializeField] InputField cubeSize;
	[SerializeField] Slider slider;
	[SerializeField] Text showCubeSize;
	ConstructCube cubeConstructingScript;

	// Use this for initialization
	void Start () {
		cubeConstructingScript = FindObjectOfType<ConstructCube>();
		
		slider.onValueChanged.AddListener(delegate {ChangeCubeSize();});
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			DestroyAllCubes();
			FindObjectOfType<RotateMe>().PerfectDist(slider.value);
			FindObjectOfType<RotateMe>().upSpeed = 7 * slider.value;
			FindObjectOfType<ConstructCube>().StopAllCoroutines();
			cubeConstructingScript.ConstructCubeSlowly((int)slider.value);
			// FindObjectOfType<RotateMe>().PerfectDist(slider.value);
		}
		if(Input.GetKeyDown(KeyCode.Space))
			{
				DestroyAllCubes();
				FindObjectOfType<RotateMe>().PerfectDist(slider.value);
				FindObjectOfType<RotateMe>().upSpeed = 7 * slider.value;
				FindObjectOfType<ConstructCube>().StopAllCoroutines();
				cubeConstructingScript.ConstructCubeQuickly((int)slider.value);
				
			}
		
	}

	public void ChangeCubeSize()
	{
		showCubeSize.text = slider.value.ToString();
	}

	void DestroyAllCubes()
	{
		CubePiece[] allLitteCubes = FindObjectsOfType<CubePiece>();
		foreach(CubePiece x in allLitteCubes)
		{
			Destroy(x.gameObject);
		}
	}
}
