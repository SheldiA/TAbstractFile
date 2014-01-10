using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAbstractFile
{
    abstract class TAbstractFile:IDisposable
    {
        protected long size;
        public long Size
        {
            get
            {
                return size;
            }
        }
        protected long currPosition;
        public long CurrPosition
        {
            get
            {
                return currPosition;
            }
        }

        public abstract long Write(byte[] writingByte);
        public abstract byte[] Read(long count);
        public virtual long ChangePosition(long position)
        {
            if (position > size)
                position = size;
            if (position < 0)
                position = 0;
            currPosition = position;
            return currPosition;
        }
        public abstract long CopyTo(TAbstractFile toFile,long fromOffset);
        public abstract void Dispose();
    }
}
