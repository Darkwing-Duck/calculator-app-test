using Modules.AlertPopup.Model;
using Modules.AlertPopup.View;
using Modules.Common;

namespace Modules.AlertPopup.Presenter
{
	/// <summary>
	/// Alert Popup module's presenter
	/// </summary>
	public class AlertPopupPresenter : Presenter<AlertPopupView, AlertPopupModel>
	{
		public AlertPopupPresenter() : base(new ResourcesViewProvider<AlertPopupView>())
		{ }

		protected override void InitializeView(AlertPopupView view)
		{
			view.name = "AlertPopup";
			view.SetMessage(Model.AlertMessage);
		}

		public void SetAlertMessage(string value)
		{
			Model.SetAlertMessage(value);
		}

		protected override void OnActivate()
		{
			View.OkButton.onClick.AddListener(OnOkButtonClick);
			Model.OnAlertMessageChanged += OnAlertMessageChanged;
		}

		private void OnAlertMessageChanged(string message)
		{
			View.SetMessage(message);
		}

		protected override void OnDeactivate()
		{
			View.OkButton.onClick.RemoveListener(OnOkButtonClick);
			Model.OnAlertMessageChanged -= OnAlertMessageChanged;
		}

		private void OnOkButtonClick()
		{
			Hide();
		}
	}
}