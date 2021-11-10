using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.RestAPI.Core.Interfaces;
using Rhino.Geometry;
using System.Drawing;
using HB.RestAPI.Core.Types;
using HbConnector.Core.Interfaces;

namespace GHHBConnector.Core
{
    // TODO: Create HBConnector.Core repo
    public class GHMeshConverter : IGeometryConverter<Mesh>
    {
       
        public IHbObject ToHbType(Mesh mesh)
        {
            if (!mesh.IsValid)
                throw new ArgumentException("Mesh is not valid!");

            var vertices = mesh.Vertices;

            var faces = mesh.Faces;

            var colors = mesh.VertexColors;

            int totalVertices = vertices.Count;

            int totalFaces = faces.Count;

            int totalColors = colors.Count;

            var hbMeshVertices = new double[totalVertices][];

            var hbMeshFaces = new int[totalFaces][];

            var hbMeshColors = new int[totalColors][];

            for (int i = 0; i < totalVertices; i++)
            {
                var vertex = vertices[i];

                hbMeshVertices[i] = new double[] { vertex.X, vertex.Y, vertex.Z };
            }

            for (int i = 0; i < totalFaces; i++)
            {
                var face = faces[i];

                if (face.IsQuad)
                    hbMeshFaces[i] = new[] { face.A, face.B, face.C, face.D };

                if (face.IsTriangle)
                    hbMeshFaces[i] = new[] { face.A, face.B, face.C};
            }

            for (int i = 0; i < totalColors; i++)
            {
                var color = colors[i];

                hbMeshColors[i] = new int[] { color.A, color.R, color.G, color.B };
            }

            return new HbMesh(hbMeshVertices, hbMeshFaces, hbMeshColors);
        }
    }
}
