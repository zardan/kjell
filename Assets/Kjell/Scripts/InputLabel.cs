using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class InputLabel : MonoBehaviour
	{
		[FormerlySerializedAs("Text")]
		public Text text;
        [FormerlySerializedAs("BubbleImage")]
        public Image bubbleImage;
        
        private void Start()
        {
            if (text.text == "")
            {
                Destroy(gameObject);
            }
        }

	}
}
