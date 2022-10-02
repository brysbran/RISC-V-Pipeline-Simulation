using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Pipeline_Lab{

public class Pipeline{

    private static Dictionary<string, string> pipeline;
    private static int clock_cycle = 0;
    private int count = 1;
    private static String[] instructions;
    private static Boolean[] registers;
    private static int program_counter = 1;

    

    //puts instructions into array
    public static void Reader(){
        Pipeline.instructions = File.ReadAllLines("input.trace");
            }

    public static void Fetch(string instruction_id){
        Console.WriteLine("\nInstruction fetched of instruction id: " + instruction_id + "\n");
    }
    public static void Decode(string instruction_id){
        Console.WriteLine("\nInstruction decoded of instruction id: " + instruction_id + "\n");
    }
    public static void Execute(string instruction_id){
        Console.WriteLine("\nInstruction executed of instruction id: " + instruction_id + "\n");
    }
    public static void Memory(string instruction_id){
        Console.WriteLine("\nMemory Accessed: instruction id: " + instruction_id + "\n");
    }
    public static void WriteBack(string instruction_id){
        Console.WriteLine("\nWriteback phase of instruction id: " + instruction_id + "\n");
        Console.WriteLine("--------Instruction id: " + instruction_id + "was completed at clock cyle: "+ Pipeline.clock_cycle + "----------");
    }

    public static void Stages(){
        
        string delete_instr = "";
        Pipeline.clock_cycle += 1;
        Boolean flag = true;
        Console.WriteLine("Different Phase of Instructions in clock cycle: " + Pipeline.clock_cycle);
        foreach(KeyValuePair<string, string> kvp in pipeline){
            if(kvp.Value.Length > 0){
                if(kvp.Key == "IF"){
                    Pipeline.Fetch(kvp.Value);
                }
                else if(kvp.Key == "ID"){
                    Pipeline.Decode(kvp.Value);
                }
                else if(kvp.Key == "EX"){
                    Pipeline.Execute(kvp.Value);
                }
                else if(kvp.Key == "MEM"){
                    Pipeline.Memory(kvp.Value);
                }
                else Pipeline.WriteBack(kvp.Value);
            }
            else
            {
                flag = false;
                delete_instr = kvp.Key;
            } 

        }
        if(flag == false){
            Pipeline.pipeline.Remove(delete_instr);
        }
        Thread.Sleep(10);


    }


    public static void Process(){
        string IF = "IF";
        string ID = "ID";
        string EX = "EX";
        string MEM = "MEM";
        string WB = "WB";
        Pipeline.Reader();
        
        for(int i = 0; i < instructions.Length; i++){
            Console.WriteLine("\nFetching instruction " + instructions[i] + " with instruction ID: " + Pipeline.program_counter + "\n");
            foreach(KeyValuePair<string, string> kvp in Pipeline.pipeline){
                kvp.Key.Insert(Pipeline.program_counter, IF);
                kvp.Key.Insert(Pipeline.program_counter, ID);
                kvp.Key.Insert(Pipeline.program_counter, EX);
                kvp.Key.Insert(Pipeline.program_counter, MEM);
                kvp.Key.Insert(Pipeline.program_counter, WB);
            }
            Pipeline.Stages();
            program_counter += 1;
        }

        while(true){
            if(pipeline != null){
                Pipeline.Stages();
            }
            else break;
        }

        Console.WriteLine("\nThe pipeline simulation is now complete with total # clock cycles: " + clock_cycle);
        System.Console.WriteLine("\nTotal number of instructions executed: " + program_counter);
    }





}
}