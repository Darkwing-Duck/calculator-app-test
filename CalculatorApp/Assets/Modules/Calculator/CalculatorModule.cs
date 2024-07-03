using Modules.Calculator.Model;
using Modules.Calculator.Presenter;
using Modules.Calculator.View;

namespace Modules.Calculator
{
	
	public static class CalculatorModule
	{
		public static CalculatorPresenter Assemble()
		{
			var model = new CalculatorModel();
			CalculatorView view = CreateView(); // TODO: Load view
			var presenter = new CalculatorPresenter(view, model);
			
			model.Initialize(presenter);
			view.Initialize(presenter);
			
			return presenter;
		}
	}
	
}