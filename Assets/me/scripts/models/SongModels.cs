using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongModel 
{
	public string name;
	public Nota[] notas;
}

[System.Serializable]
public class Nota
{
	public int duracion;
	public int tipo;
}
