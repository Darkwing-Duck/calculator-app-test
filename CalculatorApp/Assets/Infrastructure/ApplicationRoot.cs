using Modules.Screens.Main.Presenter;
using UnityEngine;

namespace Infrastructure
{
	/// <summary>
	/// Application composition root.
	/// Here we just create the Main screen of our app.
	/// </summary>
	public class ApplicationRoot : MonoBehaviour
	{
		private void Awake()
		{
			var presenter = new MainScreenPresenter();
			presenter.ShowUnder(transform);
		}
	}
}