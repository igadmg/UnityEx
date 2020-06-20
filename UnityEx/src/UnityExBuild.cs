namespace UnityEditorEx
{
#if false
	public class UnityExBuild
    {
        public static void Build(string solutionName)
        {
            UnityBuildLogger logger = new UnityBuildLogger();

            try
            {
                Engine.GlobalEngine.BuildEnabled = true;
                Engine.GlobalEngine.RegisterLogger(logger);
                Engine.GlobalEngine.GlobalProperties.SetProperty("UNITYEDITORDATA_PATH", Path.Combine(EditorApplication.applicationPath, "Contents"));
                Engine.GlobalEngine.GlobalProperties.SetProperty("Configuration", "Release Unity");

                Project p = new Project(Engine.GlobalEngine);
                p.BuildEnabled = true;
                //p.SetProperty("")
                p.Load(solutionName);
                p.Build();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
#endif
}