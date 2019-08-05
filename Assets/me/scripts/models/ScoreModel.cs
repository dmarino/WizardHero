using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreModel 
{
	public List<ScorePlayer> scores;
}

[System.Serializable]
public class ScorePlayer
{
	public string name;
	public string cancion;
	public int score;
}