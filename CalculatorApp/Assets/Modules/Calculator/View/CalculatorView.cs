using System;
using Modules.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Calculator.View
{
	
	/// <summary>
	/// Calculator module view.
	/// Just a "passive" view that just forwards the event to the presenter. 
	/// </summary>
	public class CalculatorView : ModuleView
	{
		public event Action OnResultButtonClicked;
		public event Action<string> OnInputFieldValueChanged;
		
		[SerializeField]
		private TMP_InputField _inputField;
		
		[SerializeField]
		private Button _resultButton;
		
		[SerializeField]
		private InputFieldFocusLineView _inputFieldFocusLine;
		
		[SerializeField]
		private Transform _historyContainer;
		public Transform HistoryContainer => _historyContainer;
		
		public void SetInputValue(string value) => _inputField.text = value;
		public void SetResultButtonActive(bool isActive) => _resultButton.interactable = isActive;

		private void OnEnable()
		{
			_resultButton.onClick.AddListener(OnResultButtonClick);
			_inputField.onValueChanged.AddListener(OnInputFieldValueChange);
			_inputField.onSelect.AddListener(OnInputFieldSelect);
			_inputField.onDeselect.AddListener(OnInputFieldDeSelect);
		}
		
		private void OnDisable()
		{
			_resultButton.onClick.RemoveAllListeners();
			_inputField.onValueChanged.RemoveAllListeners();
			_inputField.onSelect.RemoveAllListeners();
			_inputField.onDeselect.RemoveAllListeners();
		}

		private void OnResultButtonClick() => OnResultButtonClicked?.Invoke();
		private void OnInputFieldValueChange(string value) => OnInputFieldValueChanged?.Invoke(value);
		private void OnInputFieldSelect(string value) => _inputFieldFocusLine.SetSelectionActive(true);
		private void OnInputFieldDeSelect(string value) => _inputFieldFocusLine.SetSelectionActive(false);
	}
	
}