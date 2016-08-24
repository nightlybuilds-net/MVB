using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvb.FakeContacts.Common.Response
{
    public class ImageUrls
    {
        public string epic { get; set; }
        public string bigger { get; set; }
        public string normal { get; set; }
        public string mini { get; set; }
    }

    public class UiFaceResponse
    {
        public string username { get; set; }
        public ImageUrls image_urls { get; set; }
    }
}
