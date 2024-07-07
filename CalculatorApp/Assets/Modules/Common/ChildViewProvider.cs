using UnityEngine;

namespace Modules.Common
{
	public class ChildViewProvider<TView> : IViewProvider<TView>
		where TView : MonoBehaviour
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