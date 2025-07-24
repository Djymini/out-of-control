using Godot;
using System;

public class FireEffect : BallEffectInterface
{
    public void OnHitEnemy()
    {
        GD.Print("Ennemie brulé !");
    }

    public void OnBounce()
    {
        GD.Print("Rebond avec feu");
    }

    public void OnMove()
    {
        GD.Print("Ca brule sur la route");
    }
}
