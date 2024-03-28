using Reflectis.PLG.Tasks;
using System.Collections;
using UnityEngine;
using static Reflectis.PLG.Tasks.TaskNode;

namespace Reflectis.PLG.TasksReflectis
{
    public class TaskReflectis : Task
    {
        private bool forceCompleted = false;
        [SerializeField]
        private int taskID;

        private ITasksRPCManager rpcManagerInterface;

        public int TaskID { get => taskID; }

        private void Start()
        {
            if (TaskSystemReflectis.Instance.isNetworked)
            {
                StartCoroutine(WaitForRPCManager());
            }
        }

        IEnumerator WaitForRPCManager()
        {
            StartCoroutine(TaskSystemReflectis.Instance.WaitForRPCManager());
            while (rpcManagerInterface == null)
            {
                rpcManagerInterface = TaskSystemReflectis.Instance.rpcManagerInterface;
                yield return null;
            }
            rpcManagerInterface.SetOnTaskCompleted(ForceTaskComplete);
            forceCompleted = false;
        }

        protected override void OnStatusChanged(TaskStatus oldStatus)
        {
            if ((Node.Status == TaskStatus.Completed && !forceCompleted) && TaskSystemReflectis.Instance.isNetworked)
            {

                TaskSystemReflectis.Instance.rpcManagerInterface.UpdateTasksID(taskID);
                TaskSystemReflectis.Instance.rpcManagerInterface.SendRPCTaskStatusChange(taskID);
            }

            forceCompleted = false;

            base.OnStatusChanged(oldStatus);
        }

        public void ForceTaskComplete(int id)
        {
            if (taskID != id)
                return;

            forceCompleted = true;
            CompleteTask();
        }

        //Auto-assign random id when task is created from graph
        private void Reset()
        {
            taskID = Mathf.Abs(gameObject.GetInstanceID());
        }
    }
}
