using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Droid
{
    public class MvbInit
    {
        public static void Init()
        {
            RIoc.Register<IUiRunner,DroidUiRunner>();
        }
    }
}