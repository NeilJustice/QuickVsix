using CSharpUtils;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;

public class ZipFileReader
{
   [ExcludeFromCodeCoverage]
   public virtual ReadOnlyCollection<string> ReadFileNamesContainedInZipFile(string zipFilePath)
   {
      var zipFileNames = new Collection<string>();
      using (var vsixFileStreamReader = new StreamReader(zipFilePath))
      using (ZipArchive zipArchive = new ZipArchive(vsixFileStreamReader.BaseStream, ZipArchiveMode.Read))
      {
         foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
         {
            zipFileNames.Add(zipArchiveEntry.FullName);
         }
      }
      var readonlyZipFileNames = zipFileNames.ToReadOnlyCollection();
      return readonlyZipFileNames;
   }

   [ExcludeFromCodeCoverage]
   public virtual string ReadFileTextOfFileContainedInZipFile(string zipFilePath, string zippedFileName)
   {
      using (var vsixFileStreamReader = new StreamReader(zipFilePath))
      using (ZipArchive vsixFileZipArchive = new ZipArchive(vsixFileStreamReader.BaseStream, ZipArchiveMode.Read))
      {
         foreach (ZipArchiveEntry zipArchiveEntry in vsixFileZipArchive.Entries)
         {
            if (zipArchiveEntry.FullName == zippedFileName)
            {
               var zippedFileStreamReader = new StreamReader(zipArchiveEntry.Open());
               string zippedFileText = zippedFileStreamReader.ReadToEnd();
               return zippedFileText;
            }
         }
      }
      throw new ArgumentException($"zippedFileName {zippedFileName} was not found in .zip file {zipFilePath}");
   }
}
