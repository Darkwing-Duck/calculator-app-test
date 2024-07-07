using UnityEngine;

namespace Modules.Common
{
	public class EmptyModel {}
	
	public abstract class Presenter<TView, TModel> 
		where TView : MonoBehaviour
		where TModel : new()
	{
		protected TView View;
		protected readonly TModel Model;

		private IViewProvider<TView> _viewProvider;

		public Presenter(IViewProvider<TView> viewProvider)
		{
			_viewProvider = viewProvider;
			Model = new TModel();
			Initialize();
		}
		
		private void Initialize()
		{
			InitializeModel(Model);
		}

		protected virtual void InitializeView(TView view)
		{ }
		
		protected virtual void InitializeModel(TModel model)
		{ }

		public void Show() => ShowUnder(null);
		
		public void ShowUnder(Transform parent)
		{
			View = _viewProvider.Get(parent);
			InitializeView(View);
			OnActivate();
		}
		
		public void Hide()
		{
			_viewProvider.Release();
			OnDeactivate();
		}
		
		protected virtual void OnActivate()
		{ }
		
		protected virtual void OnDeactivate()
		{ }
	}

	public abstract class StatelessPresenter<TView> : Presenter<TView, EmptyModel>
		where TView : MonoBehaviour
	{
		protected StatelessPresenter(IViewProvider<TView> viewProvider) : base(viewProvider)
		{ }
	}
}