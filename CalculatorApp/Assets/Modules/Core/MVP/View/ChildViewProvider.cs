using UnityEngine;

namespace Modules.Common
{
	public class ChildViewProvider<TView> : IModuleViewProvider<TView>
		where TView : ModuleView
	{
		private TView _view;

		public ChildViewProvider(TView view)
		{
			_view = view;
		}

		public TView Get(Transform parent = null) => _view;

		public void Release() { }
	}
}