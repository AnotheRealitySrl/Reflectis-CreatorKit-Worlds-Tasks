using Reflectis.SDK.Graphs;
using Reflectis.SDK.Tasks;
using Reflectis.SDK.Tasks.Detectors;
using UnityEngine;

namespace Reflectis.SDK.TasksReflectis
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
