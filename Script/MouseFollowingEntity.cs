using Godot;

namespace TowerDefence.Script
{
	public class MouseFollowingEntity : TowerDefence.Entities.Enemy
	{
		public override void _Process(float delta)
		{
			Position = GetViewport().GetMousePosition();
		}
	}
}
