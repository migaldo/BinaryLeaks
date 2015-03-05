using System;
using System.IO;

namespace BinaryLeaks.Core.Helpers
{
    /// <summary>
    /// Provides a File helper.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Copy streams.
        /// </summary>
        /// <param name="readStream">The the stream you need to read.</param>
        /// <param name="writeStream">The the stream you need to write.</param>
        public static void StreamCopy(Stream readStream, Stream writeStream)
        {
            int length = 256;
            byte[] buffer = new byte[length];
            int bytesRead = readStream.Read(buffer, 0, length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, length);
            }

            readStream.Close();
            writeStream.Close();
        } 
    }
}
