using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

using ESystems.Mentoring.FileSystem.Interface;
using ESystems.Mentoring.FileSystem.Interface.Data;
using ESystems.Mentoring.FileSystem.Interface.Exceptions;

namespace ESystems.Mentoring.FileSystem
{
    public sealed class FileSystemService : IFileSystemService
    {
        public IEnumerable<FileData> GetFiles(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(path);
            }

            try
            {
                return (new DirectoryInfo(path).GetFiles() ?? Array.Empty<FileInfo>())
                    .Select(file => new FileData(name: file.Name, fullName: file.FullName));
            }
            catch (DirectoryNotFoundException exception)
            {
                throw new WrongPathFileSystemServiceException(path, exception);
            }
            catch (SecurityException exception)
            {
                throw new AccessDeniedFileSystemServiceException(path, exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new AccessDeniedFileSystemServiceException(path, exception);
            }
        }

        public IEnumerable<FolderData> GetFolders(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(path);
            }

            try
            {
                return (new DirectoryInfo(path).GetDirectories() ?? Array.Empty<DirectoryInfo>())
                    .Select(folder => new FolderData(name: folder.Name, fullName: folder.FullName));
            }
            catch (DirectoryNotFoundException exception)
            {
                throw new WrongPathFileSystemServiceException(path, exception);
            }
            catch (SecurityException exception)
            {
                throw new AccessDeniedFileSystemServiceException(path, exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new AccessDeniedFileSystemServiceException(path, exception);
            }
        }
    }
}
