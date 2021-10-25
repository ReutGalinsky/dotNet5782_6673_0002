using System;
using System.Collections;
using System.Collections.Generic;
using DalObject;
using IDAL.DO;

public enum Options { Adding=1, Updating, ShowItemp, ShowList,exit }
public enum States { BaseStation=1, Drone, Customer,Parcel, Unmatched,Available }
public enum Update { Match=1, Collect, Giving, Sending, Release }


namespace ConsoleUI
{
    class Program
    {
        static public void PrintMenu()
        {
            Console.WriteLine(@"1. adding
                                2. updating
                                3. show single item
                                4. show list
                                5. exit");
        }
        //static public bool check<T>(int i,List<T> l)
        //{

        //}
        /// <summary>
        /// reading the new station's details
        /// </summary>
        /// <param name="system"></param>
        static public void AddBaseStation(DalObject.DalObject system)
        {
            Console.WriteLine("please enter id number for the new base station");//האם צריך לבדוק שאכן ייחודי?
            BaseStation base1 = new BaseStation();
            base1.IdNumber= Console.Read();
            while(system.getItem<BaseStation>(base1).IdNumber==0 )
            {
                Console.WriteLine("this id is already exist. please enter new one");
                base1.IdNumber = Console.Read();
            }
            Console.WriteLine("please enter the name of the station");
            base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            base1.ChargeSlots = Console.Read();
            Console.WriteLine("please enter the location of your base station (longitude,latitude)");
            base1.Longitude = Console.Read();
            base1.Latitude = Console.Read();
            system.AddingBaseStation(base1);

        }
        /// <summary>
        /// reading the new Drone's details
        /// </summary>
        /// <param name="system"></param>
        static public void AddDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter id number for the new drone");//האם צריך לבדוק שאכן ייחודי?
            Drone Drone1 = new Drone();
            Drone1.IdNumber = Console.Read();
            Console.WriteLine("please enter the model of the drone");
            Drone1.Model = Console.ReadLine();
            Console.WriteLine("please enter the weight of your drone: 1 for light, 2 for middle and 3 for heavy");
            Drone1.MaxWeight = (WeightCategories)(Console.Read());
            Console.WriteLine("please enter the battary status of the drone");//האם צריך לאתחל ישר ל100? האם לבדוק תקינות?
            Drone1.Battery = Console.Read();
            Console.WriteLine("please enter the status of your drone: 1 for availible, 2 for maintence and 3 for shipping");
            Drone1.Status = (DroneStatus)(Console.Read());
            system.AddingDrone(Drone1);
        }
        /// <summary>
        /// reading the details of the new customer
        /// </summary>
        /// <param name="system"></param>
        static public void AddCustomer(DalObject.DalObject system)
        {
            Console.WriteLine("please enter id number for the new customer");
            Customer Customer1 = new Customer();
            Customer1.Id = Console.Read();
            Console.WriteLine("please enter the name of customer");
            Customer1.Name = Console.ReadLine();
            Console.WriteLine("plesae enter the phone number of the new customer");
            Customer1.Phone = Console.ReadLine();
            Console.WriteLine("please enter the location of your base station (longitude,latitude)");
            Customer1.Longitude = Console.Read();
            Customer1.Latitude = Console.Read();
            system.addingCustomer(Customer1);
        }
        /// <summary>
        /// read the details of the new parcel
        /// </summary>
        /// <param name="system"></param>
        static public void AddParcel(DalObject.DalObject system)
        {
            Parcel Parcel1 = new Parcel();
            Parcel1.IdNumber = 0;
            Console.WriteLine("please enter the id of the sender customer");
            Parcel1.ClientSendName = Console.Read();
            Console.WriteLine("please enter the id of the reciever customer");
            Parcel1.ClientGetName = Console.Read();
            Console.WriteLine("please enter the weight of your Parcel: 1 for light, 2 for middle and 3 for heavy");
            Parcel1.Weight = (WeightCategories)(Console.Read());
            Console.WriteLine("please enter the priority of your Parcel: 1 for Regular, 2 for Speed and 3 for Emergency");
            Parcel1.Priority = (Priorities)(Console.Read());
            Parcel1.DroneId = 0;
            Parcel1.CreateParcelTime = DateTime.Now;
            Parcel1.collectingDroneTime = new DateTime();//האם באמת צריך לאתחל ככה ל0?
            Parcel1.MatchForDroneTime = new DateTime();
            Parcel1.ArrivingDroneTime = new DateTime();
            system.AddingParcel(Parcel1);
        }
        static public void MatchParcelToDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            int num = Console.Read();
            Parcel temp = new Parcel() { IdNumber = num };
            //temp = system.getItem(temp);
            //if(temp.IdNumber==0)
            //{            system.ParcelToDrone(temp);

            //    Console.WriteLine("error-invalid input");
            //    return;
            //}
        }

        static public void CollectingFromCustomer(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            int num = Console.Read();
            Parcel temp = new Parcel() { IdNumber = num };
            system.ParcelToCollecting(temp);

        }
        static public void GivingToCustomer(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            int num = Console.Read();
            Parcel temp = new Parcel() { IdNumber = num };
            system.ParcelToCustomer(temp);

        }
        static public void RelaseCharge(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the Drone's code");
            Drone Dc = new Drone() { IdNumber = Console.Read() };
            system.releaseCharge(Dc);
        }

        static public void ChargeDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the Drone's code");
            DroneCharge Dc = new DroneCharge() { DroneId = Console.Read() };
            Console.WriteLine( "the list of the availible charge stations:");
            List<BaseStation> availibles = system.GetAvailibeStation();
            foreach (var item in availibles)
                Console.WriteLine("*" + item + "\n");
            Console.WriteLine("enter the code of the wanted station:");
            Dc.StationId = Console.Read();
            system.SendToCharge(Dc);
        }

        static void Main(string[] args)
        {
            DalObject.DalObject DeliverySystem = new DalObject.DalObject();
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
                        Console.WriteLine("1. match parcel to a drone \n 2.collecting parcel by a drone \n 3.giving parcel to a customer \n 4.sending a dorne to be charged \n 5. release drone from charging");
                        specialChoose = (Update)Console.Read();
                        switch (specialChoose)
                        {
                            case Update.Match:
                                MatchParcelToDrone(DeliverySystem);
                                break;
                            case Update.Collect:
                                CollectingFromCustomer(DeliverySystem);
                                break;
                            case Update.Giving:
                                GivingToCustomer(DeliverySystem);
                                break;
                            case Update.Sending:
                                ChargeDrone(DeliverySystem);
                                break;
                            case Update.Release:

                                break;
                            default:
                                Console.WriteLine("error-invalid output");
                                break;
                        }
                        break;
                    case Options.ShowItemp:
                        Console.WriteLine("1. show base station \n 2.show drone \n 3.show customer \n 4.show parcel");
                        internalChoose = (States)Console.Read();
                        Console.WriteLine("please enter the id number");
                        int num = Console.Read();
                        switch (internalChoose)
                        {
                            case States.BaseStation:
                                BaseStation help = new BaseStation() { IdNumber = num };
                                help = DeliverySystem.getItem<BaseStation>(help);
                                if(help.IdNumber==0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(help);
                                break;
                            case States.Drone:
                                Drone Dront = new Drone() { IdNumber = num };
                                Dront = DeliverySystem.getItem<Drone>(Dront);
                                if (Dront.IdNumber == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(Dront);
                                break;
                            case States.Customer:
                                Customer cust = new Customer() { Id = num };
                                cust = DeliverySystem.getItem<Customer>(cust);
                                if (cust.Id == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(cust);
                                break;
                            case States.Parcel:
                                Parcel parc = new Parcel() { IdNumber = num };
                                parc = DeliverySystem.getItem<Parcel>(parc);
                                if (parc.IdNumber == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(parc);
                                break;
                            default:
                                Console.WriteLine("error-invalid input");
                                break;
                        }
                        break;
                    case Options.ShowList:
                        Console.WriteLine("1. show the list of base stations \n 2. show the list of the drones \n 3.shoe the list of the customer \n 4. shoe the list of the parcel \n 5. show the list of the unmatched percels \n 6. show the list of base stations with availible charge slots");
                        internalChoose = (States)Console.Read();
                        switch (internalChoose)
                        {
                            case States.BaseStation:
                                List < BaseStation > bases= DeliverySystem.GetBaseStations();
                                foreach(var item in bases)
                                    Console.WriteLine("*"+item+"\n");
                                break;
                            case States.Drone:
                                List<Drone> Drones = DeliverySystem.GetDrones();
                                foreach (var item in Drones)
                                    Console.WriteLine("*" + item + "\n");
                                break;
                            case States.Customer:
                                List<Customer> Customers = DeliverySystem.GetCustomers();
                                foreach (var item in Customers)
                                    Console.WriteLine("*" + item + "\n");
                                break;
                            case States.Parcel:
                                List<Parcel> Parcels = DeliverySystem.GetParcels();
                                foreach (var item in Parcels)
                                    Console.WriteLine("*" + item + "\n");
                                break;
                            case States.Unmatched:
                                List<Parcel> NoMatches = DeliverySystem.GetNonMatchParcels();
                                foreach (var item in NoMatches)
                                    Console.WriteLine("*" + item + "\n");
                                break;
                            case States.Available:
                                List<BaseStation> availibles = DeliverySystem.GetAvailibeStation();
                                foreach (var item in availibles)
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
                        break;
                }
            }

        }

    }
}
