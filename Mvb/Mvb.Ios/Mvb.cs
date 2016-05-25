using System;
using System.Collections.Generic;
using System.Text;
using Mvb.Cross.Abstract;
using RemIoc;

namespace Mvb.Ios
{
    public class Mvb
    {
        public static void Init()
        {
            RIoc.Register<IUiRunner,IosUiRunner>();
        }
    }
}
