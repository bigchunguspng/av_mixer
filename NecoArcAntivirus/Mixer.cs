using System.IO;
using MediaToolkit.Services;
using static System.IO.Path;
using static NecoArcAntivirus.Extensions;

namespace NecoArcAntivirus
{
    public class Mixer
    {
        private string _video;
        private readonly string _audio;
        private readonly string _path;
        
        public Mixer(string[] args, string path)
        {
            _audio = string.Concat(args);
            _path = path;
        }

        public void StartProcess()
        {
            FindVideo();
            if (_video == null) return;

            Mix();
        }

        private void FindVideo()
        {
            string[] files = Directory.GetFiles(_path);
            foreach (string file in files)
            {
                if (file.EndsWith(".mp4"))
                {
                    _video = file;
                    break;
                }
            }
        }
        
        private void Mix()
        {
            string name = UniquePath(Combine(_path, "sus.mp4"));

            var ffmpeg = "C:\\ffmpeg\\bin\\ffmpeg.exe";
            var service = MediaToolkitService.CreateInstance(ffmpeg);
            var task = new FfTaskMix(_video, _audio, name);
            service.ExecuteAsync(task).Wait();
        }
    }
}