using DynamiConf.Helpers;

namespace DynamiConf
{
    public class DynamiConfiguration
    {
        // Esta clase construye un objeto configuration
        public DynamiConfiguration()
        {
            MergeWith = new InterpreterSources(this);
        }

        public InterpreterSources MergeWith { get; private set; }

        public dynamic GetConfiguration()
        {
            return MergeWith.FinalConfiguration;
        }
    }
}