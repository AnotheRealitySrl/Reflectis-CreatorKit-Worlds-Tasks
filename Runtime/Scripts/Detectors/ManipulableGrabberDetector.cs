
using Reflectis.CreatorKit.Worlds.Core.Interaction;
using Reflectis.CreatorKit.Worlds.Placeholders;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.CreatorKit.Worlds.Core.Interaction.IInteractable;

namespace Reflectis.CreatorKit.Worlds.Tasks
{
    public class ManipulableGrabberDetector : MonoBehaviour
    {
        public InteractablePlaceholderObsolete interactablePlaceholder;
        public UnityEvent OnGrabStart = default;
        public UnityEvent OnGrabEnd = default;
        public UnityEvent OnRayGrabStart = default;
        public UnityEvent OnRayGrabEnd = default;

        //private Grabbable grabbableObj;

        // Start is called before the first frame update
        void Start()
        {
            if (interactablePlaceholder.InteractionModes.HasFlag(EInteractableType.Manipulable))
            {
                interactablePlaceholder.OnSetupFinished.AddListener(OnSetupFinished);
            }
        }

        public void OnSetupFinished()
        {
            //gameObject.GetOrAddComponent<Grabbable>();

            //add OnGrabEvent to the Manipulable. The ManipulableVR and ManipulableDesktop will have a callback on that event
            IManipulable manipulable = interactablePlaceholder.gameObject.GetComponent<IManipulable>();

            if (manipulable != null)
            {
                if (manipulable.OnGrabManipulableStart == null)
                {
                    manipulable.OnGrabManipulableStart = new UnityEvent();
                }
                manipulable.OnGrabManipulableStart.AddListener(OnGrabStartEvent);
                manipulable.OnGrabManipulableEnd.AddListener(OnGrabEndEvent);
                manipulable.OnRayGrabManipulableStart.AddListener(OnRayGrabStartEvent);
                manipulable.OnRayGrabManipulableEnd.AddListener(OnRayGrabEndEvent);
            }
        }

        public void OnEnable()
        {
            OnSetupFinished();
        }

        public void OnDisable()
        {
            IManipulable manipulable = interactablePlaceholder.gameObject.GetComponent<IManipulable>();

            if (manipulable != null)
            {
                if (manipulable.OnGrabManipulableStart == null)
                {
                    manipulable.OnGrabManipulableStart = new UnityEvent();
                }

                manipulable.OnGrabManipulableStart.RemoveListener(OnGrabStartEvent);
                manipulable.OnGrabManipulableEnd.RemoveListener(OnGrabEndEvent);
                manipulable.OnRayGrabManipulableStart.RemoveListener(OnRayGrabStartEvent);
                manipulable.OnRayGrabManipulableEnd.RemoveListener(OnRayGrabEndEvent);
            }
        }

        private void OnGrabStartEvent()
        {
            OnGrabStart.Invoke();
        }

        private void OnGrabEndEvent()
        {
            OnGrabEnd.Invoke();
        }

        private void OnRayGrabStartEvent()
        {
            OnRayGrabStart.Invoke();
        }

        private void OnRayGrabEndEvent()
        {
            OnRayGrabEnd.Invoke();
        }



    }
}

