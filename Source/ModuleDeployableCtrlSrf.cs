using System;
using UnityEngine;

namespace DeployableCtrlSrf
{
    public class ModuleDeployableCtrlSrf:PartModule
    {
        //Index of ModuleAnimateGeneric (Or other module implementing IScalarModule)
        [KSPField]
        public int DeployModuleIndex;

        //Position of deploy module
        [KSPField]
        public int DeployModulePos;

        //Variables for tracking values
        public bool isActive = false;
        private IScalarModule deployModule;
        private ModuleControlSurface controlSrfModule;
        private float range;
        private float lift;

        //Initial set up of the values
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            controlSrfModule = part.FindModuleImplementing<ModuleControlSurface>();
            deployModule = part.Modules[DeployModuleIndex] as IScalarModule;

            //Gets the values from the modules
            if (controlSrfModule != null)
            {
                range = controlSrfModule.ctrlSurfaceRange;
                lift = controlSrfModule.deflectionLiftCoeff;
            }
        }

        //I'm going to change this to an event soon
        public void FixedUpdate()
        {
            //Keep checking the deployment state
            if (deployModule.GetScalar == DeployModulePos)
                enableSurface();
            else
                disableSurface();
        }

        public void disableSurface()
        {
            //Disable the control surface
            if (controlSrfModule != null)
            {
                controlSrfModule.ctrlSurfaceRange = 0.01f;
                controlSrfModule.deflectionLiftCoeff = 0.01f;
            }
            isActive = false;
        }

        public void enableSurface()
        {
            //Enable the control surface
            if (controlSrfModule != null)
            {
                controlSrfModule.ctrlSurfaceRange = range;
                controlSrfModule.deflectionLiftCoeff = lift;
            }
            isActive = true;
        }
    }
}
