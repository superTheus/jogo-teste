using Godot;
using System;

public partial class LevelEnd : Area2D
{

	public enum Levels
	{
		County,
		Forest,
		Tropic,
	}

	[Export]
	public Levels NextLevel = Levels.County;
	public void _on_body_entered(Node2D body)
	{
		CallDeferred("loadNextLevel");
	}

	public void loadNextLevel()
	{
		GetTree().ChangeSceneToFile("res://scene/" + NextLevel + ".tscn");
	}
}
