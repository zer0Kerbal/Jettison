// https://kerbalspaceprogram.com/api/class_module_fuel_jettison.html
// ModuleFuelJettiscon

using System;
using System.Collections.Generic;
using UnityEngine;
using KSP;

namespace Jettison
{
    public class ModuleJettison : PartModule
    {
        public override void OnUpdate()
        {
            int count = 0;

            foreach (PartResource resource in part.Resources)
            {
                if (resource.resourceName == "ElectricCharge")
                {
                    continue;
                }
                else
                {
                    count++;
                }
            }

            if (count == 0)
            {
                foreach (BaseEvent ev in Events)
                {
                    if (ev.guiName == "Jettison resource")
                    {
                        ev.active = false;
                    }
                }

                base.OnUpdate();
            }
        }

        [KSPEvent(guiActive = true, guiActiveEditor = false, guiName = "Jettison Resource")]
        public void button_JettisonClicked()
        {
            PartResource liquidFuel = null;
            bool emptiedSomething = false;

            foreach (PartResource resource in part.Resources)
            {
                if (!resource.flowState || resource.resourceName == "ElectricCharge")
                {
                    continue;
                }

                if (resource.resourceName == "LiquidFuel")
                {
                    liquidFuel = resource;
                }
                else
                {
                    if (resource.amount > 0.01f)
                    {
                        resource.amount = 0f;
                        emptiedSomething = true;
                        continue;
                    }
                }
            }

            if (!emptiedSomething && liquidFuel != null)
            {
                liquidFuel.amount = 0f;
            }
        }
    }
}