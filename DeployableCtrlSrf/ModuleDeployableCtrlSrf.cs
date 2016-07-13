using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DeployableCtrlSrf
{
    public class ModuleDeployableCtrlSrf:PartModule
    {
        //Index of IScalarModule
        [KSPField]
        public int DeployModuleIndex = 0;

        //Deployed Position of the IScalarModule
        [KSPField]
        public int DeployModulePos = 1;

        public bool isDeployed = true;

        //The IScalarModule
        private IScalarModule DeployModule
        {
            get
            {
                return this.part.Modules[DeployModuleIndex] as IScalarModule;
            }
        }

        //Check deployment each frame (Is there a better way?)
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (isDeployed != (DeployModule.GetScalar == DeployModulePos))
                ToggleDeployment();
        }

        //Deployment controller
        private void ToggleDeployment()
        {
            if (!isDeployed)
            {
                this.part.ShieldedFromAirstream = false;
                isDeployed = true;
            }
            else
            {
                this.part.ShieldedFromAirstream = true;
                isDeployed = false;
            }
        }
    }
}
