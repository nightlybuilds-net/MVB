namespace Mvb.FakeContacts.ModelBinders.Errors
{
    public class ModelBindersErrorArgs
    {
        public ModelBindersErrorArgs(string message, int errorcode = -1)
        {
            this.Message = message;
            this.ErrorCode = errorcode;
        }

        public int ErrorCode { get; }

        public string Message { get; }
    }
}