﻿using System.IO;

namespace CLAP
{
    /// <summary>
    /// A helper for file reading to allow mocking for tests
    /// </summary>
    public static class FileSystemHelper
    {
        public static IFileSystem FileHandler { get; set; }

        static FileSystemHelper()
        {
            FileHandler = new FileSystem();
        }

        internal static string ReadAllText(string path)
        {
            return FileHandler.ReadAllText(path);
        }

        public interface IFileSystem
        {
            string ReadAllText(string path);
        }

        [CoverageExclude]
        private class FileSystem : IFileSystem
        {
            public string ReadAllText(string path)
            {
                return File.ReadAllText(path);
            }
        }
    }
}