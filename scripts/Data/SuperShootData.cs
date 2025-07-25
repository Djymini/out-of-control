using Godot;
using System;

[GlobalClass]
public partial class SuperShootData : Resource
{
    [Export] public string name = "SuperProjectile";
    [Export] public string description = "";
    [Export] public int speed = 300;
    [Export] public int damage = 30;
}
