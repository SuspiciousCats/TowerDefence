using Godot;

namespace TowerDefence.Entities
{
	public class Enemy : Node2D
	{
		[Export] public float MovementSpeed = 100;

		public bool Dead = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			
		}

		public override void _Process(float delta)
		{
			base._Process(delta);
			//placeholder movement
			Position += new Vector2(MovementSpeed, 0) * delta;
		}

		public virtual void Die()
		{
			if (!Dead)
			{
				Dead = true;

				//placeholder
				QueueFree();
			}
			
		}
	}
}
