using Reflectis.PLG.Graphs;
using Reflectis.PLG.Tasks.Detectors;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
{
    public class TriggerTaskReflectis : TaskReflectis, ITaskNode<TriggerTaskNode>
    {
        TriggerTaskNode IContainer<TriggerTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("TriggerDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TriggerDetector>();
        }
    }
}
