namespace Mvb.FakeContacts.Concrete.Pcl.Response
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
