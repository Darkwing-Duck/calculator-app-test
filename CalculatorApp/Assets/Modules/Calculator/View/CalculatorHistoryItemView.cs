using TMPro;
using UnityEngine;

namespace Modules.Calculator.View
{
	public class CalculatorHistoryItemView : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _textField;

		public void SetValue(string value)
		{
			_textField.text = value;
		}
	}
}