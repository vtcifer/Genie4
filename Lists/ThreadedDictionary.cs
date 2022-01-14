using System;
using System.Collections;
using System.Threading;

namespace GenieClient.Genie.Collections
{
    public class ThreadedDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
    {
        private ReaderWriterLockSlim m_oRWLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private const int m_iDefaultTimeout = 250;

        public bool AcquireWriterLock()
        {
            try
            {
                if (m_oRWLock.IsWriteLockHeld | m_oRWLock.IsReadLockHeld)
                {
                    return false;
                }
                m_oRWLock.EnterWriteLock();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool AcquireReaderLock()
        {
            try
            {
                m_oRWLock.EnterReadLock();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }
        public bool ReleaseWriterLock()
        {
            try
            {
                m_oRWLock.ExitWriteLock();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool ReleaseReaderLock()
        {
            try
            {
                m_oRWLock.ExitReadLock();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public ThreadedDictionary() : base()
        {
        }

        public new void Clear()
        {
            if (AcquireWriterLock())
            {
                try
                {
                    base.Clear();
                }
                finally
                {
                    ReleaseWriterLock();
                }
            }
            else
            {
                throw new Exception("Unable to aquire writer lock.");
            }
        }

        public new TValue this[TKey key] 
        { 
            get 
            {
                if (AcquireReaderLock())
                {
                    try
                    {
                        return base[key];
                    }
                    finally
                    {
                        ReleaseReaderLock();
                    }
                }
                else
                {
                    throw new Exception("Unable to aquire reader lock.");
                }
            } 
        }
        public new void Add(TKey key, TValue value)
        {
            if (AcquireWriterLock())
            {
                try
                {
                    if (base.ContainsKey(key))
                    {
                        base[key] = value;
                    }
                    else
                    {
                        base.Add(key, value);
                    }
                }
                finally
                {
                    ReleaseWriterLock();
                }
            }
            else
            {
                throw new Exception("Unable to aquire writer lock.");
            }
        }

        public new void Remove(TKey key)
        {
            if (AcquireWriterLock())
            {
                try
                {
                    base.Remove(key);
                }
                finally
                {
                    ReleaseWriterLock();
                }
            }
            else
            {
                throw new Exception("Unable to aquire writer lock.");
            }
        }
    }
}