using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Droid
{
    public class Mvb
    {
        public static void Init()
        {
            RIoc.Register<IUiRunner,DroidUiRunner>();
        }
    }
}