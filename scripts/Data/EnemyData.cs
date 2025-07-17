using Godot;
using System;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export] public string name = "Enemy";
    [Export] public int life = 10;
    [Export] public int damage = 1;
}
