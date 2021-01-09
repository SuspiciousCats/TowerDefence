using System;
using Godot;

namespace TowerDefence.Entities
{
	public class Turret : Node2D
	{
		[Export] public float FireRate = 1;

		[Export] public string BulletScenePath = "res://Entities/Bullets/Bullet.tscn";

		protected float _timeSinceLastShot = 0;
		
		private Area2D _senseArea;

		private AnimatedSprite _body;
	
		private Enemy _currentEnemy;

		public Enemy CurrentEnemy
		{
			get => _currentEnemy;
		}

		public override void _Ready()
		{
			base._Ready();
			_senseArea = GetNode<Area2D>("SenseArea");
			_body = GetNode<AnimatedSprite>("AnimatedSprite");
		}

		protected void _UpdateTarget()
		{
			var sensedAreas = _senseArea?.GetOverlappingAreas();
			if (sensedAreas != null)
			{
				if (!sensedAreas.Contains(_currentEnemy))
				{
					//clear current enemy
					_currentEnemy = null;
				}
			}
			if (sensedAreas != null && sensedAreas.Count > 0)
			{
				//we don't do checks because it's supposed to return Area2Ds
				foreach (Area2D sensedArea in sensedAreas)
				{
					if (sensedArea.GetParent() is Enemy)
					{
						_currentEnemy = sensedArea.GetParent<Enemy>();
						break;
					}
				}
			}
		}

		protected virtual void Shoot()
		{
			try
			{
				var scene = GD.Load<PackedScene>(BulletScenePath);
				if (scene != null)
				{
					var bullet = scene.Instance();
					if (bullet != null && bullet is Bullets.BulletBase)
					{
						GetParent().AddChild(bullet);
						(bullet as Bullets.BulletBase).Position = Position;
					}
				}
			}
			catch (InvalidCastException e)
			{
				GD.Print(e.Message);
				throw;
			}
			
		}
		
		public override void _Process(float delta)
		{
			base._Process(delta);
			_UpdateTarget();
			if (_currentEnemy != null)
			{
				_body.LookAt(_currentEnemy.Position);
				_body.Rotation += 90;

				_timeSinceLastShot += delta;
				if (_timeSinceLastShot >= FireRate)
				{
					Shoot();
					_timeSinceLastShot = 0;
				}
			}
		}
	}
}
