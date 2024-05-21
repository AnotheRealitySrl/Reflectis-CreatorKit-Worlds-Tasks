using Reflectis.PLG.Graphs;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
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
            go.AddComponent<ManipulableGrabberDetector>();
        }
    }
}
