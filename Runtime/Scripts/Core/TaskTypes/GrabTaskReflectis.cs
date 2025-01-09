using Reflectis.SDK.Graphs;
using Reflectis.SDK.Tasks;
using UnityEngine;

namespace Reflectis.SDK.TasksReflectis
{
    public class GrabTaskReflectis : TaskReflectis, ITaskNode<GrabTaskNode>
    {
        GrabTaskNode IContainer<GrabTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        ///////////////////////////////////////////////////////////////////////////
        /*private void Awake()
        {
            OnStatusChanged(oldStatus: TaskStatus.Locked);
            Node.onStatusChanged.AddListener(OnStatusChanged);
        }*/

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("ManipulableDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TaskReactor>();
            go.AddComponent<ManipulableGrabberDetector>().enabled = false;
        }
    }
}
