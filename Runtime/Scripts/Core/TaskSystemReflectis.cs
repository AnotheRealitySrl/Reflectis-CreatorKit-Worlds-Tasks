using Reflectis.CreatorKit.Worlds.Core.ClientModels;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Tasks;
using Reflectis.SDK.Tasks.UI;

using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.CreatorKit.Worlds.Tasks
{
    public class TaskSystemReflectis : TaskSystem
    {
        public ITasksRPCManager rpcManagerInterface { get; private set; }
        //public static TaskSystemReflectis Instance { get; private set; }

        public TaskUIManager taskUIManager;
        private int initializedTaskDescriptions = 0;

        [SerializeField] private bool _isNetworked;
        public bool isNetworked
        {
            private set
            {
                _isNetworked = value;
            }
            get
            {
                return _isNetworked && SM.GetSystem<IClientModelSystem>().CurrentSession.Multiplayer;
            }
        }


        // VisualScripting usage
        public UnityEvent OnTaskSystemReady => taskSystemReady;


        //questo deve prendersi istanza dell'rpcManager che viene generato tramite il placeholder, in realt� si piglia l'interfaccia. 
        //dovr� poi aspettare che mi arrivi questo componennt prima di fare il Prepare nel caso in cui io sia networkato

        protected override void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            //if (Instance != null && Instance != this)
            //{
            //    Destroy(this);
            //}
            //else
            //{
            //    Instance = this;
            //}

            initializedTaskDescriptions = 0;

            /*if (isNetworked)
            {
                Debug.Log(gameObject.GetComponent<ITasksRPCManager>());
            }*/

            //TODO wait for all taskNode to update their descriptions....

            if (isNetworked)
            {
                StartCoroutine(AddRevertCallbacks());
            }

            base.Awake();
        }

        public IEnumerator WaitForRPCManager()
        {
            if (rpcManagerInterface == null)
            {
                while (gameObject.GetComponent<ITasksRPCManager>() == null)
                {
                    yield return null;
                }
                rpcManagerInterface = gameObject.GetComponent<ITasksRPCManager>();
            }
        }

        public IEnumerator AddRevertCallbacks()
        {
            if (rpcManagerInterface == null)
            {
                yield return StartCoroutine(WaitForRPCManager());
            }
            rpcManagerInterface.SetOnRevert(base.Revert);
        }

        //function called when the descriptions on the tasks have been initialized in the localizeTaskBridge
        public void InitializedTaskDescription()
        {
            initializedTaskDescriptions += 1;

            //this simply rebuilds the UI
            //taskSystemReady.Invoke();

            if (initializedTaskDescriptions == Tasks.Count)
            {
                //Debug.LogError("AllTasks initialized!");
                if (taskUIManager != null)
                {
                    taskUIManager.RebuildUIImmediately();
                }
            }
        }

        public override void Revert()
        {
            if (isNetworked)
            {
                rpcManagerInterface.SendRPCTaskRevert();
                rpcManagerInterface.UpdateTasksID(-1);
            }
            base.Revert();
        }

        public void RebuildTaskUI()
        {
            if (taskUIManager != null)
            {
                taskUIManager.RebuildUIImmediately();
            }
        }
    }
}
