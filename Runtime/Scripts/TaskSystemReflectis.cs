using Reflectis.PLG.Tasks;
using UnityEngine;

namespace Reflectis.PLG.TasksReflectis
{
    public class TaskSystemReflectis : TaskSystem
    {
        [SerializeField] public bool isNetworked { get; private set; }

        public ITasksRPCManager rpcManagerInterface { get; private set; }
        public static TaskSystemReflectis Instance { get; private set; }


        //questo deve prendersi istanza dell'rpcManager che viene generato tramite il placeholder, in realtà si piglia l'interfaccia. 
        //dovrò poi aspettare che mi arrivi questo componennt prima di fare il Prepare nel caso in cui io sia networkato

        protected override void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            if (isNetworked)
            {
                //wait for the placeholder to get the rpcManagerInterface, then get the interface and save it to my variables
                while (gameObject.GetComponent<ITasksRPCManager>() == null)
                {
                    rpcManagerInterface = gameObject.GetComponent<ITasksRPCManager>();
                }
            }

            base.Awake();
        }
    }
}
