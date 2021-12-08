//Reut Galinsky 323946673
//Osnat Ashush 323810002
using System;
using System.Collections;
using System.Collections.Generic;
using Dal;
using DO;

public enum Options { Adding = 1, Updating, ShowItemp, ShowList, exit }//enum for optional actions
public enum States { BaseStation = 1, Drone, Customer, Parcel }//enum for internal choices
public enum Update { Drone = 1, BaseStation, Customer }//enum for updating options


namespace ConsoleUI
{
    class Program
    {

       
        static public void PrintMenu()//the first menu for the user
        {
            Console.WriteLine(@"1. adding
2. updating
3. show single item
4. show list
5. exit");
        }
        /// <summary>
        /// reading the new station's details
        /// </summary>
        /// <param name="system"></param>
        static public void AddBaseStation(DalApi.IDal system)
        {
            Console.WriteLine("please enter id number for the new base station");
            BaseStation Base1 = new BaseStation();
            Base1.IdNumber = (Console.ReadLine());
            Console.WriteLine("please enter the name of the station");
            Base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            try
            {
                Base1.ChargeSlots = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("this amount is illegal please try to add this base station again");
                return;
            }
            try
            {
                system.AddBaseStation(Base1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        /// <summary>
        /// reading the new Drone's details
        /// </summary>
        /// <param name="system"></param>
        static public void AddDrone(DalApi.IDal system)
        {
            Console.WriteLine("please enter id number for the new drone");//האם צריך לבדוק שאכן ייחודי?
            Drone Drone1 = new Drone();
            Drone1.IdNumber = Console.ReadLine();
            Console.WriteLine("please enter the model of the drone");
            Drone1.Model = Console.ReadLine();
            Console.WriteLine("please enter the weight of your drone: 1 for light, 2 for middle and 3 for heavy");
            try
            {
                Drone1.MaxWeight = (WeightCategories)(int.Parse(Console.ReadLine()));
            }
            catch (Exception e)
            {
                Console.WriteLine("you have not entered a number, please try to add this drone again");
                return;
            }
            try
            {
                system.AddDrone(Drone1);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        /// <summary>
        /// reading the details of the new customer
        /// </summary>
        /// <param name="system"></param>
        static public void AddCustomer(DalApi.IDal system)
        {
            Console.WriteLine("please enter id number for the new customer");
            Customer Customer1 = new Customer() { IdNumber = (Console.ReadLine()) };
            Console.WriteLine("please enter the name of customer");
            Customer1.Name = Console.ReadLine();
            Console.WriteLine("plesae enter the phone number of the new customer");
            Customer1.Phone = Console.ReadLine();
            try
            {
                system.AddCustomer(Customer1);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        /// <summary>
        /// read the details of the new parcel
        /// </summary>
        /// <param name="system"></param>
        static public void AddParcel(DalApi.IDal system)
        {

            Parcel Parcel1 = new Parcel();
            Parcel1.IdNumber = null;
            Console.WriteLine("please enter the id of the sender customer");
            Parcel1.Sender = (Console.ReadLine());
            Console.WriteLine("please enter the id of the reciever customer");
            Parcel1.Geter = (Console.ReadLine());
            Console.WriteLine("please enter the weight of your Parcel: 1 for light, 2 for middle and 3 for heavy");
            try
            {
                Parcel1.Weight = (WeightCategories)(int.Parse(Console.ReadLine()));
                Console.WriteLine("please enter the priority of your Parcel: 1 for Regular, 2 for Speed and 3 for Emergency");
                Parcel1.Priority = (Priorities)(int.Parse(Console.ReadLine()));
            }
            catch (Exception e)
            {
                Console.WriteLine("you have not entered a number, please try to add this parcel again");
                return;
            }
            Parcel1.DroneId = null;
            Parcel1.CreateParcelTime = DateTime.Now;
            Parcel1.CollectingDroneTime = null;
            Parcel1.MatchForDroneTime = null;
            Parcel1.ArrivingDroneTime = null;
            try
            {
                system.AddParcel(Parcel1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void UpdateDrone(DalApi.IDal system)
        {
            Console.WriteLine("please enter drone's id");
            string id = Console.ReadLine();
            Console.WriteLine("please enter new Model");
            Drone temp = system.GetDrone(id);
            temp.Model = Console.ReadLine();
            try
            {
                system.UpdateDrone(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void UpdateCustomer(DalApi.IDal system)
        {
            Console.WriteLine("please enter customer id");
            string id = Console.ReadLine();
            Customer temp = system.GetCustomer(id);
            Console.WriteLine("please enter new phone");
            temp.Phone = Console.ReadLine();
            Console.WriteLine("please enter new Name");
            temp.Name = Console.ReadLine();
            try
            {
                system.UpdateCustomer(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static public void UpdateBaseStation(DalApi.IDal system)
        {
            Console.WriteLine("please enter Base station id");
            string id = Console.ReadLine();
            BaseStation temp = system.GetBaseStation(id);
            Console.WriteLine("please enter new amount of charge slots");
            try
            {
                temp.ChargeSlots = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                ;
            }
            Console.WriteLine("please enter new Name");
            temp.Name = Console.ReadLine();
            try
            {
                system.UpdateBaseStation(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void Main(string[] args)
        {
            DalApi.IDal DeliverySystem = DalApi.DLFactory.GetDal();
            string choise;
            Console.WriteLine("Hi, welcome to the new system of Drone's delivery");
            Options choose = (Options)(1);
            while (choose != Options.exit)
            {
                try
                {
                    PrintMenu();
                    choise = (Console.ReadLine());
                    while ((choise == ""))//the program wont crash if it enter will be pressed
                    {
                        choise = Console.ReadLine();
                    }
                    choose = (Options)int.Parse(choise);
                    States internalChoose = (States)0;
                    Update secondChoos = (Update)0;
                    switch (choose)
                    {
                        case Options.Adding:
                            Console.WriteLine("1.adding new base station \n2.adding drone \n3.adding new customer \n4.adding new parcel");
                            choise = (Console.ReadLine());
                            while ((choise == ""))//the program wont crash if it enter will be pressed
                            {
                                choise = Console.ReadLine();
                            }
                            internalChoose = (States)int.Parse(choise);
                            switch (internalChoose)
                            {
                                case States.BaseStation:
                                    AddBaseStation(DeliverySystem);
                                    break;
                                case States.Drone:
                                    AddDrone(DeliverySystem);
                                    break;
                                case States.Customer:
                                    AddCustomer(DeliverySystem);
                                    break;
                                case States.Parcel:
                                    AddParcel(DeliverySystem);
                                    break;
                                default:
                                    Console.WriteLine("error-invalid input");
                                    break;
                            }
                            break;
                        case Options.Updating:
                            Console.WriteLine("1.update drone \n2.update base station \n3.update customer");
                            choise = (Console.ReadLine());
                            while ((choise == ""))//the program wont crash if it enter will be pressed
                            {
                                choise = Console.ReadLine();
                            }
                            secondChoos = (Update)int.Parse(choise);
                            switch (secondChoos)
                            {
                                case Update.Drone:
                                    UpdateDrone(DeliverySystem);
                                    break;
                                case Update.BaseStation:
                                    UpdateBaseStation(DeliverySystem);
                                    break;
                                case Update.Customer:
                                    UpdateCustomer(DeliverySystem);
                                    break;
                                default:
                                    Console.WriteLine("error-invalid output");
                                    break;
                            }
                            break;
                        case Options.ShowItemp:
                            Console.WriteLine("1.show base station \n2.show drone \n3.show customer \n4.show parcel");
                            choise = (Console.ReadLine());
                            while ((choise == ""))//the program wont crash if it enter will be pressed
                            {
                                choise = Console.ReadLine();
                            }
                            internalChoose = (States)int.Parse(choise);
                            Console.WriteLine("please enter the id number");
                            string num = (Console.ReadLine());
                            switch (internalChoose)
                            {
                                case States.BaseStation:
                                    try
                                    {
                                        Console.WriteLine(DeliverySystem.GetBaseStation(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case States.Drone:
                                    try
                                    {
                                        Console.WriteLine(DeliverySystem.GetDrone(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case States.Customer:
                                    try
                                    {
                                        Console.WriteLine(DeliverySystem.GetCustomer(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case States.Parcel:
                                    try
                                    {
                                        Console.WriteLine(DeliverySystem.GetParcel(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException.Message);
                                    }
                                    break;
                                default:
                                    Console.WriteLine("error-invalid input");
                                    break;
                            }
                            break;
                        case Options.ShowList:
                            Console.WriteLine("1.show the list of base stations \n2.show the list of the drones \n3.show the list of the customer \n4.show the list of the parcel \n5.show the list of the unmatched percels \n6.show the list of base stations with availible charge slots");
                            choise = (Console.ReadLine());
                            while ((choise == ""))//the program wont crash if it enter will be pressed
                            {
                                choise = Console.ReadLine();
                            }
                            internalChoose = (States)int.Parse(choise);
                            switch (internalChoose)
                            {
                                case States.BaseStation:
                                    foreach (var item in DeliverySystem.GetBaseStations())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Drone:
                                    foreach (var item in DeliverySystem.GetDrones())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Customer:
                                    foreach (var item in DeliverySystem.GetCustomers())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Parcel:
                                    foreach (var item in DeliverySystem.GetParcels())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                default:
                                    Console.WriteLine("error- invalid input");
                                    break;
                            }
                            break;
                        case Options.exit:
                            break;
                        default:
                            Console.WriteLine("error- invalid input");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("illegal choose");
                }
            }

        }

    }
}