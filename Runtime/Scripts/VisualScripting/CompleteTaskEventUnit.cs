using Reflectis.PLG.Tasks;
using Unity.VisualScripting;

namespace Reflectis.PLG.TasksReflectis
{
    [UnitTitle("Reflectis Tasks: CompleteTask")]
    [UnitSurtitle("Tasks")]
    [UnitShortTitle("CompleteTask")]
    [UnitCategory("Reflectis\\Flow")]
    public class CompleteTaskEventUnit : Unit
    {

        protected GraphReference graphReference;

        protected Task taskReference; //should simply create the vent taskComplete to complete the task howevereI want yeah

        [DoNotSerialize]
        public ValueInput TaskToComplete { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger { get; private set; }
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger { get; private set; }

        protected override void Definition()
        {
            inputTrigger = ControlInput(nameof(inputTrigger), Output);
            outputTrigger = ControlOutput("outputTrigger");
            TaskToComplete = ValueInput<Task>(nameof(TaskToComplete), null).NullMeansSelf();
            Succession(inputTrigger, outputTrigger);

        }

        private ControlOutput Output(Flow flow)
        {
            flow.GetValue<Task>(TaskToComplete).CompleteTask();
            return outputTrigger;
        }
    }

}
