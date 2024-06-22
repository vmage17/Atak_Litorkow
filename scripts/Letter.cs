using System.Runtime.CompilerServices;
using Godot;

namespace AtakLitorków.scripts;

public partial class Letter : CharacterBody2D
{
	
	private Variables _variables;
	
	private float _speed = 100.0f;
	private bool _isHit = false;
	private bool _isPartOfAWord = false;
	private Label _label;	
	private Timer _timer;

	public override void _Ready()
	{
		_variables = (Variables)GetNode("/root/VariablesScene");
		_timer = (Timer)GetChild(1);
		_label = (Label)GetChild(0); // Getting label
		//_label.LabelSettings = (LabelSettings)GetNode<GameManager>("/root").NoHitLabelSettings;
		
		switch (_variables.Language)
		{
			case "en":
				_label.Text = _variables.LettersEn[GD.RandRange(0, _variables.LettersEn.Length - 1)].ToString();
				break;
			case "pl":
				_label.Text = _variables.LettersPl[GD.RandRange(0, _variables.LettersPl.Length - 1)].ToString();
				break;
			case "ua":
				_label.Text = _variables.LettersUa[GD.RandRange(0, _variables.LettersUa.Length - 1)].ToString();
				break;
		}
		AddToGroup("letters");
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Vector2.Down * _speed * (float)delta;
	}

	public void Hit()
	{
		if (!_isHit)
		{
			_isHit = true;
			_label.LabelSettings = (LabelSettings)_variables.HitLabelSettings;
			
			if (!_isPartOfAWord)
			{
				SetPhysicsProcess(false);
				_timer.Start();
			}
		}
	}
	
	public bool IsHit()
	{
		return _isHit;
	}
	
	private void Die()
	{
		QueueFree();
	}

	public string GetText()
	{
		return _label.Text;
	}

	public void SetSpeed(int speed)
	{
		_speed = speed;
	}
}



