using ProcGenEx;
using UnityDissolve;
using UnityEngine;

namespace ProcGenEx.Test
{
	public class PlaneTest : DissolvedMonoBehaviour
	{
		[Component]
		public MeshFilter meshFilter;

		public bool IsSimple = true;
		//public bool IsSphere = false;
		public int Subdivisions = 0;
		public int Steps = 1;

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
	
			var mb = IsSimple
				? PlaneMesh.Create()
				: PlaneMesh.Create();

			for (int i = 0; i < Subdivisions; i++)
			{
				mb.Subdivide(Steps);
			}

			//if (IsSphere)
			//{
			//	mb.Sphere(0.5f);
			//}

			meshFilter.mesh = mb.ToMesh();
			meshFilter.mesh.RecalculateNormals();
		}
#endif
	}
}