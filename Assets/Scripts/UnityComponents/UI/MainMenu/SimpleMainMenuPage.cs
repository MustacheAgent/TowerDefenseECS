using UnityEngine;

namespace UnityComponents.UI.MainMenu
{
    public class SimpleMainMenuPage : MonoBehaviour, IMainMenu
    {
        public Canvas canvas;
        
        public virtual void Hide()
        {
            if (canvas != null)
            {
                canvas.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void Show()
        {
            if (canvas != null)
            {
                canvas.enabled = true;
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}