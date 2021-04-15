using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace UtromsTehgik
{
    class Program
    {
        static void Main(string[] args)
        {
            string way = @"src/Utrom's secrets";
            
            DirectoryInfo[] parr = Readerd();
            
            
            //Reader1(out farr,out parr);
            int[] dirhelp = new int[parr.Length];

            ////зачем я сортирую, если в папке всё само?
            //SortedDictionary<DirectoryInfo, string> sortdi = new SortedDictionary<DirectoryInfo, string>(new DComp());
            //for(int i=0;i<parr.Length;i++)
            //{
            //    sortdi.Add(parr[i], Nam(parr[i]));
            //}
            //Dictionary<DirectoryInfo, string> sortdi = new Dictionary<DirectoryInfo, string>();
            //for (int i = 0; i < parr.Length; i++)
            //{
            //    sortdi.Add(parr[i], Nam(parr[i]));
            //}

            for (int i = 0; i < (parr.Length-1); i++)
            {
                if(Nam(parr[i])== Nam(parr[i + 1])) //(parr[i]== parr[i+1])
                {
                    dirhelp[i + 1] = 2;
                }
            }
            for (int i = 0; i < (parr.Length); i++)
            {
                if (dirhelp[i] == 2)
                {
                    Directory.Delete(parr[i].FullName, true);
                }
                else
                {
                    DelID(ref parr[i]);
                }
            }

            Readerf(way, out FileInfo[] farr);

            int[] filhelp = new int[farr.Length];
            Sort(ref farr);//,ref filhelp);

            for (int i = 0; i < (farr.Length - 1); i++)
            {                
                if (Nam(farr[i]) == Nam(farr[i + 1])) //имена сразу с расширениями, farr[i].Extension не нужно
                {
                    filhelp[i + 1] = 2;
                }
            }
            for (int i = 0; i < (farr.Length); i++)
            {
                if (filhelp[i] == 2)
                {
                    File.Delete(farr[i].FullName);
                }
                else
                {
                    DelID(ref farr[i]);
                }
            }

            //DelID(ref farr[3]);

            //foreach(FileInfo el in farr)
            //{
            //    Console.WriteLine(el.Name+' '+ el.Length.ToString()+" байт"+' '+",атрибут: "+el.Attributes.ToString());
            //}

            //DirectoryInfo dir = new DirectoryInfo(@"src/Utrom's secrets");



            string zipFile = @"src/Utrom's secrets.zip"; // сжатый файл
            File.Delete(zipFile);
            ZipFile.CreateFromDirectory(way, zipFile);

            Directory.Delete(way, true);

            Console.WriteLine("The happy end!!!");
            Console.ReadKey();
        }

        
        static public void Reader1(out FileInfo[] farr, out DirectoryInfo[] parr)
        {
            //метод, который считывает все файлы и папки, запишет файлы в массив файлов, папки в массив папок
            DirectoryInfo dir = new DirectoryInfo(@"src/Utrom's secrets");
            farr = dir.GetFiles("*",SearchOption.AllDirectories);
            parr = dir.GetDirectories();
        }
        static public DirectoryInfo[] Readerd()
        {
            //метод, который считывает папки
            DirectoryInfo dir = new DirectoryInfo(@"src/Utrom's secrets");
            return dir.GetDirectories();
        }
        static public void Readerf(string way,out FileInfo[] farr)
        {
            //метод, который считывает все файлы, запишет файлы в массив файлов
            DirectoryInfo dir = new DirectoryInfo(way);
            farr = dir.GetFiles("*", SearchOption.AllDirectories);
        }

        static public void DelID(ref FileInfo f)
        {
            //метод удаляет идентификаторы

            string id = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";

            string name2 = Regex.Replace(f.Name, id, replacement);

            if (name2 != f.Name)
            {
                string path2 = Regex.Replace(f.FullName, f.Name, name2);

                File.Move(f.FullName, path2);
            }
        }
        static public void DelID(ref DirectoryInfo f)
        {
            //метод удаляет идентификаторы

            string id = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";

            string name2 = Regex.Replace(f.Name, id, replacement);

            if (name2 != f.Name)
            {
                string path2 = Regex.Replace(f.FullName, f.Name, name2);

                Directory.Move(f.FullName, path2);
            }
        }
        static public string Nam(DirectoryInfo f)
        {
            //метод удаляет идентификаторы

            string id = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";

            return Regex.Replace(f.Name, id, replacement);
        }
        static public string Nam(FileInfo f)
        {
            //метод удаляет идентификаторы

            string id = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";

            return Regex.Replace(f.Name, id, replacement);
        }

        public static void Sort(ref FileInfo[] farr)//, ref int[] filhelp)
        {
            //метод сортирует файлы по их именам

            Array.Sort(farr, new FCopmp());

        }

        //static public void UdalitePapku(DirectoryInfo di)
        //{
        //    FileInfo[] farr;

        //    Readerf(@"src/Utrom's secrets"+@"/"+di.Name, out farr);

        //    for(int i=0;i<farr.Length;i++)
        //    {
        //        File.Delete(farr[i].FullName);
        //    }

        //    Directory.Delete(, true);
        //}

        // This method accepts two strings the represent two files to
        // compare. A return value of тру indicates that the contents of the files
        // are the same. A return value of фолс indicates that the
        // files are not the same.
        static private bool FileCompare(string file1, string file2)
        {
            FileStream fs1 = null;// = new FileStream(file2, FileMode.Open);
            FileStream fs2=null;
            try
            {
                int file1byte;
                int file2byte;
                

                // Open the two files.
                fs1 = new FileStream(file1, FileMode.Open);            
                fs2 = new FileStream(file2, FileMode.Open);

                // Check the file sizes. 
                if (fs1.Length != fs2.Length)
                {
                    // Close the file
                    fs1.Close();
                    fs2.Close();

                    // Return false to indicate files are different
                    return false;
                }

                // Read and compare a byte from each file until either a
                // non-matching set of bytes is found or until the end of
                // file1 is reached.
                do
                {
                    // Read one byte from each file.
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                }
                while ((file1byte == file2byte) && (file1byte != -1));

                // Close the files.
                fs1.Close();
                fs2.Close();

                // Return the success of the comparison. "file1byte" is
                // equal to "file2byte" at this point only if the files are
                // the same.
                return ((file1byte - file2byte) == 0);
            }
            catch
            {

            }
            finally
            {
                if(fs1!=null)
                {
                    fs1.Close();
                }
                if (fs2 != null)
                {
                    fs2.Close();
                }
            }
            return false;
        }
    }
}
