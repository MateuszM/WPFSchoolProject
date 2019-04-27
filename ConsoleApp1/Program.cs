﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpa3.Loggers;
using Tpa3.Logic;
using Tpa3.Models;
using Tpa3.ViewModel;

namespace ConsoleApp1
{
    class Program
    {
        static void Display(List<string> a)
        {
            foreach (string d in a)
                Console.WriteLine(d);
        }
        static void Display(List<TreeNode> s)
        {
            foreach (TreeNode d in s)
                Console.WriteLine(d.Value);
        }
      static ILogger logger;
       static Serialization j = new Serialization();
        static List<TreeNode> Node;
        static SecondViewModelCh d;

        static void Main(string[] args)
        {
            logger = new Logger();
          Node = new List<TreeNode>();
            try
            {
                logger.Trac("StartOfMain");
                String command = args[0];
                String filename = args[1];
                switch (command)
                {
                    case "LoadFromXml":
                        {
                            d = new SecondViewModelCh(filename,"Serialization");
                            d.Load();
                            Node = d.GreatParent;
                            break;
                        }
                    case "Open":
                        {
                            d = new SecondViewModelCh(filename,"Cos");
                            d.ReadFromDll(filename);
                            Node = d.GreatParent;

                            break;
                        }
                    case "SaveToDatabase":
                         {
                            d = new SecondViewModelCh(filename,"Database");
                          //  Node = d.GreatParent;
                            d.Save();
                            break;   
                        }
                    case "SaveToXml":
                        {
                            d = new SecondViewModelCh(filename,"Serialization");
                            //  Node
                            d.Save();
                            break;
                        }
                    case "LoadFromDatabase":
                        {
                            d = new SecondViewModelCh(filename,"Database");
                            Node = d.GreatParent;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Commend");
                            break;
                        }


                }


                Loop();
            }
            catch (Exception d)
            {
                logger.Error(d.ToString());
            }
           
            //   Console.ReadLine();

        }
       static String space = "";
       private static void Loop()
        {
            string command;
            bool quit = false;
            
            while(!quit)
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "load":
                        {
                            string loadl;
                            Console.WriteLine("Filename");
                            loadl=Console.ReadLine();
                           Node[0]=j.Deserialization(loadl);
                            break;
                        }
                    case "save":
                        {
                            Console.WriteLine("Nazwa pliku");
                            string load = Console.ReadLine();
                            j.Serialize(load, Node[0]);
                            break; }

                    case "open":
                        {
                            Console.WriteLine("FileName");
                            string load = Console.ReadLine();
                            d = new SecondViewModelCh(load,"Cos");


                            break;
                        }
                    case "display":
                        {
                            Ks(Node, true, "");
                            break;
                        }
                    case "quit":
                        {
                            quit = true;
                            break;
                        }
                }
            }
        }
        public static bool Ks(List<TreeNode> d,bool first,string space)
        {
            String addspace = "     ";
            space = addspace + space;
           
                
                        
                        //Display(d);
                        foreach (TreeNode s in d)
                    {
                            Console.WriteLine(space+s.TypeName);
                            Console.WriteLine(space+s.Value);
                            Ks(s.Childs, false,space);
                    }
               // Console.WriteLine("                             back");
                
               
           
            return false;
        }
       
    }
}
