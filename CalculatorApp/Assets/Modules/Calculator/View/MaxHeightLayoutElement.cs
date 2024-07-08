using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.Calculator.View
{
	
	/// <summary>
	/// A utility script that resolves the max height issue of the history list using autolayouting.
	/// For this trick we need to make a real ScrollRect's content object to be ignored by the autolayouting
	/// and create a fake autolayouting element which will take a part in aoutolayout calculation.
	/// So, this script takes the real height of the history content rectTransform and if the height is greater than
	/// "_maxHeight" than sets to the _fakeLayoutElement height "_maxHeight"
	/// </summary>
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