using CSharpUtils;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.IO;

[TestFixture]
public class FileSystemTests
{
   FileSystem _fileSystem;
   MethodCaller _methodCallerMock;

   [SetUp]
   public void SetUp()
   {
      _fileSystem = new FileSystem();
      _methodCallerMock = Mock.Component<MethodCaller>(_fileSystem, "_methodCaller");
   }

   // Exceptions

   [Test]
   public void ThrowIfFilePathDoesNotExist_FileExists_DoesNothing()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), true);
      string filePath = TestRandom.String();
      //
      _fileSystem.ThrowIfFilePathDoesNotExist(filePath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(File.Exists, filePath));
   }

   [Test]
   public void ThrowIfFilePathDoesNotExist_FileDoesNotExist_ThrowsFileNotFoundException()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), false);
      string filePath = TestRandom.String();
      //
      Assert2.Throws<FileNotFoundException>(() => _fileSystem.ThrowIfFilePathDoesNotExist(filePath),
         filePath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(File.Exists, filePath));
   }

   [Test]
   public void ThrowIfFilePathArgumentDoesNotExist_FileExists_DoesNothing()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), true);
      string filePath = TestRandom.String();
      string fileArgumentName = TestRandom.String();
      //
      _fileSystem.ThrowIfFilePathArgumentDoesNotExist(filePath, fileArgumentName);
      //
      Called.Once(() => _methodCallerMock.CallFunction(File.Exists, filePath));
   }

   [Test]
   public void ThrowIfFilePathArgumentDoesNotExist_FileDoesNotExist_ThrowsFileNotFoundException()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), false);
      string filePath = TestRandom.String();
      string fileArgumentName = TestRandom.String();
      //
      string expectedExceptionMessage = $"File specified by argument \"{fileArgumentName}\" does not exist: \"{filePath}\"";
      Assert2.Throws<FileNotFoundException>(() => _fileSystem.ThrowIfFilePathArgumentDoesNotExist(filePath, fileArgumentName),
         expectedExceptionMessage);
      //
      Called.Once(() => _methodCallerMock.CallFunction(File.Exists, filePath));
   }

   [Test]
   public void ThrowIfFilePathArgumentDoesNotExistIfFilePathIsNotNull_FilePathIsNull_DoesNothing()
   {
      string argumentName = TestRandom.String();
      Assert.DoesNotThrow(() => _fileSystem.ThrowIfFilePathArgumentDoesNotExistIfFilePathIsNotNull(null, argumentName));
   }

   [Test]
   public void ThrowIfFilePathArgumentDoesNotExistIfFilePathIsNotNull_FilePathIsNotNull_CallsThrowIfFilePathArgumentDoesNotExist()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string, string>), null, null));
      string filePath = TestRandom.String();
      string argumentName = TestRandom.String();
      //
      _fileSystem.ThrowIfFilePathArgumentDoesNotExistIfFilePathIsNotNull(filePath, argumentName);
      //
      Called.Once(() => _methodCallerMock.CallAction(_fileSystem.ThrowIfFilePathArgumentDoesNotExist, filePath, argumentName));
   }

   [Test]
   public void ThrowIfFolderPathArgumentDoesNotExistIfFolderPathIsNotNull_FolderPathIsNull_DoesNothing()
   {
      string argumentName = TestRandom.String();
      Assert.DoesNotThrow(() => _fileSystem.ThrowIfFolderPathArgumentDoesNotExistIfFolderPathIsNotNull(null, argumentName));
   }

   [Test]
   public void ThrowIfFolderPathArgumentDoesNotExistIfFolderPathIsNotNull_FolderPathIsNotNull_CallsThrowIfFolderPathArgumentDoesNotExist()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string, string>), null, null));
      string folderPath = TestRandom.String();
      string argumentName = TestRandom.String();
      //
      _fileSystem.ThrowIfFolderPathArgumentDoesNotExistIfFolderPathIsNotNull(folderPath, argumentName);
      //
      Called.Once(() => _methodCallerMock.CallAction(_fileSystem.ThrowIfFolderPathArgumentDoesNotExist, folderPath, argumentName));
   }

   [Test]
   public void ThrowIfFolderPathArgumentDoesNotExist_FolderExists_DoesNothing()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), true);
      string folderPath = TestRandom.String();
      string argumentName = TestRandom.String();
      //
      _fileSystem.ThrowIfFolderPathArgumentDoesNotExist(folderPath, argumentName);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Directory.Exists, folderPath));
   }

   [Test]
   public void ThrowIfFolderPathArgumentDoesNotExist_FolderDoesNotExist_ThrowsDirectoryNotFoundException()
   {
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, bool>), null), false);
      string folderPath = TestRandom.String();
      string argumentName = TestRandom.String();
      //
      Assert2.Throws<DirectoryNotFoundException>(() => _fileSystem.ThrowIfFolderPathArgumentDoesNotExist(folderPath, argumentName),
         $"Folder specified by argument \"{argumentName}\" does not exist: \"{folderPath}\"");
      //
      Called.Once(() => _methodCallerMock.CallFunction(Directory.Exists, folderPath));
   }

   // File Paths

   [Test]
   public void CombineTwoPaths_()
   {
      string twoCombinedPaths = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string, string>), null, null));

      string resolvedCombinedTwoPaths = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string>), null));

      string path1 = TestRandom.String();
      string path2 = TestRandom.String();
      //
      string path1CombinedWithPath2 = _fileSystem.CombineTwoPaths(path1, path2);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Path.Combine, path1, path2)).Then(
      Called.Once(() => _methodCallerMock.CallFunction(Path.GetFullPath, twoCombinedPaths)));
      Assert.AreEqual(resolvedCombinedTwoPaths, path1CombinedWithPath2);
   }

   [Test]
   public void GetFullPath_ReturnsFullFileOrFolderPath()
   {
      string fullFileOrFolderPath = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string>), null));
      string relativeFileOrFolderPath = TestRandom.String();
      //
      string returnedFullFileOrFolderPath = _fileSystem.GetFullPath(relativeFileOrFolderPath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Path.GetFullPath, relativeFileOrFolderPath));
      Assert.AreEqual(fullFileOrFolderPath, returnedFullFileOrFolderPath);
   }

   [Test]
   public void GetFullPathIfRelativePathIsNotNull_RelativeFileOrFolderPathIsNull_ReturnsNull()
   {
      string fullFileOrFolderPath = _fileSystem.GetFullPathIfRelativePathIsNotNull(null);
      Assert.IsNull(fullFileOrFolderPath);
   }

   [Test]
   public void GetFullPathIfRelativePathIsNotNull_RelativeFileOrFolderPathIsNull_ReturnsFullFileOrFolderPath()
   {
      string fullFileOrFolderPath = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string>), null));
      string relativeFileOrFolderPath = TestRandom.String();
      //
      string returnedFullFileOrFolderPath = _fileSystem.GetFullPathIfRelativePathIsNotNull(relativeFileOrFolderPath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Path.GetFullPath, relativeFileOrFolderPath));
      Assert.AreEqual(fullFileOrFolderPath, returnedFullFileOrFolderPath);
   }

   // Folder Paths

   [Test]
   public void GetParentFolderPath_ReturnsResultOfCallingGetDirectoryNameOnFilePath()
   {
      string containingFolderPath = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string>), null));
      string filePath = TestRandom.String();
      //
      string returnedContainingFolderPath = _fileSystem.GetContainingFolderPath(filePath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Path.GetDirectoryName, filePath));
      Assert.AreEqual(containingFolderPath, returnedContainingFolderPath);
   }

   [Test]
   public void GetCurrentDirectory_ReturnsCurrentDirectoryPath()
   {
      string currentDirectoryPath = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string>)));
      //
      string returnedCurrentDirectoryPath = _fileSystem.GetCurrentDirectory();
      //
      Called.Once(() => _methodCallerMock.CallFunction(Directory.GetCurrentDirectory));
   }

   [Test]
   public void GetFolderPaths_ReturnsTopLevelFolderPathsInFolderPath()
   {
      string[] folderPathsArray = new string[] { TestRandom.String(), TestRandom.String(), TestRandom.String() };
      Mock.Return(() => _methodCallerMock.CallFunction(default(Func<string, string[]>), null), folderPathsArray);
      string folderPath = TestRandom.String();
      //
      ReadOnlyCollection<string> folderPaths = _fileSystem.GetFolderPaths(folderPath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Directory.GetDirectories, folderPath));
      ReadOnlyCollection<string> expectedFolderPaths = folderPathsArray.ToReadOnlyCollection();
      Assert.AreEqual(expectedFolderPaths, folderPaths);
   }

   [Test]
   public void GetFolderName_ReturnsResultOfCallingGetFileNameOnFolderPath()
   {
      string folderName = Mock.ReturnRandomString(() => _methodCallerMock.CallFunction(default(Func<string, string>), null));
      string folderPath = TestRandom.String();
      //
      string returnedFolderName = _fileSystem.GetFolderName(folderPath);
      //
      Called.Once(() => _methodCallerMock.CallFunction(Path.GetFileName, folderPath));
      Assert.AreEqual(folderName, returnedFolderName);
   }

   [Test]
   public void SetCurrentDirectory_CallsSetCurrentDirectory()
   {
      Mock.Expect(() => _methodCallerMock.CallAction(default(Action<string>), null));
      string folderPath = TestRandom.String();
      //
      _fileSystem.SetCurrentDirectory(folderPath);
      //
      Called.Once(() => _methodCallerMock.CallAction(Directory.SetCurrentDirectory, folderPath));
   }
}
