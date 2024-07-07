using System;

namespace Modules.AlertPopup.Model
{
	public class AlertPopupModel
	{
		public event Action<string> OnAlertMessageChanged; 
		
		public string AlertMessage { get; private set; }

		public void SetAlertMessage(string value)
		{
			AlertMessage = value;
			OnAlertMessageChanged?.Invoke(AlertMessage);
		}
	}
}