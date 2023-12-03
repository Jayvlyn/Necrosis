using Godot;
using System;

public partial class ClassSelect : Node2D
{
	public override void _Ready()
	{
	}

	public void _on_circle_button_pressed()
	{
        StartGame();
    }

    public void _on_square_button_pressed()
	{
        StartGame();
    }

	public void _on_triangle_button_pressed()
	{
        StartGame();
    }

	public void _on_start_button_pressed()
	{
		StartGame();
	}

	public void StartGame()
	{
        GetTree().ChangeSceneToFile("res://Prototype/PrototypeLevel.tscn");
    }
}
