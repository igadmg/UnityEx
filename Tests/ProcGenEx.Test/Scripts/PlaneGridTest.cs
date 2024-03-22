using MathEx;
using System.Linq;
using UnityDissolve;
using UnityEngine;

namespace ProcGenEx.Test
{
	public class PlaneGridTest : DissolvedMonoBehaviour
	{
		[Component]
		public MeshFilter meshFilter;

		public vec2i gridSize = new vec2i(10, 10);
		public vec2 cellSize = vec2.one;


#if UNITY_EDITOR
		public override void OnValidate()
		{
			base.OnValidate();

			var mb = PlaneMesh.Create(gridSize, Foreach.Cell(gridSize, cellSize).Select(c => c.o));

			meshFilter.sharedMesh = mb.ToMesh();
			meshFilter.sharedMesh.RecalculateNormals();
		}
#endif

	}
}
