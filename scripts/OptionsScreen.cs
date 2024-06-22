using Godot;

namespace AtakLitorków.scripts;

public partial class OptionsScreen : Control
{

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
}
