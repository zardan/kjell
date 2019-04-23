using UnityEngine;
using UnityEngine.UI;

namespace Kjell
{
	public class Container : MonoBehaviour
	{
		public RectTransform TextTransform;
		public LayoutElement LayoutElement;

		// Something like this can be used to update live when user is writing
		public void UpdateWidth()
		{
			if (TextTransform.rect.width >= 275)
				LayoutElement.preferredWidth = 300;
			else if (TextTransform.rect.width < 240)
				LayoutElement.preferredWidth = -1;
		}

		public void SetWidth(int length)
		{
			if (length >= 50)
				LayoutElement.preferredWidth = 300;
		}
	}
}
