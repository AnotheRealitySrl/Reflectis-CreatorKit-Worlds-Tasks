using Reflectis.PLG.Graphs;
using Reflectis.PLG.Tasks;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
{
    public class AnimatorTaskReflectis : TaskReflectis, ITaskNode<AnimatorTaskNode>
    {
        AnimatorTaskNode IContainer<AnimatorTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("AnimatorDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TaskReactor>();
            go.AddComponent<AnimatorReverseDetector>().enabled = false;
        }
    }
}
