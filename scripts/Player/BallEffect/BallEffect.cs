using System;

public abstract class BallEffect
{
    private int damage { get; set; }

    public abstract void ApplyOnHitEnemy();
    public abstract void ApplyOnBounceAgainWall();
    public abstract void ApplyOnMove();
}