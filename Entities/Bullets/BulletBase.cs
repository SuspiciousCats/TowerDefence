using System;
using Godot;

namespace TowerDefence.Entities.Bullets
{
	public class BulletBase : Area2D
	{
		[Export] public float Damage = 100;

		[Export] public Node2D BulletOwner;

		[Export] public float Speed = 10;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		
		}

		public override void _Process(float delta)
		{
			base._Process(delta);
			Position += new Vector2(Speed, Speed) * delta;
		}

		private void _on_Area2D_area_entered(object area)
		{
			if (area is Area2D)
			{
				var parent = (area as Area2D).GetParent();
				if (parent is Enemy)
				{
					(parent as Enemy).Die();
				}
			}
		}
	}
}



