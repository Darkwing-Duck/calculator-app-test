using Modules.Screens.Main.Presenter;
using UnityEngine;

namespace Infrastructure
{
	public class ApplicationRoot : MonoBehaviour
	{
		private void Awake()
		{
			var presenter = new MainScreenPresenter();
			presenter.ShowUnder(transform);
		}
	}
}