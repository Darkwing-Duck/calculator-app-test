using Modules.AlertPopup.Presenter;
using Modules.Calculator;
using Modules.Calculator.Presenter;
using Modules.Common;
using Modules.Screens.Main.View;

namespace Modules.Screens.Main.Presenter
{
	
	/// <summary>
	/// Main screen of the app.
	/// Here you can see how we can split different interface implementations in different partial classes to separate.
	/// MainScreenPresenter is also an Alert popup service to which calculator module delegates Alert popup openning.
	/// </summary>
	public class MainScreenPresenter : StatelessPresenter<MainScreenView>, IAlertPopupService
	{
		private readonly AlertPopupPresenter _alertPopupPresenter;

		public MainScreenPresenter() : base(new ResourcesViewProvider<MainScreenView>())
		{
			_alertPopupPresenter = new AlertPopupPresenter();
		}

		protected override void InitializeView(MainScreenView view)
		{
			view.name = "MainScreen";
		}

		protected override void OnActivate()
		{
			// creating calculator presenter and passing alert popup service to the constructor by interface
			var presenter = new CalculatorPresenter(this);
			presenter.ShowUnder(View.ContentContainer);
		}

		#region IAlertPopupService Implementation

		void IAlertPopupService.ShowAlert(string message)
		{
			_alertPopupPresenter.SetAlertMessage(message);
			_alertPopupPresenter.ShowUnder(View.PopupsContainer);
		}

		void IAlertPopupService.HideAlert()
		{
			_alertPopupPresenter.Hide();
		}

		#endregion
	}
	
}