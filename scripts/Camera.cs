using Godot;
using System;

public partial class Camera : Camera2D
{
	private Node2D target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		getTarget();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position = target.Position;
	}

	private void getTarget()
	{
		var nodes = GetTree().GetNodesInGroup("Player");
		if (nodes.Count == 0)
		{
			GD.PrintErr("No player found in the scene.");
			return;
		}

		target = (Node2D)nodes[0];
	}
}
