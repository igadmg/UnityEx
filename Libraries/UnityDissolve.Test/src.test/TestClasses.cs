using UnityEngine;

namespace UnityDissolve.Test
{
	[Component]
	public class TaggedDissolveClass
	{
		public GameObject gameObject;
		public MeshFilter meshFilter;
		private MeshCollider meshCollider;

		public string ommitedFieldString;
		private int ommitedFieldInt;
	}

	[Component]
	public class TaggedDissolveClassWithAddComponentsAndNamedField
	{
		public GameObject gameObject;
		[AddComponent]
		public MeshFilter meshFilter;
		[AddComponent("Collider")]
		private MeshCollider meshCollider;

		public string ommitedFieldString;
		private int ommitedFieldInt;
	}

	[Component]
	public class TaggedDissolveClassWithNamedField
	{
		public GameObject gameObject;
		public MeshFilter meshFilter;
		[Component("Collider")] private MeshCollider meshCollider;
	}

	public class UntaggedDissolveClassWithNamedField
	{
		[Component]
		public GameObject gameObject;
		public MeshFilter meshFilter;
		[Component("Collider")]
		private MeshCollider meshCollider;

		public string ommitedFieldString;
		private int ommitedFieldInt;
	}

	public class UntaggedDissolveClassWithAddComponentsAndNamedField
	{
		[Component]
		public GameObject gameObject;
		public MeshFilter meshFilter;
		[AddComponent("Collider")]
		private MeshCollider meshCollider;

		public string ommitedFieldString;
		private int ommitedFieldInt;
	}

	public class UntaggedDissolveClassWithNestedComponents
	{
		[Component]
		public GameObject gameObject;
		public MeshFilter meshFilter;
		[Component("Nested")]
		private TaggedDissolveClass nestedClass;

		public string ommitedFieldString;
		private int ommitedFieldInt;
	}
}
