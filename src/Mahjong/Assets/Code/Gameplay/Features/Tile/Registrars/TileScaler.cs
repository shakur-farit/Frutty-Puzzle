using Code.Infrastructure.View.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.Tile.Behaviours
{
	public class TileScaler : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour _tileEntity;
		[SerializeField] private Transform _tileTransform;

		private void Start()
		{
			_tileTransform.localScale =
				new Vector3(
					_tileEntity.Entity.TileSizeX,
					_tileEntity.Entity.TileSizeY,
					_tileEntity.Entity.TileSizeZ);
		}
	}
}
