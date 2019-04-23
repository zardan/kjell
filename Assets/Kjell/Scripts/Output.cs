using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class Output : MonoBehaviour
	{
		[FormerlySerializedAs("Image")]
		public Image image;

		[FormerlySerializedAs("Opacity")]
		public float opacity;

		[FormerlySerializedAs("Text")]
		public Text text;

		void Start()
		{
			// Change the transparency of the output message (0 is transparent, 1 is opaque)
			if (text.text == "")
			{
				Color tempColor = image.color;
				tempColor.a = opacity;
				image.color = tempColor;
			}
		}
	}
}
