using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace AOC
{
	public class ElfDirectory
	{
		public string Name { get; set; }
		public ElfDirectory ParentDir { get; set; }
		public Dictionary<string, ElfDirectory> SubDir { get; set; }
		public Dictionary<string, int> Files { get; set; }

		public ElfDirectory(string name)
		{
			this.Name = name;
			SubDir = new Dictionary<string, ElfDirectory>();
			Files = new Dictionary<string, int>();
			ParentDir = this;//parent dir is its self
		}
		public ElfDirectory(string name, ElfDirectory parent)
		{
			this.Name = name;
			SubDir = new Dictionary<string, ElfDirectory>();
			Files = new Dictionary<string, int>();
			ParentDir = parent;
		}

		public int GetDirSize()
		{
			int size = 0;
			size += GetDirSize(this);
			return size;
		}

		//can add memorization if not fast
		private int GetDirSize(ElfDirectory dir)
		{
			int size = 0;

			foreach (var item in dir.Files)
				size += item.Value;
			foreach (var item in dir.SubDir)
				size += GetDirSize(item.Value);

			return size;
		}

		public ElfDirectory AddDirectory(string name)
		{
			if (name == "..")
				return ParentDir;

			if (!SubDir.ContainsKey(name))
				SubDir.Add(name, new ElfDirectory(name, this));

			return SubDir[name];
		}

		internal void AddFile(string name, int size)
		{
			Files.Add(name, size);
		}

		public bool IsEmpty()
		{
			return (Files.Count == 0 && SubDir.Count == 0);
		}

	}


	public class Day7 : BaseDay
	{
		public Day7()
		{
			Day = "7";
			Answer1 = "1490523";
			Answer2 = "12390492";
		}

		protected override string Solution1(string[] input)
		{
			ElfDirectory root = Parse(input);
			return GetDirSizeSub100000(root).ToString();
		}

		protected override string Solution2(string[] input)
		{
			ElfDirectory root = Parse(input);

			int availableSpace = 70000000;
			int updateSize = 30000000;
			int usedSpace = root.GetDirSize();

			int freeSpace = availableSpace - usedSpace;

			List<int> dirSizes = new();
			GetSizeOfAllDirs(root, dirSizes, freeSpace);

			dirSizes.Sort();

			foreach (var item in dirSizes)
			{
				if ((freeSpace + item) >= updateSize)
				{
					return item.ToString();
				}
			}
			return "Error: Can't find min dir to delete";
		}

		public int GetDirSizeSub100000(ElfDirectory dir)
		{
			int size = 0;

			int sizeT = dir.GetDirSize();
			if (sizeT <= 100000)
				size += sizeT;

			foreach (var item in dir.SubDir)
				size += GetDirSizeSub100000(item.Value);

			return size;
		}

		public void GetSizeOfAllDirs(ElfDirectory dir, List<int> sizeList, int minimunSpace)
		{
			sizeList.Add(dir.GetDirSize());

			foreach (var item in dir.SubDir)
				GetSizeOfAllDirs(item.Value, sizeList, minimunSpace);
		}

		private ElfDirectory Parse(string[] input)
		{
			ElfDirectory root = new("root");

			ElfDirectory currentDir = root;

			foreach (var item in input)
			{
				//command

				string[] com = item.Split(' ');

				//command entered
				if (com[0] == "$")
				{
					switch (com[1])
					{
						case "cd":
							currentDir = currentDir.AddDirectory(com[2]);
							break;
						case "ls":
							break;
						default:
							Console.WriteLine("Un known command:{0}", item);
							break;
					}
				}
				//subdirs
				else if (com[0] == "dir")
				{
					currentDir.AddDirectory(com[1]);
				}
				//add file
				else
				{
					currentDir.AddFile(com[1], int.Parse(com[0]));
				}
			}

			return root;
		}


	}
}
