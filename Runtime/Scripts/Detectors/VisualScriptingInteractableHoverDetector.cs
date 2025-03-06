using Reflectis.CreatorKit.Worlds.Core.Interaction;
using Reflectis.CreatorKit.Worlds.Placeholders;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.CreatorKit.Worlds.Tasks
{
    public class VisualScriptingInteractableHoverDetector : MonoBehaviour
    {
        [HideInInspector]
        public InteractablePlaceholderObsolete interactablePlaceholder;
        public VisualScriptingInteractablePlaceholder visualscriptingPlaceholder;
        [SerializeField]
        private bool allowRay;
        [SerializeField]
        private bool allowHandDirect;
        [SerializeField]
        private bool allowMouse;

        public UnityEvent OnHoverEnter = default;
        public UnityEvent OnHoverExit = default;


        public async void Setup()
        {
            if (visualscriptingPlaceholder == null)
            {
                return;
            }

            IVisualScriptingInteractable visualScriptingInteractable = null;
            while (!visualscriptingPlaceholder.TryGetComponent(out visualScriptingInteractable))
            {
                await Task.Yield();
            }
            if (visualScriptingInteractable != null)
            {
                if (allowHandDirect)
                {
                    visualScriptingInteractable.OnHoverGrabEnter.AddListener(OnHoverEnterEvent);
                    visualScriptingInteractable.OnHoverGrabExit.AddListener(OnHoverExitEvent);
                }

                if (allowRay)
                {
                    visualScriptingInteractable.OnHoverRayEnter.AddListener(OnHoverEnterEvent);
                    visualScriptingInteractable.OnHoverRayExit.AddListener(OnHoverExitEvent);
                }

                if (allowMouse)
                {
                    visualScriptingInteractable.OnHoverMouseEnter.AddListener(OnHoverEnterEvent);
                    visualScriptingInteractable.OnHoverMouseExit.AddListener(OnHoverExitEvent);
                }
            }
        }

        public void OnEnable()
        {
            Setup();
        }

        public void OnDisable()
        {
            IVisualScriptingInteractable interactable = visualscriptingPlaceholder.gameObject.GetComponent<IVisualScriptingInteractable>();
            if (interactable != null)
            {
                if (allowHandDirect)
                {
                    interactable.OnHoverGrabEnter.RemoveListener(OnHoverEnterEvent);
                    interactable.OnHoverGrabExit.RemoveListener(OnHoverExitEvent);
                }

                if (allowRay)
                {
                    interactable.OnHoverRayEnter.RemoveListener(OnHoverEnterEvent);
                    interactable.OnHoverRayExit.RemoveListener(OnHoverExitEvent);
                }

                if (allowMouse)
                {
                    interactable.OnHoverMouseEnter.RemoveListener(OnHoverEnterEvent);
                    interactable.OnHoverMouseExit.RemoveListener(OnHoverExitEvent);
                }
            }
        }

        private void OnHoverEnterEvent()
        {
            OnHoverEnter.Invoke();
        }

        private void OnHoverExitEvent()
        {
            OnHoverExit.Invoke();
        }
    }
}
