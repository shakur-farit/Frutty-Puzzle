using Code.Infrastructure.View.Behaviours;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Tile.Behaviours
{
	public class TileScaler : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour _tileEntity;
		[SerializeField] private Transform _tileTransform;

		private void Start()
		{
			Debug.Log(_tileEntity.Entity.hasCellSizeX);

			//_tileTransform.localScale = 
			//	new Vector3(
			//		_tileEntity.Entity.CellSizeX, 
			//		_tileEntity.Entity.CellSizeY, 
			//		_tileEntity.Entity.CellSizeZ);
		}
	}
}
