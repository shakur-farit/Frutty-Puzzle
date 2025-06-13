using Code.Infrastructure.View.Registrars;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Tile.Behaviours
{
	public class TileSpriteRendererRegistrar : EntityComponentRegistrar
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;

		private IStaticDataService _staticDataService;

		[Inject]
		public void Constructor(IStaticDataService staticDataService) => 
			_staticDataService = staticDataService;

		public override void RegisterComponents()
		{
			_spriteRenderer.sprite = 
				_staticDataService.GetTileConfig(Entity.TileTypeId)
					.Sprite;

			Entity.AddTileSpriteRenderer(_spriteRenderer);
		}

		public override void UnregisterComponents()
		{
			throw new System.NotImplementedException();
		}
	}
}