using Godot;
using System;

public abstract partial class Upgrade : Node
{
	protected int playerClass;
	protected int tier;

	public abstract dynamic Effect();
}
