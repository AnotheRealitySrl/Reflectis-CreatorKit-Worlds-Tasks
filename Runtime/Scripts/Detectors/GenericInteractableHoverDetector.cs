using Reflectis.CreatorKit.Worlds.Placeholders;
using Reflectis.SDK.Core.Interaction;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.SDK.Core.Interaction.IInteractable;

namespace Reflectis.CreatorKit.Worlds.Tasks
{
    public class GenericInteractableHoverDetector : MonoBehaviour
    {
        public InteractablePlaceholder interactablePlaceholder;
        [SerializeField]
        private bool allowRay;
        [SerializeField]
        private bool allowHandDirect;
        [SerializeField]
        private bool allowMouse;

        public UnityEvent OnHoverEnter = default;
        public UnityEvent OnHoverExit = default;

        // Start is called before the first frame update
        void Start()
        {
            if (interactablePlaceholder.InteractionModes.HasFlag(EInteractableType.GenericInteractable))
            {
                interactablePlaceholder.OnSetupFinished.AddListener(OnSetupFinished);
            }
        }

        public void OnSetupFinished()
        {
            //add OnGrabEvent to the Manipulable. The ManipulableVR and ManipulableDesktop will have a callback on that event
            GenericInteractable interactable = interactablePlaceholder.gameObject.GetComponent<GenericInteractable>();

            if (interactable)
            {
                if (allowHandDirect)
                {
                    interactable.OnHoverGrabEnter.AddListener(OnHoverEnterEvent);
                    interactable.OnHoverGrabExit.AddListener(OnHoverExitEvent);
                }

                if (allowRay)
                {
                    interactable.OnHoverRayEnter.AddListener(OnHoverEnterEvent);
                    interactable.OnHoverRayExit.AddListener(OnHoverExitEvent);
                }

                if (allowMouse)
                {
                    interactable.OnHoverMouseEnter.AddListener(OnHoverEnterEvent);
                    interactable.OnHoverMouseExit.AddListener(OnHoverExitEvent);
                }
            }
        }

        public void OnEnable()
        {
            OnSetupFinished();
        }

        public void OnDisable()
        {
            GenericInteractable interactable = interactablePlaceholder.gameObject.GetComponent<GenericInteractable>();
            if (interactable)
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
