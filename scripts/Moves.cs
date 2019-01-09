using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {
	public struct Move
	{
		public Move(string _direction,float _column,float _angle)
		{
			direction = _direction;
			column = _column;
			angle = _angle;	
		}
		public string direction;
		public float column;
		public float angle;
	}
	public List<Move> moves = new List<Move>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AddToMovesList(string dir, float _column, float _angle)
	{
		Move newMove = new Move(dir,_column,_angle);
		moves.Add(newMove);
	}
	public void SaveMoves()
	{
		string infoString = FindObjectOfType<ConstructCube>().sizeOfCurrentCube.ToString();
		infoString += " ";
		foreach(Move move in moves)
		{
			infoString += move.direction;
			infoString += " ";
			infoString += move.column.ToString();
			infoString += " ";
			infoString += move.angle.ToString();
			infoString += " ";

		}
		PlayerPrefs.SetString("savedCube",infoString);
	}
}
