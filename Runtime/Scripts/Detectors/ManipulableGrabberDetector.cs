
using Reflectis.SDK.CreatorKit;
using Reflectis.SDK.InteractionNew;
using UnityEngine;
using UnityEngine.Events;
using static Reflectis.SDK.InteractionNew.IInteractable;

namespace Reflectis.PLG.TasksReflectis
{
    public class ManipulableGrabberDetector : MonoBehaviour
    {
        public InteractablePlaceholder interactablePlaceholder;
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
            Manipulable manipulable = interactablePlaceholder.gameObject.GetComponent<Manipulable>();

            if (manipulable)
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
            Manipulable manipulable = interactablePlaceholder.gameObject.GetComponent<Manipulable>();

            if (manipulable)
            {
                if (manipulable.OnGrabManipulableStart == null)
                {
                    manipulable.OnGrabManipulableStart = new UnityEvent();
                }

                Debug.LogError("Demove all lsiteners");
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

