using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Calculator.View
{
	
	public class CalculatorView : MonoBehaviour
	{
		public event Action OnResultButtonClicked;
		public event Action<string> OnInputFieldValueChanged;
		
		[SerializeField]
		private TMP_InputField _inputField;
		
		[SerializeField]
		private Button _resultButton;
		
		[SerializeField]
		private Transform _historyContainer;
		public Transform HistoryContainer => _historyContainer;
		
		public void SetInputValue(string value) => _inputField.text = value;
		public void SetResultButtonActive(bool isActive) => _resultButton.interactable = isActive;

		private void OnEnable()
		{
			_resultButton.onClick.AddListener(OnResultButtonClick);
			_inputField.onValueChanged.AddListener(OnInputFieldValueChange);
		}
		
		private void OnDisable()
		{
			_resultButton.onClick.RemoveAllListeners();
			_inputField.onValueChanged.RemoveAllListeners();
		}

		private void OnResultButtonClick() => OnResultButtonClicked?.Invoke();
		private void OnInputFieldValueChange(string value) => OnInputFieldValueChanged?.Invoke(value);
	}
	
}