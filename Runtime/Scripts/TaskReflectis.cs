using Reflectis.PLG.Tasks;
using static Reflectis.PLG.Tasks.TaskNode;
using Reflectis.PLG.Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reflectis.PLG.TasksNetworked;

namespace Reflectis.PLG.TasksReflectis
{
    public class TaskReflectis : Task
    {
        private bool forceCompleted = false;
        [SerializeField]
        private int taskID;

        public int TaskID { get => taskID; }

        private void Start()
        {
            if (TaskSystemReflectis.Instance.isNetworked)
            {
                TasksRPCManager.Instance.OnTaskCompleted += ForceTaskComplete;
                forceCompleted = false;
            }
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
