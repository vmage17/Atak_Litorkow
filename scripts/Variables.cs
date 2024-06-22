using System.Xml.Schema;
using Godot;

namespace AtakLitorków.scripts;

public partial class Variables : Node
{

	public string Difficulty = "easy";
	//private int NumberOfLetters;
	//private int NumberOfWords;
	//private int NumberOfSentences;
	public int NumberOfWaves = 3;
	public bool IsInfinite = false;
	public int LowerBoundSpeed = 80;
	public int UpperBoundSpeed = 110;
	
	public string Language = "pl";
	public string LettersEn = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public string LettersPl = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźżAĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
	public string LettersUa = "АаБбВвГгҐґДдЕеЄєИиІіЇїЖжЗзЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЮюЯя";
	
	public float WindowWidth;
	public int SpawnDistanceFromEdge = 100;
	
	public Resource HitLabelSettings = ResourceLoader.Load<Resource>("res://label_settings/HitLetterSettings.tres");
	//public Resource NoHitLabelSettings = ResourceLoader.Load<Resource>("res://label_settings/NoHitLetterSettings.tres");
	
	public Node CurrentScene { get; set; }

	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}

	public void SetDifficulty(string difficulty)
	{
		Difficulty = difficulty;
		switch (difficulty)
		{
			case "easy":
				//NumberOfLetters
				//NumberOfWords;
				//NumberOfSentences;
				LowerBoundSpeed = 70;
				UpperBoundSpeed = 100;
				break;
			case "normal":
				//NumberOfLetters
				//NumberOfWords;
				//NumberOfSentences;
				LowerBoundSpeed = 100;
				UpperBoundSpeed = 130;
				
				break;
			case "hard":
				//NumberOfLetters
				//NumberOfWords;
				//NumberOfSentences;
				LowerBoundSpeed = 130;
				UpperBoundSpeed = 160;
				break;
		}
		
	}
	
	public void SetLanguage(string language)
	{
		Language = language;
	}
	
	public void SetNumberOfWaves(int numberOfWaves)
	{
		NumberOfWaves = numberOfWaves;
		if (numberOfWaves == 0)
		{
			IsInfinite = true;
		}
	}
}
