using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Modules.Common;

namespace Modules.Calculator.Model
{
	
	public class CalculatorModel : PersistentModel<CalculatorModel.Data>
	{
		public event Action<HistoryItemData> OnHistoryItemAdded;
		public event Action<string> OnInputValueChanged;

		public string InputValue => PersistentData.InputValue;
		
		private static readonly Regex InputValueValidator = new (@"^[0-9\+]*$");

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
			if (!InputValueValidator.IsMatch(input)) {
				result = -1;
				return false;
			}
			
			var valuesToAdd = input.Split("+");
			result = valuesToAdd
				.Select(int.Parse)
				.Sum();
			
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