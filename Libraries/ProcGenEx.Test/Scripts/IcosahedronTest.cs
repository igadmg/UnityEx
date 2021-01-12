using ProcGenEx;
using UnityDissolve;
using UnityEngine;

namespace ProcGenEx.Test
{
	public class IcosahedronTest : DissolvedMonoBehaviour
	{
		[Component]
		public MeshFilter meshFilter;

		public bool IsSimple = true;
		public bool IsSphere = false;
		public int Subdivisions = 0;
		public int Steps = 1;

#if UNITY_EDITOR
		public override void OnValidate()
		{
			base.OnValidate();
	
			var mb = IsSimple
				? Icosahedron.CreateSimple()
				: Icosahedron.Create();

			for (int i = 0; i < Subdivisions; i++)
			{
				mb.Subdivide(Steps);
			}

			if (IsSphere)
			{
				mb.Sphere(0.5f);
			}

			meshFilter.sharedMesh = mb.ToMesh();
			meshFilter.sharedMesh.RecalculateNormals();
		}
#endif
	}
}