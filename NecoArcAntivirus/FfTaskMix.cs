using System.Collections.Generic;
using System.Threading.Tasks;
using MediaToolkit.Core;
using MediaToolkit.Tasks;

namespace NecoArcAntivirus
{
    public class FfTaskMix : FfMpegTaskBase<int>
    {
        private readonly string _video, _audio, _output;
        
        public FfTaskMix(string video, string audio, string name)
        {
            _video = video;
            _audio = audio;
            _output = name;
        }

        public override IList<string> CreateArguments() => new[]
        {
            "-i", _video,
            "-i", _audio,
            "-c:v", "copy",
            "-map", "0:v:0",
            "-map", "1:a:0",
            "-shortest", _output
        };

        public override async Task<int> ExecuteCommandAsync(IFfProcess ffProcess)
        {
            await ffProcess.Task;
            return 0;
        }
    }
}