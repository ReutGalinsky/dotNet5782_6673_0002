using System;
using System.Collections;
using System.Collections.Generic;
using DalObject;

public enum Options { Adding, Updating, ShowItemp, ShowList,exit }
public enum States { BaseStation, Drone, Customer,Parcel, Unmatched,Available }
public enum Update { Match, Collect, Giving, Sending, Release }


namespace ConsoleUI
{
    class Program
    {
        static public void PrintMenu()
        {
            Console.WriteLine(@"1. adding
                                2. updating
                                3. show single item
                                4. show list");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("hi, welcome to the new system of Drone's delivery");
            Options choose = (Options)(1);
            while (choose != Options.exit)
            {
                PrintMenu();
                choose = (Options)Console.Read();
                States internalChoose=(States)0;
                Update specialChoose =(Update) 0;
                switch (choose)
                {
                    case Options.Adding:
                        Console.WriteLine("1. adding new base station \n 2.adding drone \n 3.adding new customer \n 4.adding new parcel");
                        internalChoose = (States)Console.Read();
                        
                            switch (internalChoose)
                            {
                                case States.BaseStation:

                                    break;
                                case States.Drone:
                                    break;
                                case States.Customer:
                                    break;
                                case States.Parcel:
                                    break;
                               
                                default:
                                    break;
                            }
                        break;
                    case Options.Updating:
                        Console.WriteLine("1. match parcel to a drone \n 2.collecting parcel by a drone \n 3.giving parcel to a customer \n 4.sending a dorne to be charged \n 5. release drone from charging");
                        specialChoose = (Update)Console.Read();
                        switch (specialChoose)
                        {
                            case Update.Match:
                                break;
                            case Update.Collect:
                                break;
                            case Update.Giving:
                                break;
                            case Update.Sending:
                                break;
                            case Update.Release:
                                break;
                            default:
                                break;
                        }
                        break;
                    case Options.ShowItemp:
                        Console.WriteLine("1. show base station \n 2.show drone \n 3.show customer \n 4.show parcel");
                        internalChoose = (States)Console.Read();
                        switch (internalChoose)
                        {
                            case States.BaseStation:
                                break;
                            case States.Drone:
                                break;
                            case States.Customer:
                                break;
                            case States.Parcel:
                                break;
                            case States.Unmatched:
                            default:
                                break;
                        }
                        break;
                    case Options.ShowList:
                        Console.WriteLine("1. show the list of base stations \n 2. show the list of the drones \n 3.shoe the list of the customer \n 4. shoe the list of the parcel \n 5. show the list of the unmatched percels \n 6. show the list of base stations with availible charge slots");
                        break;
                    case Options.exit:
                        break;
                    default:
                        break;
                }
            }

        }

    }
}
