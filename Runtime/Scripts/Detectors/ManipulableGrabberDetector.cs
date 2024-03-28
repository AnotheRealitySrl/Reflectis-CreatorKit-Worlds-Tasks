
using Reflectis.SDK.CreatorKit;
using UnityEngine;
using static Reflectis.SDK.InteractionNew.IInteractable;

namespace Reflectis.PLG.TasksReflectis
{
    public class ManipulableGrabberDetector : MonoBehaviour
    {
        public InteractablePlaceholder interactablePlaceholder;

        // Start is called before the first frame update
        void Start()
        {
            if (interactablePlaceholder.InteractionModes.HasFlag(EInteractableType.Manipulable))
            {
                Debug.LogError("This is a maniupulable object and so I have to add the detector when grabbed");
            }
        }
    }
}

