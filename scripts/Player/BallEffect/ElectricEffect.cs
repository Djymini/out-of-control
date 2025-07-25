using Godot;
using System;

public class ElectricEffect : BallEffect
{
    public ElectricEffect(){}

    public override void ApplyOnHitEnemy()
    {
        GD.Print("Ennemie electrocuté !");
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
