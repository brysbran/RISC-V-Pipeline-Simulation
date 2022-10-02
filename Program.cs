using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Pipeline_Lab{
    public class Program{
        static void Main(string[] args)
        {
            try{
            Pipeline_Lab.Pipeline.Process();
            }
            catch(NullReferenceException ex){
                System.Console.WriteLine("Oops the object was not initialized properly.");
            }
        }
    }
}