using UnityEngine;

namespace Code.Meta.UI.Windows.Config
{
	[CreateAssetMenu(menuName = "Mohjong/Window Config", fileName = "WindowConfig")]
	public class WindowConfig : ScriptableObject
	{
		public WindowId Id;
		public GameObject ViewPrefab;
	}
}