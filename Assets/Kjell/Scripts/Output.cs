using UnityEngine;
using UnityEngine.UI;

namespace Kjell
{
	public class Output : MonoBehaviour
	{
		public Text Text;
        public Image Image;
        public float Opacity;

        private void Start()
        {
            // Change the transparency of the output message (0 is transparent, 1 is opaque)
            if (Text.text == "")
            {
                var tempColor = Image.color;
                tempColor.a = Opacity; 
                Image.color = tempColor;
            }
        }
	}
}

