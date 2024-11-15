namespace SE7.Threading.Events
{
    internal class ExceptionThrownInThreadPoolEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public ExceptionThrownInThreadPoolEventArgs(Exception exception) => Exception = exception;
    }
}
