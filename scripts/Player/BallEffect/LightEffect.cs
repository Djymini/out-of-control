using Godot;
using System;

public class LightEffect : BallEffect
{
    public LightEffect(){}

    public override void ApplyOnHitEnemy()
    {
        GD.Print("Ennemie Touché par la lumière !");
    }

    public override void ApplyOnBounceAgainWall()
    {
        GD.Print("Lumière diffusée");
    }

    public override void ApplyOnMove()
    {
        return;
    }
}
