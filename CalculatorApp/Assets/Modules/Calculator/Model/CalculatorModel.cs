using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Modules.Core;

namespace Modules.Calculator.Model
{
	
	/// <summary>
	/// Persistent model of Calculator module.
	/// Here is the business logic of the module that can be easily tested separately by Unit Tests
	/// </summary>
	public class CalculatorModel : PersistentModel<CalculatorModel.Data>
	{
		public event Action<HistoryItemData> OnHistoryItemAdded;
		public event Action<string> OnInputValueChanged;

		public string InputValue => PersistentData.InputValue;
		
		private static readonly Regex InputValueValidator = new (@"^[0-9\+]*$");

		/// <summary>
		/// Sets new input value to the module state.
		/// </summary>
		public void SetInputValue(string value)
		{
			PersistentData.InputValue = value.Replace(" ", "");
		}

		/// <summary>
		/// Computes passed expression, changes the state and fires the notifications.
		/// </summary>
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
		
		/// <summary>
		/// Calculates the result of the expression.
		/// </summary>
		/// <param name="input">raw expression</param>
		/// <param name="result">result of the expression</param>
		/// <returns>true, if the expression was calculated successfuly</returns>
		private bool ProcessResult(string input, out int result)
		{
			// check if the expression has unsupported symbols and return
			if (!InputValueValidator.IsMatch(input)) {
				result = -1;
				return false;
			}
			
			// calculate the expression
			var valuesToAdd = input.Split("+");
			result = valuesToAdd
				.Select(int.Parse)
				.Sum();
			
			return true;
		}
		
		/// <summary>
		/// Returns read only history items list 
		/// </summary>
		public IReadOnlyList<HistoryItemData> GetHistory()
		{
			return PersistentData.HistoryItems;
		}
		

		#region Model Data Types

		/// <summary>
		/// A persistent data that is managing by the model
		/// </summary>
		[Serializable]
		public class Data
		{
			public string InputValue = string.Empty;
			public List<HistoryItemData> HistoryItems = new();
		}
		
		/// <summary>
		/// History item data that holds expression calculation result.
		/// </summary>
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