using System;
using Godot;

namespace AtakLitorków.scripts;

public partial class GameManager : Node2D
{

	private Variables _variables;
	
	private int _currentWave = 1;
	private int _health = 3;
	private int _hits;
	
	private bool _shiftPressed = false;
	private bool _altPressed = false;
	
	private Godot.Collections.Array<PackedScene> _enemyTypeList = new()
	{
		ResourceLoader.Load<PackedScene>("res://scenes/Letter.tscn"),
		//ResourceLoader.Load<PackedScene>("res://scenes/Word.tscn"),
		//ResourceLoader.Load<PackedScene>("res://scenes/Sentence.tscn")
	};

	private Godot.Collections.Array<Node2D> _letters;

	//public Resource NoHitLabelSettings = ResourceLoader.Load<Resource>("res://label_settings/NoHitLetterSettings.tres");
	//public Resource HitLabelSettings = ResourceLoader.Load<Resource>("res://label_settings/HitLetterSettings.tres");
	
	public override void _Ready()
	{
		// Set difficulty level
		// Set number of letters, words and sentences and set speed
		_variables = (Variables)GetNode("/root/VariablesScene");
		_variables.WindowWidth = GetViewport().GetVisibleRect().Size.X;
		
		//_label.LabelSettings = (LabelSettings)GetNode<GameManager>("/root").NoHitLabelSettings;
		
		GetNode<Label>("%Czas").Text = "Czas: 30";
		GetNode<Label>("%Trafienia").Text = "Trafienia: 0";
		GetNode<Label>("%Życie").Text = "Życie: " + _health;
		GetNode<Label>("%Fala").Text = "Fala: " + _currentWave + "/" + _variables.NumberOfWaves;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<Label>("%Fala").Text = "Fala: " + _currentWave + "/" + _variables.NumberOfWaves;
		if (!GetNode<Timer>("%EnemySpawnTimer").Paused)
		{
			GetNode<Label>("%Czas").Text = "Czas: " + ((int)GetNode<Timer>("%WaveTimer").TimeLeft + 1);
		}
		else
		{
			GetNode<Label>("%Czas").Text = "Czas: " + ((int)GetNode<Timer>("%BreakTimer").TimeLeft + 1);
		}
		
		if (_health <= 0)
		{
			GameOver();
		}
		if (_currentWave > _variables.NumberOfWaves)
		{
			Victory();
		}
	}

	private void GameOver()
	{
		// Game Over Screen
		GD.Print("Game Over!");
		GetTree().ChangeSceneToFile("res://scenes/MainMenuScreen.tscn");
	}

	private void Victory()
	{
		// Victory Screen
		GD.Print("You Won!");
		GetTree().ChangeSceneToFile("res://scenes/MainMenuScreen.tscn");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventKey keyEvent) return;

		if (keyEvent.Keycode == Key.Escape && keyEvent.Pressed)
		{
			GameOver();
		}
		
		if (keyEvent.Keycode == Key.Shift && keyEvent.Pressed)
		{
			if (!_shiftPressed)
			{
				GD.Print("Shift pressed!");
			}
			_shiftPressed = true;
			
		}
		else if (keyEvent.Keycode == Key.Shift && keyEvent.IsReleased())
		{
			_shiftPressed = false;
			GD.Print("Shift released!");
		}
		if (keyEvent.Keycode == Key.Alt && keyEvent.Pressed)
		{
			if (!_altPressed)
			{
				GD.Print("Alt pressed!");
			}
			_altPressed = true;
			
		}
		else if (keyEvent.Keycode == Key.Alt && keyEvent.IsReleased())
		{
			_altPressed = false;
			GD.Print("Alt released!");
		}
		else if (keyEvent.Keycode.ToString().Length == 1 && keyEvent.Pressed)
		{
			//var inputLetter = keyEvent.Keycode;
			var inputLetter = "";
			switch (_variables.Language)
			{
				case "en":
					inputLetter = _shiftPressed ? keyEvent.Keycode.ToString() : keyEvent.Keycode.ToString().ToLower();
					break;
				case "pl":
					if (_altPressed)
					{
						inputLetter = _shiftPressed ? PolishLetter(keyEvent.Keycode) : PolishLetter(keyEvent.Keycode).ToLower();
					}
					else
					{
						inputLetter = _shiftPressed ? keyEvent.Keycode.ToString() : keyEvent.Keycode.ToString().ToLower();
					}
					break;
				case "ua":
					if (_altPressed)
					{
						//inputLetter = _shiftPressed ? UkrainianLetter(keyEvent.Keycode) : UkrainianLetter(keyEvent.Keycode).ToLower();
					}
					else
					{
						inputLetter = _shiftPressed ? keyEvent.Keycode.ToString() : keyEvent.Keycode.ToString().ToLower();
					}
					break;
			}
			
			GD.Print(inputLetter);
			HitEnemy(inputLetter);
		}
		//GD.Print("End of input of key: " + keyEvent.Keycode);
	}

	private string PolishLetter(Key key)
	{
		return key switch
		{
			Key.A => "Ą",
			Key.C => "Ć",
			Key.E => "Ę",
			Key.L => "Ł",
			Key.N => "Ń",
			Key.O => "Ó",
			Key.S => "Ś",
			Key.X => "Ź",
			Key.Z => "Ż",
			_ => ""
		};
	}
	
	/*private string UkrainianLetter(Key key, bool isAltPressed)
	{
		if (isAltPressed && key == Key.U)
		{
			return "Ґ";
		}
		
		return key switch
		{
			Key.U => "Ґ",
			Key.U => "А",
			Key.U => "Б",
			Key.U => "В",
			Key.U => "Г",
			Key.U => "Д",
			Key.U => "Е",
			Key.U => "Є",
			Key.U => "И",
			Key.U => "І",
			Key.U => "Ї",
			Key.U => "Ж",
			Key.U => "З",
			Key.U => "Й",
			Key.U => "К",
			Key.U => "Л",
			Key.U => "М",
			Key.U => "Н",
			Key.U => "О",
			Key.U => "П",
			Key.U => "Р",
			Key.U => "С",
			Key.U => "Т",
			Key.U => "У",
			Key.U => "Ф",
			Key.U => "Х",
			Key.U => "Ц",
			Key.U => "Ч",
			Key.U => "Ш",
			Key.U => "Щ",
			Key.U => "Ь",
			Key. => "Ю",
			Key.Z => "Я",
			_ => ""
		};
	}*/

	// Hits newest letter
	private void HitEnemy(string inputLetter)
	{
		// Iterate in group 'letters'
		foreach (var letter in GetTree().GetNodesInGroup("letters"))
		{
			// Search for specific letter
			var currentLetter = (Letter)letter;
			if (currentLetter.GetText() != inputLetter || currentLetter.IsHit()) continue;
			currentLetter.Hit();
			_hits++;
			GetNode<Label>("%Trafienia").Text = "Trafienia: " + _hits;
			break;
		}
	}

	private void SpawnEnemy()
	{
		//var enemyType = GD.RandRange(0, enemyTypeList.Count);
		// Change 0 to enemyType
		var enemy = (Node2D)_enemyTypeList[0].Instantiate();
		
		// Setting spawn point
		var enemyPosition = enemy.Position;
		enemyPosition.Y = GetNode<Node2D>("SpawnLocation").Position.Y;
		enemyPosition.X = GD.RandRange(_variables.SpawnDistanceFromEdge, (int)_variables.WindowWidth - _variables.SpawnDistanceFromEdge);
		enemy.Position = enemyPosition;
		
		// Maybe add all enemies to one big array? or count them
		AddChild(enemy);
		if (enemy is Letter)
		{
			((Letter)enemy).SetSpeed(GD.RandRange(_variables.LowerBoundSpeed, _variables.UpperBoundSpeed) + 3 * (_currentWave-1));
			GD.Print("Letter is created!");
			
		}
	}
	
	private void OnEnemyEntered(Node enemy)
	{
		// Check what kind of enemy and subtract number of hp depends on enemy type
		if (enemy is Letter)
		{
			CastleHit(1);
			GD.Print("Letter is deleted!");
		}
		/*
		else if (enemy is Word)
		{
			
		}
		else if (enemy is Sentence)
		{
			
		}
		*/
		enemy.QueueFree();
	}

	private void CastleHit(int damage)
	{
		_health -= damage;
		GetNode<Label>("%Życie").Text = "Życie: " + _health;
	}
	
	private void OnEndOfWave()
	{
		GetNode<Timer>("%EnemySpawnTimer").Paused = true;
		GetNode<Timer>("%WaveTimer").Stop();
		GetNode<Timer>("%BreakTimer").Start();
		if (_currentWave >= _variables.NumberOfWaves)
		{
			Victory();
		}
	}
	
	private void OnEndOfBreak()
	{
		_currentWave++;
		GetNode<Timer>("%EnemySpawnTimer").Paused = false;
		GetNode<Timer>("%BreakTimer").Stop();
		GetNode<Timer>("%WaveTimer").Start();
	}
}



