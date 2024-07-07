using Modules.Calculator.Model;
using Modules.Calculator.View;
using Modules.Common;
using Modules.Common.Storage;

namespace Modules.Calculator.Presenter
{
	
	public class CalculatorPresenter : Presenter<CalculatorView, CalculatorModel>
	{
		private const string Name = "Calculator";
		private const int MinCharacters = 3;
		
		private readonly IViewProvider<CalculatorHistoryItemView> _historyItemViewProvider;
		private IAlertPopupService _alertPopupService;
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

			// initialize history view
			var historyItems = Model.GetHistory();
			
			foreach (var historyItem in historyItems) {
				AddHistoryItemView(historyItem);
			}
			
			InvalidateResultButton();
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
		}

		private void AddHistoryItemView(CalculatorModel.HistoryItemData itemData)
		{
			var itemView = _historyItemViewProvider.Get(View.HistoryContainer);
			var result = itemData.IsSuccess
				? itemData.Result.ToString()
				: "ERROR";
			
			itemView.SetValue($"{itemData.InputValue}={result}");
		}

		private void OnHistoryItemAdded(CalculatorModel.HistoryItemData itemData)
		{
			AddHistoryItemView(itemData);
			Save();
			
			if (!itemData.IsSuccess) {
				_alertPopupService.ShowAlert("Please check the expression you just entered");
			}
		}
		
		private void OnModelInputValueChanged(string value)
		{
			View.SetInputValue(value);
		}
		
		private void OnViewInputValueChanged(string value)
		{
			Model.SetInputValue(value);
			InvalidateResultButton();
			Save();
		}

		private void InvalidateResultButton()
		{
			View.SetResultButtonActive(Model.InputValue.Length >= MinCharacters);
		}

		private void OnResultButtonClick()
		{
			Model.Compute();
		}
	}
	
}