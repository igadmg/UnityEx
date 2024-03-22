using NUnit.Framework;
using System;
using System.Collections;
using System.Reflection;
using SystemEx;

namespace UnityDissolve.Test
{
	[TestFixture]
	public class DissolvedTypeTest
	{
		private IComparer DissolvedTypeItemComparer = LambdaComparer.Create(
					(Tuple<string, FieldInfo> a, Tuple<string, FieldInfo> b) => {
						return string.Compare(a.Item1, b.Item2.Name);
					}
				);

		[Test]
		public void TaggedClass()
		{
			var dt = new DissolvedType(typeof(TaggedDissolveClass));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(3));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(0));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(0));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}

		[Test]
		public void TaggedClassWithAddComponentAndNamedField()
		{
			var dt = new DissolvedType(typeof(TaggedDissolveClassWithAddComponentsAndNamedField));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(1));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(2));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(0));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.AddComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.AddComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.AddComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.AddComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item1 == "Collider"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}

		[Test]
		public void TaggedClassWithNamedField()
		{
			var dt = new DissolvedType(typeof(TaggedDissolveClassWithNamedField));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(3));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(0));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(0));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item1 == "Collider"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}

		[Test]
		public void UntaggedClassWithNamedField()
		{
			var dt = new DissolvedType(typeof(UntaggedDissolveClassWithNamedField));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(2));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(0));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(0));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item1 == "Collider"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}

		[Test]
		public void UntaggedClassWithAddComponentAndNamedField()
		{
			var dt = new DissolvedType(typeof(UntaggedDissolveClassWithAddComponentsAndNamedField));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(1));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(1));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(0));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.AddComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.AddComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.AddComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.AddComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item1 == "Collider"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}

		[Test]
		public void UntaggedClassWithNestedComponents()
		{
			var dt = new DissolvedType(typeof(UntaggedDissolveClassWithNestedComponents));

			Assert.That(dt.ComponentFields, Has.Count.EqualTo(1));
			Assert.That(dt.AddComponentFields, Has.Count.EqualTo(0));
			Assert.That(dt.SubComponents, Has.Count.EqualTo(1));

			Assert.That(dt.ComponentFields, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "gameObject"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshFilter"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "meshCollider"));

			Assert.That(dt.SubComponents, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "nestedClass"));

			Assert.That(dt.SubComponents, Has.Exactly(1).Matches((Tuple<string, FieldInfo> i) => i.Item1 == "Nested"));

			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldString"));
			Assert.That(dt.ComponentFields, Has.None.Matches((Tuple<string, FieldInfo> i) => i.Item2.Name == "ommitedFieldInt"));
		}
	}
}
