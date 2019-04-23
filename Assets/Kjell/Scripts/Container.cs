using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class Container : MonoBehaviour
	{
		[FormerlySerializedAs("LayoutElement")]
		public LayoutElement layoutElement;

		[FormerlySerializedAs("TextTransform")]
		public RectTransform textTransform;

		// Something like this can be used to update live when user is writing
		public void UpdateWidth()
		{
			if (textTransform.rect.width >= 275)
			{
				layoutElement.preferredWidth = 300;
			}
			else if (textTransform.rect.width < 240)
			{
				layoutElement.preferredWidth = -1;
			}
		}

		public void SetWidth(int length)
		{
			if (length >= 50)
			{
				layoutElement.preferredWidth = 300;
			}
		}
	}
}
