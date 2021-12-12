namespace NecoArcAntivirus
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0) return;
            
            var path = @"D:\Desktop";
            var mixer = new Mixer(args, path);
            mixer.StartProcess();
        }
    }
}