using System.IO;
using System.Reflection;
using SystemEx;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;



namespace UnityEditorEx
{
	class UnityExSettingsWindow : EditorWindow
	{
		protected static string m_SettingsPath = Path.GetFullPath("Library/UnityEx.asset");
		protected static string m_UnityEngineAssembliesPath = Path.GetFullPath("Assets/UnityEx/Assemblies");
		protected static string m_UnityEditorAssembliesPath = Path.GetFullPath("Assets/UnityEx/Editor/Assemblies");

		[MenuItem("UnityEx/Settings")]
		public static void ShowWindow()
		{
			UnityExSettingsWindow window = EditorWindow.GetWindow<UnityExSettingsWindow>(false, "UnityEx Settings");
		}



		private UnityExSettings settings;
		private string m_UnityEngineBuildPath;
		private string m_UnityEditorBuildPath;
		private bool m_CanImport;



		void OnGUI()
		{
			if (settings == null)
			{
				UnityEngine.Object[] objectArray = InternalEditorUtility.LoadSerializedFileAndForget(m_SettingsPath);
				if (objectArray != null && objectArray.Length > 0)
				{
					settings = objectArray[0] as UnityExSettings;
				}
				if (settings == null)
				{
					settings = CreateInstance<UnityExSettings>();
					settings.hideFlags = HideFlags.DontSave;
					settings.name = "UnityEx Settings";
					settings.buildName = "Release";
					InternalEditorUtility.SaveToSerializedFileAndForget(new UnityEngine.Object[1] { settings }, m_SettingsPath, true);
				}

				CheckPaths();
			}

			bool bIsDirty = false;
			EditorGUILayout.Space();

			GUILayout.Label("Project namespace:", EditorStyles.boldLabel);
			using (EditorGUIEx.ChangeCheck(() => UnityEditorExSettings.instance.Save()))
			{
				UnityEditorExSettings.instance.namespaceName = EditorGUILayout.TextField(UnityEditorExSettings.instance.namespaceName);
			}

			GUILayout.Label("UnityEx solution path:", EditorStyles.boldLabel);
			using (EditorGUIEx.ChangeCheck(() => bIsDirty = true))
			{
				settings.solutionPath = EditorGUILayout.TextField(settings.solutionPath);
			}
			if (bIsDirty)
			{
				CheckPaths();
			}

			if (EditorGUILayout.DropdownButton(new GUIContent(settings.buildName), FocusType.Keyboard))
			{
				GenericMenu menu = new GenericMenu();

				menu.AddItem(new GUIContent("Release"), settings.buildName == "Release", () => { settings.buildName = "Release"; bIsDirty = true; CheckPaths(); });
				menu.AddItem(new GUIContent("Debug"), settings.buildName == "Debug", () => { settings.buildName = "Debug"; bIsDirty = true; CheckPaths(); });

				menu.DropDown(EditorGUILayoutEx.GetLastRect());
			}

			using (EditorGUIEx.DisabledScopeIf(!m_CanImport))
			{
				if (GUILayout.Button("Import"))
				{
					foreach (string file in Directory.GetFiles(m_UnityEngineBuildPath))
					{
						if (file.EndsWith("dll") || file.EndsWith("pdb"))
						{
							string fileName = Path.GetFileName(file);
							string destFile = Path.Combine(m_UnityEngineAssembliesPath, fileName);
							if (File.GetLastWriteTime(file) != File.GetLastWriteTime(destFile))
							{
								File.Copy(file, destFile, true);
								EditorProgressBar.ShowProgressBar("Importing {0}...".format(fileName), 1);
							}
						}
					}

					foreach (string file in Directory.GetFiles(m_UnityEditorBuildPath))
					{
						if (file.EndsWith("dll") || file.EndsWith("pdb"))
						{
							string fileName = Path.GetFileName(file);
							string destFile = Path.Combine(m_UnityEditorAssembliesPath, fileName);
							if (File.GetLastWriteTime(file) != File.GetLastWriteTime(destFile))
							{
								File.Copy(file, destFile, true);
								EditorProgressBar.ShowProgressBar("Importing {0}...".format(fileName), 1);
							}
						}
					}

					EditorProgressBar.ClearProgressBar();
					AssetDatabase.Refresh();
				}
			}

			if (bIsDirty)
			{
				InternalEditorUtility.SaveToSerializedFileAndForget(new UnityEngine.Object[1] { settings }, m_SettingsPath, true);
			}
		}

		protected void CheckPaths()
		{
			string buildName = string.IsNullOrEmpty(settings.buildName) ? "Release" : settings.buildName;

			if (!string.IsNullOrEmpty(settings.solutionPath))
			{
				m_UnityEngineBuildPath = Path.Combine(settings.solutionPath, "bin//{0}.UnityEx//Assemblies".format(buildName));
				m_UnityEditorBuildPath = Path.Combine(settings.solutionPath, "bin//{0}.UnityEx//Editor//Assemblies".format(buildName));

				m_CanImport = Directory.Exists(m_UnityEngineBuildPath) && Directory.Exists(m_UnityEditorBuildPath);
			}
			else
			{
				m_CanImport = false;
			}
		}
	}
}
