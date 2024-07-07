using UnityEngine;

namespace Modules.Screens.Main.View
{
	public class MainScreenView : MonoBehaviour
	{
		[SerializeField]
		private Transform _contentContainer;
		
		[SerializeField]
		private Transform _popupsContainer;
		
		public Transform ContentContainer => _contentContainer;
		public Transform PopupsContainer => _popupsContainer;
	}
}