using System;
using UnityEngine;

namespace DeployableCtrlSrf
{
    public class ModuleDeployableLiftSrf : PartModule
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
        private ModuleLiftingSurface liftSurface;
        private float lift;


        //Initial set up of the values
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            liftSurface = part.FindModuleImplementing<ModuleLiftingSurface>();
            deployModule = part.Modules[DeployModuleIndex] as IScalarModule;

            //Gets the values from the modules
            if (liftSurface != null)
                lift = liftSurface.deflectionLiftCoeff;
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
            //Disable the lift surface
            if (liftSurface != null)
                liftSurface.deflectionLiftCoeff = 0.01f;
            isActive = false;
        }

        public void enableSurface()
        {
            //Enable the aero surfaces
            if (liftSurface != null)
                liftSurface.deflectionLiftCoeff =lift;
            isActive = true;
        }
    }
}
