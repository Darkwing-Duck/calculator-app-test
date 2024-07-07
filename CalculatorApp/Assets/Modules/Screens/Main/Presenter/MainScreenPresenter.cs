using Modules.AlertPopup.Presenter;
using Modules.Calculator;
using Modules.Calculator.Presenter;
using Modules.Common;
using Modules.Screens.Main.View;

namespace Modules.Screens.Main.Presenter
{
	public partial class MainScreenPresenter : StatelessPresenter<MainScreenView>
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
			var presenter = new CalculatorPresenter(this);
			presenter.ShowUnder(View.ContentContainer);
		}
	}
	
	public partial class MainScreenPresenter : IAlertPopupService
	{
		public void ShowAlert(string message)
		{
			_alertPopupPresenter.SetAlertMessage(message);
			_alertPopupPresenter.ShowUnder(View.PopupsContainer);
		}

		public void HideAlert()
		{
			_alertPopupPresenter.Hide();
		}
	}
}