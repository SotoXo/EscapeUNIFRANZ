using UnityEngine;
using UnityEngine.UI;

namespace EscapeUNIFRANZ.UI
{
    public sealed class ObjectiveView : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Text label;

        public void Show(string text)
        {
            if (root == null || label == null || string.IsNullOrWhiteSpace(text))
            {
                Hide();
                return;
            }

            label.text = text;
            root.SetActive(true);
        }

        public void Hide()
        {
            root?.SetActive(false);
        }
    }
}
