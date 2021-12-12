using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.IO.Path;
using static NecoArcAntivirus.Extensions;

namespace NecoArcAntivirus
{
    public class Mixer
    {
        private string _audio, _video;
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

            FixPathSpaces(ref _audio);
            FixPathSpaces(ref _video);
            
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
            string name = GetFileName(UniquePath(Combine(_path, "sus.mp4")));
            
            var process = new Process();
            var info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            process.StartInfo = info;
            process.Start();

            using var writer = process.StandardInput;
            if (writer.BaseStream.CanWrite)
            {
                writer.WriteLine($"{_path[0]}:");
                writer.WriteLine($"cd {_path}");
                writer.WriteLine($"ffmpeg -i {_video} -i {_audio} -c:v copy -map 0:v:0 -map 1:a:0 -shortest {name}");
                Console.ReadKey();
            }
        }
    }
}