using Reflectis.SDK.Tasks;
using Unity.VisualScripting;

namespace Reflectis.SDK.TasksReflectis
{
    [UnitTitle("Reflectis Tasks: On Task Completed")]
    [UnitSurtitle("Tasks")]
    [UnitShortTitle("On Task Completed")]
    [UnitCategory("Events\\Reflectis")]
    public class OnTaskCompletedEventUnit : EventUnit<Task>
    {
        protected override bool register => true;

        protected GraphReference graphReference;

        protected Task taskReference;

        [DoNotSerialize]
        public ValueInput TaskReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            TaskReference = ValueInput<Task>(nameof(TaskReference), null).NullMeansSelf();
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;
            using (var flow = Flow.New(reference))
            {
                RegisterTaskOnCompleted(flow);
            }
            return new EventHook("Task" + this.ToString().Split("EventUnit")[0]);
        }

        private void RegisterTaskOnCompleted(Flow flow)
        {
            taskReference = flow.GetValue<Task>(TaskReference);
            if (taskReference == null)
            {
            }
            else
            {
                taskReference.OnTaskCompleted.AddListener(OnTaskCompletedUnit);
            }
        }

        private void OnTaskCompletedUnit(bool value)
        {
            Trigger(graphReference, taskReference);
        }
    }
}
