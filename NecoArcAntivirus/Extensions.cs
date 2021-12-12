using System.IO;

namespace NecoArcAntivirus
{
    public static class Extensions
    {
        public static string UniquePath(string path)
        {
            while (File.Exists(path))
            {
                var dot = '.';
                string[] shit = path.Split(dot); // [...][..name][mp4]
                char c = shit[^2][^1];
                shit[^2] += c;
                path = string.Join(dot, shit);
            }
            return path;
        }
    }
}