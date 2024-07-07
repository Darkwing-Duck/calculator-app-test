using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Calculator.View
{
	[ExecuteInEditMode]
	public class MaxHeightLayoutElement : UIBehaviour
	{
		[SerializeField]
		private RectTransform _targetRectTransform;
		
		[SerializeField]
		private LayoutElement _fakeLayoutElement;
		
		[SerializeField]
		private float _maxHeight;

		protected override void OnRectTransformDimensionsChange()
		{
			InvalidateSize();
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			InvalidateSize();
		}

		private void InvalidateSize()
		{
			if (_targetRectTransform is null) {
				return;
			}
			
			if (_fakeLayoutElement is null) {
				return;
			}
			
			var height = Mathf.Min(_targetRectTransform.rect.size.y,_maxHeight);
			_fakeLayoutElement.preferredHeight = height;
		}
	}
}