using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_2022
{
    internal class Day7
    {
        private class DFile
        {
            private string _name;
            private int _size;

            public string Name => _name;
            public int Size => _size;

            public DFile(string name, int size)
            {
                _name = name;
                _size = size;
            }
        }
        
        private class Directory
        {
            private string _name;

            private static Directory _root;

            private Directory _parentDirectory;
            private List<Directory> _directories;
            private List<DFile> _files;

            /// <summary>
            /// This constructor should be used for the root Directory
            /// </summary>
            public Directory(string name)
            {
                _root = this;
                _parentDirectory = this;    //make it its own parent
                
                _name = name;
                _directories = new List<Directory>();
                _files = new List<DFile>();
            }
            public Directory(string name, Directory parentDirectory)
            {
                if (NameExistsInParent(name, parentDirectory))
                    throw new Exception($"You can create another file with name {name} in {parentDirectory}");

                _parentDirectory = parentDirectory;

                _name = name;
                _directories = new List<Directory>();
                _files = new List<DFile>();
            }

            private bool NameExistsInParent(string name, Directory parentDirectory)
            {
                for (int i = 0; i < parentDirectory._directories.Count; i++) {
                    if (parentDirectory._directories[i]._name == name)
                        return true;
                }

                return false;
            }

            public void AddDFile(DFile file)
            {
                _files.Add(file);
            }

            public void AddDirectory(Directory directory)
            {
                _directories.Add(directory);
            }

            public Directory GetRoot()
            {
                return _root;
            }

            public Directory GetParent()
            {
                return _parentDirectory;
            }

            public Directory GetSon(string sonName)
            {
                for (int i = 0; i < _directories.Count; i++)
                {
                    if (_directories[i]._name == sonName)
                        return _directories[i];
                }

                throw new Exception($"Directory {sonName} not found in {_name}");
            }

            public Directory[] GetAllSons()
            {
                return _directories.ToArray();
            }

            public int GetSize()
            {
                int size = 0;

                //sum size of all directories
                for (int i = 0; i < _directories.Count; i++) {
                    size += _directories[i].GetSize();
                }

                //sum size of all files
                for (int i = 0; i < _files.Count; i++) {
                    size += _files[i].Size;
                }

                return size;
            }
        }

        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer7A.txt");

            //set limitSize and rootDirectoryName
            int limitSize = 100000;
            string rootDirectoryName = "/";

            //create all the directories and files
            //start in root
            Directory actualDirectory = new Directory(rootDirectoryName);

            CreateAllDirectoriesAndFiles(input, actualDirectory, rootDirectoryName);

            //obtain size of all directories with a size at most 100000
            List<Directory> smallDirectories = new List<Directory>();
            List<int> smallSize = new List<int>();

            actualDirectory = actualDirectory.GetRoot();

            //while there are directories without been visited using BFS
            Queue<Directory> directoriesToSize = new Queue<Directory>();
            directoriesToSize.Enqueue(actualDirectory);

            while (directoriesToSize.Count > 0) {
                Directory currentDirectory = directoriesToSize.Dequeue();

                Directory[] sons = currentDirectory.GetAllSons();
                for (int i = 0; i < sons.Length; i++) {
                    directoriesToSize.Enqueue(sons[i]);
                }

                int currentSize = currentDirectory.GetSize();
                if (currentSize <= limitSize) { 
                    smallDirectories.Add(actualDirectory);
                    smallSize.Add(currentSize);
                }
            }

            //sum size of those directories
            int totalSize = 0;
            for (int i = 0; i < smallSize.Count; i++) {
                totalSize += smallSize[i];
            }

            return totalSize;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer7A.txt");

            //set spaces and rootDirectoryName
            int totalDiskSpace = 70000000;
            int spaceNeeded = 30000000;
            string rootDirectoryName = "/";

            //create all the directories and files
            //start in root
            Directory actualDirectory = new Directory(rootDirectoryName);
            CreateAllDirectoriesAndFiles(input, actualDirectory, rootDirectoryName);

            //obtain size smallest Directory to delete
            Directory smallDirectoryToDelete = actualDirectory.GetRoot();
            int smallSize = smallDirectoryToDelete.GetSize();

            int freeSpaceInDisk = totalDiskSpace - smallSize;
            int spaceToDelete = spaceNeeded - freeSpaceInDisk;

            //while there are directories without been visited using BFS
            actualDirectory = actualDirectory.GetRoot();
            Queue<Directory> directoriesToSize = new Queue<Directory>();
            directoriesToSize.Enqueue(actualDirectory);

            while (directoriesToSize.Count > 0) {
                Directory currentDirectory = directoriesToSize.Dequeue();

                Directory[] sons = currentDirectory.GetAllSons();
                for (int i = 0; i < sons.Length; i++) {
                    directoriesToSize.Enqueue(sons[i]);
                }

                //get directory that is possible to delete
                int currentSize = currentDirectory.GetSize();
                if (currentSize >= spaceToDelete) {
                    
                    if (currentSize < smallSize) { 
                        smallDirectoryToDelete = currentDirectory;
                        smallSize = currentSize;
                    }
                }
            }

            return smallSize;
        }

        private static void CreateAllDirectoriesAndFiles(string[] input, Directory rootDirectory, string rootDirectoryName)
        {
            Directory actualDirectory = rootDirectory;

            for (int i = 0; i < input.Length; i++) {
                string[] line = input[i].Split();

                //check if is a command
                if (line[0].Equals("$")) {
                    if (line[1].Equals("ls"))
                        continue;   //we do not want to do anything in special in ls
                    
                    //is "cd"
                    string nameToDirectory = line[2];

                    //asume that every directory is created when we are here
                    if (nameToDirectory.Equals(rootDirectoryName)) {
                        actualDirectory = actualDirectory.GetRoot();
                    }
                    else if (nameToDirectory.Equals("..")) {
                        actualDirectory = actualDirectory.GetParent();
                    }
                    //the last possibility is the name of a son directory
                    else {
                        actualDirectory = actualDirectory.GetSon(nameToDirectory);
                    }
                }
                //is directory
                else if (line[0].Equals("dir")) {
                    Directory sonDirectory = new Directory(line[1], actualDirectory);
                    actualDirectory.AddDirectory(sonDirectory);
                }
                //is a file
                else {
                    DFile fileInDirectory = new DFile(line[1], Convert.ToInt32(line[0]));
                    actualDirectory.AddDFile(fileInDirectory);
                }
            }
        }
    }
}