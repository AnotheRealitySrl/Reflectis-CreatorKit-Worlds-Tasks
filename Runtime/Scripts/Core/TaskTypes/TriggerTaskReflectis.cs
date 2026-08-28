using Virtuademy.SDK.Graphs;
using Virtuademy.SDK.Tasks;
using Virtuademy.SDK.Tasks.Detectors;
using UnityEngine;

namespace Virtuademy.CreatorKit.Worlds.Tasks
{
    public class TriggerTaskReflectis : TaskReflectis, ITaskNode<TriggerTaskNode>
    {
        TriggerTaskNode IContainer<TriggerTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("TriggerDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TaskReactor>();
            go.AddComponent<TriggerDetector>().enabled = false;
        }
    }
}
