using Godot;
using System;

public abstract partial class Upgrade : Node
{
	int playerClass;
	int tier;

	public abstract int Effect();
}
