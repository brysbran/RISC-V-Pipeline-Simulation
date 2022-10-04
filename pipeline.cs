using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Pipeline_Lab{

public class Pipeline{

    //holds the id for the instruction in the key field and the instruction itself in value
    public static IDictionary<String, String> pipeline = new Dictionary<String, String>();
    private static int clock_cycle = 0;
    private static String[] instructions; //string holding the instructions
    private static Boolean[] registers; //idea i had that maybe we could count register read/write by setting each index of this to true when the register is
                                        // read/written to
    private static int program_counter = 0;
    static string IF = "IF";
    static string ID = "ID";
    static string EX = "EX";
    static string MEM = "MEM";
    static string WB = "WB";
    static int idCount= 0;

    

    //puts instructions into array
    public static void Reader(){
        Pipeline.instructions = File.ReadAllLines("input.trace");
            }

/*
These 5 functions are the 5 stages of the pipeline, which basically just print the instruction stage and ID
*/
    public static void Fetch(string instruction_id){
       // Pipeline.pipeline.Remove(IF);
        Console.WriteLine("\nInstruction fetched of instruction id: " + instruction_id + "\n");
    }
    public static void Decode(string instruction_id){
       // Pipeline.pipeline.Remove(ID);
        Console.WriteLine("\nInstruction decoded of instruction id: " + instruction_id + "\n");
    }
    public static void Execute(string instruction_id){
        //Pipeline.pipeline.Remove(EX);
        Console.WriteLine("\nInstruction executed of instruction id: " + instruction_id + "\n");
    }
    public static void Memory(string instruction_id){
        //Pipeline.pipeline.Remove(MEM);
        Console.WriteLine("\nMemory Accessed: instruction id: " + instruction_id + "\n");
    }
    public static void WriteBack(string instruction_id){
        //Pipeline.pipeline.Remove(WB);
        Console.WriteLine("\nWriteback phase of instruction id: " + instruction_id + "\n");
        Console.WriteLine("--------Instruction id: " + instruction_id + "was completed at clock cyle: "+ Pipeline.clock_cycle + "----------");
    }

    /*
    The stages method is where the order of the different stages is assigned
    */
    public static void Stages(){
        
        string delete_instr = ""; //essentially sets the ID of an instruction to blank if the instruction length is 0
        Boolean flag = true;
        Console.WriteLine("Different Phase of Instructions in clock cycle: " + clock_cycle);
        foreach(KeyValuePair<string, string> entry in pipeline){
            //if(pipeline.Count > 0){
             
                if(entry.Key == "IF"){
                    Pipeline.Fetch(entry.Key);
                     clock_cycle++;
                }
                else if(entry.Key == "ID"){
                    Pipeline.Decode(entry.Key);
                     clock_cycle++;
                }
                else if(entry.Key == "EX"){
                    Pipeline.Execute(entry.Key);
                     clock_cycle++;
                }
                else if(entry.Key == "MEM"){
                    Pipeline.Memory(entry.Key);
                     clock_cycle++;
                }
                else if(entry.Key == "WB"){
                     Pipeline.WriteBack(entry.Key);
                      clock_cycle++;
                }
            
            //else
            //{
            //    flag = false;
            //} 
           // }
        }
        if(flag == false){
            Pipeline.pipeline.Remove(delete_instr); //removes the instruction with a blank ID
        }
      Thread.Sleep(100);


    }

   public static void keyAssignment(){
    string[] ids = new string[]{"IF", "ID", "EX", "MEM", "WB"};

    foreach(string s in ids){
        pipeline.Add(s, "NULL");
    }
   }

    /*
    This function is where the actual "computations" are occurring, in which the IDs are assigned
    to the different instructions and calls the stages() functions
    */
    public static void Process(){
       
       
        Pipeline.Reader();
        Pipeline.keyAssignment();
    foreach(string s in instructions){
        program_counter++;
        clock_cycle=0;
        foreach(KeyValuePair<string, string> entry in pipeline){
            pipeline[entry.Key] = s;

            }
           Console.WriteLine("\nFetching instruction [" + s + "] with instruction ID: " + Pipeline.program_counter + "\n");
            Pipeline.Stages();
            }

            /*foreach(KeyValuePair<string, string> entry2 in pipeline){
            Console.WriteLine("\nFetching instruction [" + pipeline[entry2.Key] + "] with instruction ID: " + Pipeline.program_counter + "\n");
           System.Console.WriteLine("terr");
            Pipeline.Stages();
            }*/
        
    
        Console.WriteLine("\nThe pipeline simulation is now complete with total # clock cycles: " + clock_cycle);
        System.Console.WriteLine("\nTotal number of instructions executed: " + program_counter);
    }

    public static void print(){
        Pipeline.Reader();
        Pipeline.keyAssignment();
        /*for(int i = 0; i < instructions.Length; i ++){
            System.Console.WriteLine(instructions[i]);
        }*/
        foreach(string s in instructions){
        foreach(KeyValuePair<string, string> entry in pipeline){
            pipeline[entry.Key] = s;

           
           
            }
             pipeline.ToList().ForEach(x => System.Console.WriteLine(x.Key + x.Value));
        }
        
    }





}
}