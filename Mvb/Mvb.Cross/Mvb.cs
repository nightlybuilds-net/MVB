using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Cross
{
    public class Mvb
    {
        /// <summary>
        /// Register Null Ui Runner
        /// Just invoke actions
        /// </summary>
        public static void NullInit()
        {
            RIoc.Register<IUiRunner,NullUiRunner>();
        }
    }
}
