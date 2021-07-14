using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GrasshopperHbConnector
{
    public class GrasshopperHbConnectorInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "GrasshopperHbConnector";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("4e3c1cdc-de7b-4ee5-a80c-f8fce1e36fb6");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
