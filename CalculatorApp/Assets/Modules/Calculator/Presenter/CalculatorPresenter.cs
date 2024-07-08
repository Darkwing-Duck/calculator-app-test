using Modules.Calculator.Model;
using Modules.Calculator.View;
using Modules.Common;
using Modules.Common.Storage;

namespace Modules.Calculator.Presenter
{
	
	/// <summary>
	/// Calculator module's presenter.
	/// Controls the visual layer of the module depends on the model response.
	/// Also has a module storage here where the persistent data is saving 
	/// </summary>
	public class CalculatorPresenter : Presenter<CalculatorView, CalculatorModel>
	{
		private const string Name = "Calculator";
		private const int MinCharacters = 3;
		
		// factory to create history items visual elements
		private readonly IModuleViewProvider<CalculatorHistoryItemView> _historyItemViewProvider;
		
		// alert popup service
		private IAlertPopupService _alertPopupService;
		
		// module storage to save the persistent state
		private IModuleStorage _storage = new PlayerPrefsModuleStorage(Name);

		public CalculatorPresenter(IAlertPopupService alertPopupService) : base(new ResourcesViewProvider<CalculatorView>())
		{
			_alertPopupService = alertPopupService;
			_historyItemViewProvider = new ResourcesViewProvider<CalculatorHistoryItemView>();
		}

		protected override void InitializeView(CalculatorView view)
		{
			view.name = Name;
			view.SetInputValue(Model.InputValue);

			InitializeHistoryView();
			InvalidateResultButton();
		}

		private void InitializeHistoryView()
		{
			var historyItems = Model.GetHistory();
			
			foreach (var historyItem in historyItems) {
				AddHistoryItemView(historyItem);
			}
		}

		protected override void InitializeModel(CalculatorModel model)
		{
			Model.LoadFrom(_storage);
		}

		private void Save()
		{
			Model.SaveTo(_storage);
		}

		protected override void OnActivate()
		{
			View.OnResultButtonClicked += OnResultButtonClick;
			View.OnInputFieldValueChanged += OnViewInputValueChanged;
			
			Model.OnInputValueChanged += OnModelInputValueChanged;
			Model.OnHistoryItemAdded += OnHistoryItemAdded;
		}
		
		protected override void OnDeactivate()
		{
			View.OnResultButtonClicked -= OnResultButtonClick;
			View.OnInputFieldValueChanged -= OnViewInputValueChanged;
			
			Model.OnInputValueChanged -= OnModelInputValueChanged;
			Model.OnHistoryItemAdded -= OnHistoryItemAdded;
			
			// saving state only on presenter deactivation.
			// but it's possible to save it on any change model change
			Save();
		}

		/// <summary>
		/// Adds visual history item to the history list view
		/// </summary>
		/// <param name="itemData">History item data from the model state</param>
		private void AddHistoryItemView(CalculatorModel.HistoryItemData itemData)
		{
			var itemView = _historyItemViewProvider.Get(View.HistoryContainer);
			var result = itemData.IsSuccess
				? itemData.Result.ToString()
				: "ERROR";
			
			// make sure that the new items are appearing at the beginning of the list
			itemView.transform.SetAsFirstSibling();
			
			// fill the history item text
			itemView.SetValue($"{itemData.InputValue}={result}");
		}

		/// <summary>
		/// Fires by the model, when the new item was added to the state.
		/// </summary>
		private void OnHistoryItemAdded(CalculatorModel.HistoryItemData itemData)
		{
			AddHistoryItemView(itemData);
			
			// if there is error in the equation process then we are showing
			// the error message through Alert Popup service
			if (!itemData.IsSuccess) {
				_alertPopupService.ShowAlert("Please check the expression you just entered");
			}
		}
		
		/// <summary>
		/// Fires when the input value in the model was changed.
		/// </summary>
		private void OnModelInputValueChanged(string value)
		{
			View.SetInputValue(value);
		}
		
		/// <summary>
		/// Fires when the input value in the view was changed.
		/// </summary>
		private void OnViewInputValueChanged(string value)
		{
			Model.SetInputValue(value);
			InvalidateResultButton();
		}

		/// <summary>
		/// Updates the state of result button
		/// </summary>
		private void InvalidateResultButton()
		{
			View.SetResultButtonActive(Model.InputValue.Length >= MinCharacters);
		}

		/// <summary>
		/// Fires when the result button was clicked
		/// </summary>
		private void OnResultButtonClick()
		{
			Model.Compute();
		}
	}
	
}