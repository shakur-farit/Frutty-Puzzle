using System;
using Code.Infrastructure.View.Registrars;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Tile.Behaviours
{
	public class TileSelectIconRegistrar : EntityComponentRegistrar
	{
		[SerializeField] private GameObject _selectIcon;

		public override void RegisterComponents() => 
			Entity.AddTileSelectIcon(_selectIcon);

		public override void UnregisterComponents()
		{
			if (Entity.hasTileSelectIcon)
				Entity.RemoveTileSelectIcon();
		}
	}
}