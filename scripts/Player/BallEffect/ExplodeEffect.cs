using Godot;
using System;

public class ExplodeEffect : BallEffect
{
    public ExplodeEffect(){}

    public override void ApplyOnHitEnemy()
    {
        GD.Print("Explosion");
    }

    public override void ApplyOnBounceAgainWall()
    {
        return;
    }

    public override void ApplyOnMove()
    {
        return;
    }
}
