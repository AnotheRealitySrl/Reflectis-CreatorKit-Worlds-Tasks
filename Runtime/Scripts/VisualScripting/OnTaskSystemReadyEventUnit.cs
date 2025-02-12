using Reflectis.SDK.Tasks;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Tasks
{
    [UnitTitle("Reflectis Tasks: On Task System Ready")]
    [UnitSurtitle("Tasks")]
    [UnitShortTitle("On Task System Ready")]
    [UnitCategory("Events\\Reflectis")]
    public class OnTaskSystemReadyEventUnit : EventUnit<TaskSystemReflectis>
    {
        protected override bool register => true;

        protected GraphReference graphReference;

        protected TaskSystemReflectis taskSystemReference;

        [DoNotSerialize]
        public ValueInput TaskSystemReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            TaskSystemReference = ValueInput<Task>(nameof(TaskSystemReference), null).NullMeansSelf();
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;
            using (var flow = Flow.New(reference))
            {
                RegisterSystemOnReady(flow);
            }
            return new EventHook("Task" + this.ToString().Split("EventUnit")[0]);
        }

        private void RegisterSystemOnReady(Flow flow)
        {
            taskSystemReference = flow.GetValue<TaskSystemReflectis>(TaskSystemReference);
            if (taskSystemReference == null)
            {
            }
            else
            {
                taskSystemReference.OnTaskSystemReady.AddListener(OnTaskSystemReadyUnit);
            }
        }

        private void OnTaskSystemReadyUnit()
        {
            Trigger(graphReference, taskSystemReference);
        }
    }
}
