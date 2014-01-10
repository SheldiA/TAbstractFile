using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TAbstractFile
{
    class TOSFile:TAbstractFile
    {
        private FileStream fs;
        public TOSFile(string fileName)
        {
            fs = new FileStream(fileName,FileMode.OpenOrCreate,FileAccess.ReadWrite);
            if (fs != null)
            {
                size = fs.Length;
                currPosition = 0;
                fs.Seek(0,SeekOrigin.Begin);
            }
        }

        public override byte[] Read(long count)
        {
            long realCount = (count + currPosition <= size) ? count : (size - currPosition);
            byte[] result = new byte[realCount];
            fs.Seek(currPosition, SeekOrigin.Begin);
            for (int i = 0; i < realCount; ++i)
            {
                result[i] = (byte)fs.ReadByte();
                ++currPosition;
            }
    
            return result;
        }

        public override long Write(byte[] writingByte)
        {
            long result = 0;
            while (currPosition < size && result < writingByte.Length)
            {
                fs.WriteByte(writingByte[result]);
                ++result;
                ++currPosition;
            }

            return result;
        }

        public override long ChangePosition(long position)
        {
            base.ChangePosition(position);
            fs.Seek(currPosition,SeekOrigin.Begin);
            return currPosition;
        }

        public override long CopyTo(TAbstractFile toFile, long fromOffset)
        {
            long result = 0;

            if (fromOffset < toFile.Size)
            {
                ChangePosition(0);
                byte[] buffer = Read(size);
                result = toFile.Write(buffer);
            }

            return result;
        }

        public override void Dispose()
        {
            if(fs != null)
                fs.Close();
            GC.SuppressFinalize(this);
        }

        ~TOSFile()
        {
            if (fs != null)
                fs.Close();
        }
    }
}
