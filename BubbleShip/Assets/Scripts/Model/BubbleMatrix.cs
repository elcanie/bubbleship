﻿using UnityEngine;
using System.Collections;

public class BubbleMatrix
{

	public const int ROW_SIZE = 1;
	public const int COL_SIZE = 1;

	Hashtable matrixBubble;

	public BubbleMatrix(){
		matrixBubble = new Hashtable();
	}


	//Move Bubble to correct position
	public Vector3 moveToCorrectPosition (Vector3 position, bool substract)
	{
		Vector3 rowCol = calcColAndRow (position);
		if (substract) {
			if (rowCol.y % 2 == 0){
				position.x -= COL_SIZE / 2f;
			}
		}
		rowCol = calcColAndRow (position);
		float x, y;
		//Debug.Log (rowCol.y % 2);
		if (rowCol.y % 2 == 0) {
			x = rowCol.x * COL_SIZE + COL_SIZE / 2f + COL_SIZE / 2f;
		} else {
			x = rowCol.x * COL_SIZE + COL_SIZE / 2f;
		}
		y = rowCol.y * ROW_SIZE + ROW_SIZE / 2f;

		return new Vector3 (x, y, position.z);
	}

	//Calculate col and row with x and y position
	public Vector3 calcColAndRow (Vector3 position)
	{
		int col = (int)position.x / COL_SIZE;
		int row = (int)position.y / ROW_SIZE;

		return new Vector3 (col, row, position.z);
	}

	//insert Bubble into matrix
	public void insert (GameObject bubbleObj)
	{
		//calcColAndRow and insert into matrix
		Vector3 rowCol = calcColAndRow (bubbleObj.transform.localPosition);
		bubbleObj.transform.localPosition = moveToCorrectPosition (bubbleObj.transform.localPosition, false);
		Bubble bubbleScript = bubbleObj.GetComponent<Bubble> ();
		bubbleScript.rowCol = rowCol;
		if (!matrixBubble.ContainsKey ("x:" + rowCol.x + ", y:" + rowCol.y)) {
			matrixBubble.Add ("x:" + rowCol.x + ", y:" + rowCol.y, bubbleObj);
		}
		//Debug.Log ("INSERT: x:"+rowCol.x+", y:"+rowCol.y);
	}

	public void remove(string key){
		matrixBubble.Remove (key);
	}

	public GameObject[] getNeighbours(Bubble bubbleScript, Vector3 localPosition){
		Vector3 rowCol = bubbleScript.rowCol;
		//Debug.Log ("--x:"+rowCol.x+", y:"+rowCol.y);
		GameObject[] neighbours = new GameObject[6];
		neighbours[0] = matrixBubble["x:"+(rowCol.x-1)+", y:"+(rowCol.y)] as GameObject;
		neighbours[1] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y)] as GameObject;
		neighbours[2] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y-1)] as GameObject;
		neighbours[3] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y+1)] as GameObject;
		neighbours[4] = matrixBubble["x:"+(rowCol.x-1)+", y:"+(rowCol.y+1)] as GameObject;
		neighbours[5] = matrixBubble["x:"+(rowCol.x-1)+", y:"+(rowCol.y-1)] as GameObject;
		//Debug.Log (rowCol+""+neighbours[0]+"0");
		//Debug.Log (rowCol+""+neighbours[1]+"1");
		//Debug.Log (rowCol+""+neighbours[2]+"2");
		//Debug.Log (rowCol+""+neighbours[3]+"3");
		//Debug.Log (rowCol+""+neighbours[4]+"4");
		//Debug.Log (rowCol+""+neighbours[5]+"5");
		return neighbours;
	}


}
