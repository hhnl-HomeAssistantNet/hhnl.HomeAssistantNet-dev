using hhnl.HomeAssistantNet.Automations.Automation;
using hhnl.HomeAssistantNet.Shared.Automation;

namespace hhnl.HomeAssistantNet.Automations.BuildingBlocks
{
    public static class CurrentRun
    {
        /// <summary>
        /// Sets the state of the current run to ignored. Ignored runs will be removed from the list of runs in the ui.
        /// </summary>
        public static void IgnoreRun()
        {
            AutomationRunContext.GetRunContextOrFail().CurrentRun.State = AutomationRunInfo.RunState.Ignored;
        }
    }
}
