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
		public int Subdivisions = 0;
		public int Steps = 1;

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
	
			var mb = IsSimple
				? Icosahedron.CreateSimple()
				: Icosahedron.Create();

			for (int i = 0; i < Subdivisions; i++)
			{
				mb.Subdivide(Steps);
			}

			meshFilter.mesh = mb.ToMesh();
		}
#endif

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}