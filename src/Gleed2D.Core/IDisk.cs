using System.Collections.Generic ;
using System.IO ;
using Gleed2D.InGame;

namespace Gleed2D.Core
{
	/// <summary>
	/// Represents an iterface to the disk.  This is here
	/// to enable unit testing of the plugins.
	/// </summary>
	public interface IDisk
	{
		/// <summary>
		/// Gets the local directory where the executing assembly is stored.
		/// </summary>
		/// <value>The directory of the executing assembly.</value>
		string DirectoryOfExecutingAssembly
		{
			get ;
		}

		/// <summary>
		/// Copies the folder.
		/// </summary>
		/// <param name="fromDirectory">From directory.</param>
		/// <param name="toDirectory">To directory.</param>
		void CopyFolder( string fromDirectory, string toDirectory ) ;

		/// <summary>
		/// Deletes the file or directory.
		/// </summary>
		/// <param name="path">The path to the file or directory.</param>
		void DeleteFileOrDirectory( string path ) ;

		/// <summary>
		/// Deletes the files or directories.
		/// </summary>
		/// <param name="paths">The paths.</param>
		void DeleteFilesOrDirectories( IEnumerable<string> paths ) ;

		/// <summary>
		/// Builds a list containing all the files under a specific directory
		/// </summary>
		/// <param name="directory">The parent directory.</param>
		/// <returns>A collection of paths representing the child directories.</returns>
		IEnumerable<string> GetChildDirectoriesRecursively( string directory ) ;

		/// <summary>
		/// Gets the type of the entity, for instance, Folder, File, etc.
		/// </summary>
		/// <param name="path">The path to the item on disk.</param>
		/// <returns>The <see cref="GLEED2D.DiskEntity"/> representing the entity at the specified path.</returns>
		DiskEntity GetEntityType( string path ) ;

		/// <summary>
		/// Builds a list containing all the files under a specific directory
		/// </summary>
		/// <param name="directory">The parent directory.</param>
		/// <returns>A collection of paths representing the child files.</returns>
		IEnumerable<string> GetFilesInDirectoryRecursively( string directory ) ;

		/// <summary>
		/// Determines whether the specified path is a file.
		/// </summary>
		/// <param name="path">The path to the disk entity.</param>
		/// <returns>
		/// <c>true</c> if the specified path is a file; otherwise, <c>false</c>.
		/// </returns>
		bool IsFile( string path ) ;

		/// <summary>
		/// Determines whether the specified path is a folder.
		/// </summary>
		/// <param name="path">The path with which to see if it's a folder.</param>
		/// <returns>
		/// <c>true</c> if the specified path is a folder; otherwise, <c>false</c>.
		/// </returns>
		bool IsFolder( string path ) ;

		/// <summary>
		/// Reads a text file from disk
		/// </summary>
		/// <param name="path">The path to the file to read.</param>
		/// <returns>The contents of the file.</returns>
		string ReadAllText( string path ) ;

		/// <summary>
		/// Toggles the Read-Only attribute on a file
		/// </summary>
		/// <param name="filePath">The path to the file.</param>
		/// <param name="isReadOnly">Whether or not to set the read only flag.</param>
		void SetFileReadOnly( string filePath, bool isReadOnly ) ;

		/// <summary>
		/// Writes a text file to disk
		/// </summary>
		/// <param name="path">The path of the file to write to.</param>
		/// <param name="fileContents">The contents to write.</param>
		void WriteTextToFile( string path, string fileContents ) ;

		bool FileExists( string localPath ) ;

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate" )]
		string GetTempPath( ) ;

		string Combine( string firstPart, string secondPart ) ;

		IEnumerable<string> FilenamesInDirectory( string pathToFolder, string searchPattern, SearchOption searchOption ) ;

		void CreateDirectoryIfDoesNotExist( string directoryPath ) ;

		string GetDirectoryNameFromPath( string path ) ;
		
		bool IsSubfolder(string potentialParent, string folder) ;

		string MakeRelativePath(string fromPath, string toPath);

		bool FolderExists(string pathToFolder);
	}
}