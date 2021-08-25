using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using GHHBConnector.Core;
using HB.RestAPI.Core.Services;
using HB.RestAPI.Core.Models;
using HB.RestAPI.Core.Settings;
using HB.RestAPI.Core.Types;
using Rhino;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace GrasshopperHbConnector
{
    
    public class HbServerSenderComponent : GH_Component
    {
        private readonly HBApiClient _hbApiClient;

        private const string AsyncPostEndpoint = HbApiEndPoints.AsyncPostEndPoint;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public HbServerSenderComponent()
          : base("HbServerSender", "Sender",
              "Description",
              "Hb Connector", "Sender")
        {
           
            _hbApiClient = new HBApiClient(new JsonSerializer());
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data nodes", "D", "A collection of data nodes to send.", GH_ParamAccess.list);

            pManager.AddTextParameter("Project stream", "S", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Info", "I", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var dataNodes = new List<DataNode>();

            string projectStream = "";

            DA.GetDataList(0, dataNodes);

            DA.GetData(1, ref projectStream);

            var applicationDataContainer = new ApplicationDataContainer(dataNodes, projectStream);

           var task =  _hbApiClient.AsyncPostRequest(AsyncPostEndpoint, applicationDataContainer);

           if(!task.IsCompleted)
                RhinoApp.Wait();

           DA.SetData(0, "Successfully sent!");

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("46a8ad6a-769c-45eb-9120-80742bc018eb"); }
        }
    }
}
