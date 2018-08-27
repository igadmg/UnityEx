#if false
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using UnityEngine;
#endif


namespace UnityEditorEx
{
#if false
	// This logger will derive from the Microsoft.Build.Utilities.Logger class,
	// which provides it with getters and setters for Verbosity and Parameters,
	// and a default empty Shutdown() implementation.
	public class UnityBuildLogger : Microsoft.Build.Utilities.Logger
	{
		/// <summary>
		/// Initialize is guaranteed to be called by MSBuild at the start of the build
		/// before any events are raised.
		/// </summary>
		public override void Initialize(IEventSource eventSource)
		{
			eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
			eventSource.TaskStarted += new TaskStartedEventHandler(eventSource_TaskStarted);
			eventSource.MessageRaised += new BuildMessageEventHandler(eventSource_MessageRaised);
			eventSource.WarningRaised += new BuildWarningEventHandler(eventSource_WarningRaised);
			eventSource.ErrorRaised += new BuildErrorEventHandler(eventSource_ErrorRaised);
			eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
		}

		void eventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
		{
			Debug.LogErrorFormat("{0}: ERROR {1}({2},{3}): {4}", e.SenderName, e.File, e.LineNumber, e.ColumnNumber, e.Message);
		}
		
		void eventSource_WarningRaised(object sender, BuildWarningEventArgs e)
		{
			Debug.LogWarningFormat("{0}: WARNING {1}({2},{3}): {4}", e.SenderName, e.File, e.LineNumber, e.ColumnNumber, e.Message);
		}

		void eventSource_MessageRaised(object sender, BuildMessageEventArgs e)
		{
			// BuildMessageEventArgs adds Importance to BuildEventArgs
			// Let's take account of the verbosity setting we've been passed in deciding whether to log the message
			if ((e.Importance == MessageImportance.High && IsVerbosityAtLeast(LoggerVerbosity.Minimal))
				|| (e.Importance == MessageImportance.Normal && IsVerbosityAtLeast(LoggerVerbosity.Normal))
				|| (e.Importance == MessageImportance.Low && IsVerbosityAtLeast(LoggerVerbosity.Detailed))				
				)
			{
				Debug.LogFormat("{0}: {1}", e.SenderName, e.Message);
			}
		}

		void eventSource_TaskStarted(object sender, TaskStartedEventArgs e)
		{
			// TaskStartedEventArgs adds ProjectFile, TaskFile, TaskName
			// To keep this log clean, this logger will ignore these events.
		}
		
		void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
		{
			Debug.LogFormat("{0}: {1}: {2}", e.SenderName, e.Message, e.ProjectFile);
		}

		void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
		{
			Debug.LogFormat("{0}: {1}: {2}", e.SenderName, e.Message, e.ProjectFile);
		}
	}
#endif
}