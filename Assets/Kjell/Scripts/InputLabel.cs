using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class InputLabel : MonoBehaviour
	{
		[FormerlySerializedAs("BubbleImage")]
		public Image bubbleImage;

		[FormerlySerializedAs("Text")]
		public Text text;

		void Start()
		{
			if (text.text == "")
			{
				Destroy(gameObject);
			}
		}
	}
}
