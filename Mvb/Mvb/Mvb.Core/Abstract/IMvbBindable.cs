namespace Mvb.Core.Abstract
{
    public interface IMvbBindable : IMvbNotifyPropertyChanged
    {
        /// <summary>
        /// Remove all handler from this object
        /// </summary>
        void ClearHandler();
    }
}