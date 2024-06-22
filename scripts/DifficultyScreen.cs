using Godot;

namespace AtakLitorków.scripts;

public partial class DifficultyScreen : Control
{
	
	private Variables _variables;
	private bool _isDifficultySet = false;
	private bool _isNumberOfWavesSet = false;
	private string _toggledDifficulty = "";
	private string _toggledWaveNumber = "";
	//private Label _labelEasy;
	//private Label _labelNormal;
	//private Label _labelHard;
	//private Label _label3;
	//private Label _label6;
	//private Label _label9;
	//private Label _label12;
	//private Label _labelInf;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_variables = (Variables)GetNode("/root/VariablesScene");
		//_labelEasy = GetNode<Label>("%Łatwy");
		//_labelNormal = GetNode<Label>("%Normalny");
		//_labelHard = GetNode<Label>("%Trudny");
		//_label3 = GetNode<Label>("%3");
		//_label6 = GetNode<Label>("%6");
		//_label9 = GetNode<Label>("%9");
		//_label12 = GetNode<Label>("%12");
		//_labelInf = GetNode<Label>("%Niesk");
	}
	
	private void Start()
	{
		if (_isDifficultySet && _isNumberOfWavesSet)
		{
			GetTree().ChangeSceneToFile("res://scenes/Level.tscn");
		}
	}
	
	private void Process()
	{
		switch (_toggledDifficulty)
		{
			case "easy":
				//_labelEasy.LabelSettings = (LabelSettings)_variables.HitLabelSettings;
				break;
			default:
				break;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventKey keyEvent) return;

		if (keyEvent.Keycode == Key.Escape && keyEvent.Pressed)
		{
			GetTree().ChangeSceneToFile("res://scenes/MainMenuScreen.tscn");
		}
	}

	private void Return()
	{
		GetTree().ChangeSceneToFile("res://scenes/MainMenuScreen.tscn");
	}

	private void Set3Waves()
	{
		_variables.SetNumberOfWaves(3);
		_isNumberOfWavesSet = true;
	}

	private void Set6Waves()
	{
		_variables.SetNumberOfWaves(6);
		_isNumberOfWavesSet = true;
	}

	private void Set9Waves()
	{
		_variables.SetNumberOfWaves(9);
		_isNumberOfWavesSet = true;
	}
	
	private void Set12Waves()
	{
		_variables.SetNumberOfWaves(12);
		_isNumberOfWavesSet = true;
	}
	
	private void SetInfinityWaves()
	{
		_variables.SetNumberOfWaves(10000);
		_isNumberOfWavesSet = true;
	}

	private void SetEasyDifficulty()
	{
		_variables.SetDifficulty("easy");
		_isDifficultySet = true;
	}
	
	private void SetNormalDifficulty()
	{
		_variables.SetDifficulty("normal");
		_isDifficultySet = true;
	}
	
	private void SetHardDifficulty()
	{
		_variables.SetDifficulty("hard");
		_isDifficultySet = true;
	}
	
}
