using Godot;
using System;
using System.Diagnostics;

public partial class ClassSelect : Node2D
{
	public override void _Ready()
	{
	}

	public void _on_circle_button_pressed()
	{
		Global.GetInstance().selectedClass = Global.Class.Soldier;
    }

    public void _on_square_button_pressed()
	{
        Global.GetInstance().selectedClass = Global.Class.Tanker;
    }

	public void _on_triangle_button_pressed()
	{
        Global.GetInstance().selectedClass = Global.Class.Sprinter;
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
