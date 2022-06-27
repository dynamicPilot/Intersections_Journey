using IJ.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.UIElements
{
    public class PanelWithSwitches : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private SpriteCollection _collection;

        protected int _index;
        private void Awake()
        {
            _index = 0;
        }
        public virtual void Move(int step)
        {
            _index += step;
            UpdateImage();
        }

        public void SetImageByIndex(int index)
        {
            _index = index;
            UpdateImage();
        }

        void UpdateImage()
        {
            if (_index < 0)
            {
                _index = _collection.Collection.Length - 1;
            }
            else if (_index > _collection.Collection.Length - 1)
            {
                _index = 0;
            }

            _image.sprite = _collection.Collection[_index];
        }

    }
}
