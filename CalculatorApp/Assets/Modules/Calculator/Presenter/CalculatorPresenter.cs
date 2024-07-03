using Modules.Calculator.Model;
using Modules.Calculator.View;

namespace Modules.Calculator.Presenter
{
	
	public class CalculatorPresenter : ICalculatorModelOut, ICalculatorViewOut
	{
		private readonly ICalculatorViewIn _view;
		private readonly ICalculatorModelIn _model;
		
		public CalculatorPresenter(ICalculatorViewIn view, ICalculatorModelIn model)
		{
			_view = view;
			_model = model;
		}
	}
	
}