using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SystemEx;
using Unity.VisualScripting;
using UnityDissolve;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngineEx;



namespace Assets.UnityEx.Tests.UnityEngineEx.Test {
	[ExecuteInEditMode]
	public class TestFindObject : MonoBehaviour {
		public bool trigger;

		public GameObject directName;
		public GameObject directSubName;
		public GameObject parentDirectName;
		public GameObject parentDirectSubName;
		public GameObject uniqueSearchable;
		public List<GameObject> searchable;

		private void Awake() {

		}

		private void OnValidate() {
			directName = gameObject.FindGameObject("DirectName").First();
			directSubName = gameObject.FindGameObject("DirectName/DirectSubName").First();
			parentDirectName = gameObject.FindGameObject("../DirectName").First();
			parentDirectSubName = gameObject.FindGameObject("../DirectName/DirectSubName").First();
			uniqueSearchable = gameObject.FindGameObject("**/UniqueSearchable").First();
			searchable = gameObject.FindGameObject("../**/Searchable").ToList();
		}
	}
}
