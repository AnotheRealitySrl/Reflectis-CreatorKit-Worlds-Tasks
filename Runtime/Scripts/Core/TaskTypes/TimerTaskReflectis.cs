using Reflectis.PLG.Graphs;
using Reflectis.PLG.Tasks.Detectors;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
{
    public class TimerTaskReflectis : TaskReflectis, ITaskNode<TimerTaskNode>
    {
        TimerTaskNode IContainer<TimerTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        ///////////////////////////////////////////////////////////////////////////
        /*private void Awake()
        {
            OnStatusChanged(oldStatus: TaskStatus.Locked);
            Node.onStatusChanged.AddListener(OnStatusChanged);
        }*/

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("TimerDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TimerDetector>();
        }
    }
}
