namespace SE7.Threading
{
    internal class MultiThreadPoolSynchronizationContext : SynchronizationContext
    {
        public override SynchronizationContext CreateCopy()
        {
            return base.CreateCopy();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void OperationCompleted()
        {
            base.OperationCompleted();
        }

        public override void OperationStarted()
        {
            base.OperationStarted();
        }

        public override void Post(SendOrPostCallback d, object? state)
        {
            base.Post(d, state);
        }

        public override void Send(SendOrPostCallback d, object? state)
        {
            base.Send(d, state);
        }

        public override string ToString()
        {
            return base.ToString() ?? string.Empty;
        }

        public override int Wait(nint[] waitHandles, bool waitAll, int millisecondsTimeout)
        {
            return base.Wait(waitHandles, waitAll, millisecondsTimeout);
        }
    }
}
