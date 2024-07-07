using System;
using System.Collections.Generic;
using Modules.Common;
using UnityEngine;

namespace Modules.Calculator.Model
{
	
	public class CalculatorModel : PersistentModel<CalculatorModel.Data>
	{
		public event Action<HistoryItemData> OnHistoryItemAdded;
		public event Action<string> OnInputValueChanged;

		public string InputValue => PersistentData.InputValue;

		public void SetInputValue(string value)
		{
			PersistentData.InputValue = value;
		}

		public void Compute()
		{
			var inputValueCache = InputValue;
			
			if (ProcessResult(InputValue, out var result)) {
				PersistentData.InputValue = string.Empty;
				OnInputValueChanged?.Invoke(InputValue);
			}
			
			var item = new HistoryItemData(inputValueCache, result);
			PersistentData.HistoryItems.Add(item);
			OnHistoryItemAdded?.Invoke(item);
		}
		
		private bool ProcessResult(string input, out int result)
		{
			if (!ExpressionEvaluator.Evaluate(input, out int evaluatedValue)) {
				result = -1;
				return false;
			}

			result = evaluatedValue;
			return true;
		}
		
		public IReadOnlyList<HistoryItemData> GetHistory()
		{
			return PersistentData.HistoryItems;
		}

		#region Model Data Types

		[Serializable]
		public class Data
		{
			public string InputValue = string.Empty;
			public List<HistoryItemData> HistoryItems = new();
		}
		
		[Serializable]
		public class HistoryItemData
		{
			public string InputValue { get; private set; }
			public int Result { get; private set; }
			public bool IsSuccess => Result >= 0;

			public HistoryItemData(string inputValue, int result)
			{
				InputValue = inputValue;
				Result = result;
			}
		}

		#endregion
	}
	
}