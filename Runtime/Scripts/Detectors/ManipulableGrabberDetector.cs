
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

        //private Grabbable grabbableObj;

        // Start is called before the first frame update
        void Start()
        {
            if (interactablePlaceholder.InteractionModes.HasFlag(EInteractableType.Manipulable))
            {
                Debug.LogError("This is a maniupulable object and so I have to add the detector when grabbed");
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
                Debug.LogError("ok the object has now a manipulable after the setup!!!!");
                Debug.LogError(manipulable);
                //Debug.LogError(manipulable.OnGrabManipulableStart);
                //manipulable.OnGrabManipulableStart.AddListener(OnGrabStartEvent);

            }
        }

        private void OnGrabStartEvent()
        {
            //OnGrabStart.Invoke();

            Debug.LogError("WORKED START EVENT ON MANIPULABLE");
        }

        private void OnGrabEndEvent()
        {
            OnGrabEnd.Invoke();
            Debug.LogError("WORKED END EVENT ON MANIPULABLE");
        }

    }
}

