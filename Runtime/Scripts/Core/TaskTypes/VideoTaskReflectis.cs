using Reflectis.PLG.Graphs;
using Reflectis.PLG.Tasks;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
{
    public class VideoTaskReflectis : TaskReflectis, ITaskNode<VideoTaskNode>
    {
        VideoTaskNode IContainer<VideoTaskNode>.Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void AddDetector()
        {
            base.AddDetector();
            GameObject go = new GameObject("VideoDetector");
            go.transform.SetParent(gameObject.transform);
            go.AddComponent<TaskReactor>();
            go.AddComponent<VideoPlayerDetector>().enabled = false;
        }
    }
}
