﻿using System.Collections.Generic;
using System.Linq;
using CircularBuffer;
using hhnl.HomeAssistantNet.Shared.Automation;

namespace hhnl.HomeAssistantNet.Automations.Automation
{
    public class AutomationEntry
    {
        private const int RunLimit = 10;

        private CircularBuffer<AutomationRunInfo> _runs = new(RunLimit);

        public AutomationEntry(AutomationInfo automationRunInfo)
        {
            Info = automationRunInfo;
        }

        public AutomationInfo Info { get; }

        public AutomationRunInfo? LatestRun
        {
            get
            {
                lock (_runs)
                {
                    return _runs.IsEmpty ? null : _runs.Front();
                }
            }
        }

        public IReadOnlyCollection<AutomationRunInfo> Runs
        {
            get
            {
                lock (_runs)
                {
                    return _runs.ToArray();
                }
            }
            set
            {
                lock (_runs)
                {
                    _runs = new CircularBuffer<AutomationRunInfo>(RunLimit, value.ToArray());
                }
            }
        }

        public void AddRun(AutomationRunInfo run)
        {
            lock (this)
            {
                _runs.PushFront(run);
            }
        }

        public void RemoveRun(AutomationRunInfo run)
        {
            lock (this)
            {
                if (_runs.Front() == run)
                {
                    _runs.PopFront();
                }
                else
                {
                    _runs = new(RunLimit, _runs.Except(new[] { run }).ToArray());
                }

            }
        }
    }
}