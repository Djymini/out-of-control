using Godot;
using System;

public partial class BasicSuperShoot : SuperShoot
{
    public override void InitializeEffect()
    {
        return;
    }

    public override void Move(double delta, double speed, Vector2 direction)
    {
        speed += delta * 2;
        Position += (float)speed * (float)delta * direction;

        if (Position.Y < 0)
        {
            QueueFree();
        }
    }

}
