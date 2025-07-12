using Godot;
using System;

public partial class Slime : Enemy
{
    public override void Attack()
    {
        GD.Print("Attack");
    }

    public override void _Ready()
    {
        InitializeTheEnemy();
    }
}
