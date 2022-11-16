using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TheProgram
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь:");
            string Path = Console.ReadLine();
            while (string.IsNullOrEmpty(Path)||!Directory.Exists(Path)) 
            {
                Console.WriteLine("Не валидный путь.");
                Console.WriteLine("Введите путь:");
                Path = Console.ReadLine();
            }
            Console.WriteLine("Создать папки:0");
            Console.WriteLine("Создать файлы:1");
            int Target;
            bool parsed;
            do
            {
                parsed=int.TryParse(Console.ReadLine(), out Target);
                if(parsed&&(Target < 0|| Target > 1))
                {
                    Console.WriteLine("Вне диапазона.");
                }
                else 
                if(!parsed){
                    Console.WriteLine("Не распознано.");
                }
            } while (!parsed);
            Console.WriteLine("Сколько создать?");

            int Count;
            do
            {
                parsed = int.TryParse(Console.ReadLine(), out Count);
                if (parsed && (Count < 0))
                {
                    Console.WriteLine("Вне диапазона.");
                }
                else
                if (!parsed)
                {
                    Console.WriteLine("Не распознано.");
                }
            } while (!parsed);

            const int PartSize = 10000;
            List<Task> tasks = new List<Task>();
            if (Target == 0)
            {
                for (int i = 0; i < Count; i += PartSize)
                {
                    int v = i;
                    tasks.Add(Task.Run(() => { FolderCreate(Path, v,Math.Min(Count,i+PartSize)); }));
                }
            }
            else 
            {
                
                for (int i = 0; i < Count; i += PartSize)
                {
                    int v = i;
                    tasks.Add(Task.Run(() => { FileCreate(Path, v, Math.Min(Count, i + PartSize)); }));
                }
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Готово");
        }
        static void FileCreate(string Path,int ID,int MaxID) 
        {
            for (int i = ID; i < MaxID; i++)
            {
                File.WriteAllText(Path + i, i.ToString());
            }
        }
        static void FolderCreate(string Path, int ID, int MaxID)
        {
            for (int i = ID; i < MaxID; i++)
            {
                Directory.CreateDirectory(Path + i);
            }
        }
    }
}

