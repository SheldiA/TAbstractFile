using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;

namespace TAbstractFile
{
    class TMemoryFile:TAbstractFile
    {
        MemoryMappedFile mmf;
        public TMemoryFile(string fileName,long maxCapacity)
        {
            //mmf = MemoryMappedFile.CreateOrOpen(fileName, maxCapacity);
            mmf = MemoryMappedFile.CreateFromFile(fileName);
            size = 19;
        }

        public override byte[] Read(long count)
        {
            long realCount = (count + currPosition <= size) ? count : (size - currPosition);
            byte[] result = new byte[realCount];

            MemoryMappedViewStream mmvs = mmf.CreateViewStream(currPosition,realCount);
            for (int i = 0; i < realCount; ++i)
            {
                result[i] = (byte)mmvs.ReadByte();
                ++currPosition;
            }
            mmvs.Close();
            return result;
        }

        public override long ChangePosition(long position)
        {
            return base.ChangePosition(position);
        }

        public override long CopyTo(TAbstractFile toFile, long fromOffset)
        {
            throw new NotImplementedException();
        }

        public override long Write(byte[] writingByte)
        {
            long toCount = (currPosition + writingByte.Length <= size) ? writingByte.Length + currPosition : 0;
            MemoryMappedViewStream mmvs = mmf.CreateViewStream(currPosition,toCount);
            for (int i = 0; i < mmvs.Length; ++i)
            {
                mmvs.WriteByte(writingByte[i]);
                ++currPosition;
            }
            long count = mmvs.Length;
            mmvs.Close();
            return count;
        }

        public override void Dispose()
        {
            if (mmf != null)
                mmf.Dispose();
            GC.SuppressFinalize(this);
        }

        ~TMemoryFile()
        {
            if (mmf != null)
                mmf.Dispose();
        }

    }
}
