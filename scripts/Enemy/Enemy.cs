using Godot;
using System;

public abstract partial class Enemy : StaticBody2D
{
    [Export] private EnemyData data;

    private int currentLife;

    public void InitializeTheEnemy()
    {
        this.currentLife = data.life;
        GD.Print("Life : " + this.currentLife + "/" + data.life);
    }

    public void Hit(int damage)
    {
        this.currentLife -= damage;
        GD.Print("Life : " + this.currentLife + "/" + data.life);
        if (this.currentLife < 0)
        {
            GetNode<CollisionShape2D>("CollisionShape2D").CallDeferred("set_disabled", true);
            QueueFree();
        }
    }

    public abstract void Attack();
}
