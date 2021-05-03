using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ScoreData : IComparable<ScoreData>
{
    public string name;
    public int score;

	public int CompareTo(ScoreData other)
	{
		//Guardian clause
		if(other == null)
		{
			return 1;
		}
		return this.score - other.score;

		//does the same as the above line
		//if (this.score > other.score)
		//{
		//	return 1;
		//}
		//if (this.score < other.score)
		//{
		//	return -1;
		//}
		//return 0;
	}
}
