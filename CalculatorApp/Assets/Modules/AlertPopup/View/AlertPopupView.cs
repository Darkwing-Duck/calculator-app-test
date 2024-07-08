using Modules.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.AlertPopup.View
{
	public class AlertPopupView : ModuleView
	{
		[SerializeField]
		private TMP_Text _messageField;

		[SerializeField] 
		private Button _okButton;
		public Button OkButton => _okButton;

		public void SetMessage(string value)
		{
			_messageField.text = value;
		}
	}
}