using UnityEngine;
using UnityEngine.UI;

namespace hardartcore.CasualGUI
{
    public class SpriteChanger : MonoBehaviour
    {
        public Slider slider;
        public Sprite enabledSprite;
        public Sprite disabledSprite;

        private Image m_Image;

        public void Awake()
        {
            m_Image = GetComponent<Image>();
            slider.wholeNumbers = true;
        }

        private void Start()
        {
            // Init based on Slider's value
            ChangeSprite();
        }

        public void ChangeSprite()
        {
            if (slider.value == slider.minValue)
            {
                m_Image.sprite = disabledSprite;
            }
            else
            {
                m_Image.sprite = enabledSprite;
            }
        }

    }
}
