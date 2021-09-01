using System;
using System.Collections.Generic;
using GHHBConnector.Core;
using Grasshopper.Kernel;
using HB.RestAPI.Core.Models;
using HB.RestAPI.Core.Services;
using HB.RestAPI.Core.Types;
using Rhino.Geometry;


namespace GrasshopperHbConnector
{
    public class MeshConverterComponent : GH_Component
    {
        private readonly GHMeshConverter _ghmeshConverter;

        private readonly JsonSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the MeshConverter class.
        /// </summary>
        public MeshConverterComponent()
           : base("HbMeshConverter", "MC",
              "Description",
              "Hb Connector", "Converters")
        {
            _ghmeshConverter = new GHMeshConverter();

            _serializer = new JsonSerializer();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "A grasshopper mesh", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data nodes", "D", "A collection of data nodes.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var ghMeshes = new List<Mesh>();

            DA.GetDataList(0, ghMeshes);

            var dataNodes = new List<DataNode>(ghMeshes.Count);

            foreach (var ghMesh in ghMeshes)
            {
                var hbMesh = _ghmeshConverter.ToHbMesh(ghMesh);

                string hbMeshJson = _serializer.Serialize(hbMesh);

                var dataNode = new DataNode(hbMeshJson, typeof(HbMesh));

                dataNodes.Add(dataNode);
            }

            DA.SetDataList(0, dataNodes);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("AA98E49B-B50A-4A1C-A6FD-70023D4B17F2"); }
        }
    }
}