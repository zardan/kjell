using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class Output : MonoBehaviour
	{
		[FormerlySerializedAs("Text")]
		public Text text;
        [FormerlySerializedAs("Image")]
        public Image image;
        [FormerlySerializedAs("Opacity")]
        public float opacity;

        private void Start()
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

