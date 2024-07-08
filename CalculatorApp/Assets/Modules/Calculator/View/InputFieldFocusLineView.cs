using UnityEngine;
using UnityEngine.UI;

namespace Modules.Calculator.View
{
	/// <summary>
	/// Part of the calculator view that controls the state of focus line under the input field.
	/// </summary>
	public class InputFieldFocusLineView : MonoBehaviour
	{
		[SerializeField]
		private Color _selectedColor;
		
		[SerializeField]
		private Color _unselectedColor;
		
		[SerializeField]
		private Image _focusImage;

		public void SetSelectionActive(bool isActive)
		{
			_focusImage.color = isActive 
				? _selectedColor 
				: _unselectedColor;
		}
	}
}