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
		public int Subdivisions = 0;
		public int Steps = 1;

#if UNITY_EDITOR
		public override void OnValidate()
		{
			base.OnValidate();
	
			var mb = IsSimple
				? PlaneMesh.Create()
				: PlaneMesh.Create();

			for (int i = 0; i < Subdivisions; i++)
			{
				mb.Subdivide(Steps);
			}

			meshFilter.sharedMesh = mb.ToMesh();
			meshFilter.sharedMesh.RecalculateNormals();
		}
#endif
	}
}