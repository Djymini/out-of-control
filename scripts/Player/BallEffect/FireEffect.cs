using Godot;
using System;

public class FireEffect : BallEffect
{
    public FireEffect(){}

    public override void ApplyOnHitEnemy()
    {
        GD.Print("Ennemie brulé !");
    }

    public override void ApplyOnBounceAgainWall()
    {
        return;
    }

    public override void ApplyOnMove()
    {
        GD.Print("Ca brule sur la route");
    }
}
