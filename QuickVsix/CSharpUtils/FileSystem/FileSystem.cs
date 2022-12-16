using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace CSharpUtils
{
   public class FileSystem
   {
      private readonly MethodCaller _methodCaller = new MethodCaller();

      // Exceptions

      public virtual void ThrowIfFilePathDoesNotExist(string filePath)
      {
         bool fileExists = _methodCaller.CallFunction(File.Exists, filePath);
         if (!fileExists)
         {
            throw new FileNotFoundException(filePath);
         }
      }

      public virtual void ThrowIfFilePathArgumentDoesNotExist(string filePath, string fileArgumentName)
      {
         bool fileExists = _methodCaller.CallFunction(File.Exists, filePath);
         if (!fileExists)
         {
            throw new FileNotFoundException($"File specified by argument \"{fileArgumentName}\" does not exist: \"{filePath}\"");
         }
      }

      public virtual void ThrowIfFolderPathArgumentDoesNotExist(string folderPath, string argumentName)
      {
         bool folderExists = _methodCaller.CallFunction(Directory.Exists, folderPath);
         if (!folderExists)
         {
            throw new DirectoryNotFoundException($"Folder specified by argument \"{argumentName}\" does not exist: \"{folderPath}\"");
         }
      }

      public virtual void ThrowIfFilePathArgumentDoesNotExistIfFilePathIsNotNull(string filePath, string argumentName)
      {
         if (filePath != null)
         {
            _methodCaller.CallAction(ThrowIfFilePathArgumentDoesNotExist, filePath, argumentName);
         }
      }

      public virtual void ThrowIfFolderPathArgumentDoesNotExistIfFolderPathIsNotNull(string folderPath, string argumentName)
      {
         if (folderPath != null)
         {
            _methodCaller.CallAction(ThrowIfFolderPathArgumentDoesNotExist, folderPath, argumentName);
         }
      }

      // File Paths

      public virtual string CombineTwoPaths(string path1, string path2)
      {
         string twoCombinedPaths = _methodCaller.CallFunction(Path.Combine, path1, path2);
         string resolvedCombinedTwoPaths = _methodCaller.CallFunction(Path.GetFullPath, twoCombinedPaths);
         return resolvedCombinedTwoPaths;
      }

      public virtual string GetFullPath(string relativeFileOrFolderPath)
      {
         string fullFileOrFolderPath = _methodCaller.CallFunction(Path.GetFullPath, relativeFileOrFolderPath);
         return fullFileOrFolderPath;
      }

      public virtual string GetFullPathIfRelativePathIsNotNull(string relativeFileOrFolderPath)
      {
         if (relativeFileOrFolderPath != null)
         {
            string fullFileOrFolderPath = _methodCaller.CallFunction(Path.GetFullPath, relativeFileOrFolderPath);
            return fullFileOrFolderPath;
         }
         return null;
      }

      // Folder Paths

      public virtual string GetContainingFolderPath(string filePath)
      {
         string containingFolderPath = _methodCaller.CallFunction(Path.GetDirectoryName, filePath);
         return containingFolderPath;
      }

      public virtual string GetCurrentDirectory()
      {
         string currentDirectoryPath = _methodCaller.CallFunction(Directory.GetCurrentDirectory);
         return currentDirectoryPath;
      }

      public virtual ReadOnlyCollection<string> GetFolderPaths(string folderPath)
      {
         string[] folderPathsArray = _methodCaller.CallFunction(Directory.GetDirectories, folderPath);
         ReadOnlyCollection<string> folderPaths = folderPathsArray.ToReadOnlyCollection();
         return folderPaths;
      }

      public virtual string GetFolderName(string folderPath)
      {
         string folderName = _methodCaller.CallFunction(Path.GetFileName, folderPath);
         return folderName;
      }

      public virtual void SetCurrentDirectory(string folderPath)
      {
         _methodCaller.CallAction(Directory.SetCurrentDirectory, folderPath);
      }
   }
}
