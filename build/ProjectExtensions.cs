using System.Linq;
using Nuke.Common.ProjectModel;

public static class ProjectExtensions
{
    public static string GetTargetFramework(this Project project)
        => project.GetTargetFrameworks()!.First();
}