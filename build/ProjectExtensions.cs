using System.Linq;
using Nuke.Common.ProjectModel;

namespace _build;

public static class ProjectExtensions
{
    public static string GetTargetFramework(this Project project)
        => project.GetTargetFrameworks()!.First();
}