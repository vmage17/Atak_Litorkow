using Godot;

namespace AtakLitorków.scripts;

public partial class MainMenuScreen : Control
{
	private void Play()
	{
		GetTree().ChangeSceneToFile("res://scenes/DifficultyScreen.tscn");
	}
	
	private void Options()
	{
		GetTree().ChangeSceneToFile("res://scenes/OptionsScreen.tscn");
	}
	
	private void Quit()
	{
		GetTree().Quit();
	}
}
