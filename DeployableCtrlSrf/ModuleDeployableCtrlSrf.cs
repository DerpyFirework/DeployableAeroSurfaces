using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private IScalarModule DeployModule;
        private ModuleAeroSurface AeroSurfaceModule;
        private ModuleControlSurface ControlSurfaceModule;
        private float aeroSrfRange;
        private float aeroSrfLift;
        private float ctrlSrfRange;
        private float ctrlSrfLift;


        //Initial set up of the values
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            AeroSurfaceModule = part.FindModuleImplementing<ModuleAeroSurface>();
            ControlSurfaceModule = part.FindModuleImplementing<ModuleControlSurface>();
            DeployModule = part.Modules[DeployModuleIndex] as IScalarModule;

            //Gets the values from the modules
            if (AeroSurfaceModule != null)
            {
                aeroSrfRange = AeroSurfaceModule.ctrlSurfaceRange;
                aeroSrfLift = AeroSurfaceModule.deflectionLiftCoeff;
            }
            if (ControlSurfaceModule != null)
            {
                ctrlSrfRange = ControlSurfaceModule.ctrlSurfaceRange;
                ctrlSrfLift = ControlSurfaceModule.deflectionLiftCoeff;
            }
        }

        public void FixedUpdate()
        {
            //Keep checking the deployment state
            if (DeployModule.GetScalar == DeployModulePos)
                enableSurface();
            else
                disableSurface();
        }

        public void disableSurface()
        {
            //Disable the aero surfaces
            if (AeroSurfaceModule != null)
            {
                AeroSurfaceModule.ctrlSurfaceRange = 0.01f;
                AeroSurfaceModule.deflectionLiftCoeff = 0.01f;
            }
            if (ControlSurfaceModule != null)
            {
                ControlSurfaceModule.ctrlSurfaceRange = 0.01f;
                ControlSurfaceModule.deflectionLiftCoeff = 0.01f;
            }
            isActive = false;
        }

        public void enableSurface()
        {
            //Enable the aero surfaces
            if (AeroSurfaceModule != null)
            {
                AeroSurfaceModule.ctrlSurfaceRange = aeroSrfRange;
                AeroSurfaceModule.deflectionLiftCoeff = aeroSrfLift;
            }
            if (ControlSurfaceModule != null)
            {
                ControlSurfaceModule.ctrlSurfaceRange = ctrlSrfRange;
                ControlSurfaceModule.deflectionLiftCoeff = ctrlSrfLift;
            }
            isActive = true;
        }
    }
}
