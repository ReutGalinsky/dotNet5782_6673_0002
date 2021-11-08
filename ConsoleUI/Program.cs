//Reut Galinsky 323946673
//Osnat Ashush 323810002
using System;
using System.Collections;
using System.Collections.Generic;
using DalObject;
using IDAL.DO;

public enum Options { Adding = 1, Updating, ShowItemp, ShowList, exit }//enum for optional actions
public enum States { BaseStation = 1, Drone, Customer, Parcel, Unmatched, Available }//enum for internal choices
public enum Update { Match = 1, Collect, Giving, Sending, Release }//enum for updating options


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
        static public void AddBaseStation(DalObject.DalObject system)
        {
            Console.WriteLine("please enter id number for the new base station");
            BaseStation Base1 = new BaseStation();
            Base1.IdNumber = int.Parse(Console.ReadLine());
            while (system.getBase(Base1).IdNumber != 0)
            {
                Console.WriteLine("this id is already exist. please enter new one");
                Base1.IdNumber = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("please enter the name of the station");
            Base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            Base1.ChargeSlots = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter the location of your base station (longitude,latitude) in israel");
            Base1.Longitude = double.Parse(Console.ReadLine());
            while (Base1.Longitude < 33 || Base1.Longitude > 35)
            {
                Console.WriteLine("error- out of israel's area, please enter again");
                Base1.Longitude = double.Parse(Console.ReadLine());

            }
            Base1.Latitude = double.Parse(Console.ReadLine());
            while (Base1.Latitude < 31 || Base1.Latitude > 33)
            {
                Console.WriteLine("error- out of israel's area, please enter again");
                Base1.Latitude = double.Parse(Console.ReadLine());

            }
            system.AddingBaseStation(Base1);

        }
        /// <summary>
        /// reading the new Drone's details
        /// </summary>
        /// <param name="system"></param>
        static public void AddDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter id number for the new drone (must be different from zero)");//האם צריך לבדוק שאכן ייחודי?
            Drone Drone1 = new Drone();
            Drone1.IdNumber = int.Parse(Console.ReadLine());
            while (Drone1.IdNumber == 0)
            {
                Console.WriteLine("zero is illegal. please enter different one");
                Drone1.IdNumber = int.Parse(Console.ReadLine());
            }
            Drone temp = system.getDrone(Drone1);
            while (temp.IdNumber != 0)
            {
                Console.WriteLine("this id is already exist. please enter new one");
                Drone1.IdNumber = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("please enter the model of the drone");
            Drone1.Model = Console.ReadLine();
            Console.WriteLine("please enter the weight of your drone: 1 for light, 2 for middle and 3 for heavy");
            Drone1.MaxWeight = (WeightCategories)(int.Parse(Console.ReadLine()));
            Console.WriteLine("please enter the battary status of the drone");
            Console.WriteLine("please enter the status of your drone: 1 for availible, 2 for maintence and 3 for shipping");
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
            Customer1.Id = int.Parse(Console.ReadLine());
            while (system.getCustomer(Customer1).Id != 0)
            {
                Console.WriteLine("this id is already exist. please enter new one");
                Customer1.Id = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("please enter the name of customer");
            Customer1.Name = Console.ReadLine();
            Console.WriteLine("plesae enter the phone number of the new customer");
            Customer1.Phone = Console.ReadLine();
            Console.WriteLine("please enter the location of your base station (longitude,latitude)  in isearl");
            Customer1.Longitude = double.Parse(Console.ReadLine());
            while (Customer1.Longitude < 33 || Customer1.Longitude > 35)
            {
                Console.WriteLine("error- out of israel's area, please enter again");
                Customer1.Longitude = double.Parse(Console.ReadLine());

            }
            Customer1.Latitude = double.Parse(Console.ReadLine());
            while (Customer1.Latitude < 31 || Customer1.Latitude > 33)
            {
                Console.WriteLine("error- out of israel's area, please enter again");
                Customer1.Latitude = double.Parse(Console.ReadLine());

            }
            system.addingCustomer(Customer1);
        }
        /// <summary>
        /// read the details of the new parcel
        /// </summary>
        /// <param name="system"></param>
        static public void AddParcel(DalObject.DalObject system)
        {
            List<Parcel> Parcels = system.GetParcels();
            Parcel temp = new Parcel();
            foreach (var item in Parcels)//remove the parcels that arrieved the customer before more than a week
            {
                if (item.ArrivingDroneTime.Day < DateTime.Now.Day - 7 && item.ArrivingDroneTime.Month < DateTime.Now.Month && item.ArrivingDroneTime != (temp.ArrivingDroneTime))
                {
                    system.RemovePar(item);
                }
            }
            Parcel Parcel1 = new Parcel();
            Parcel1.IdNumber = 0;
            Console.WriteLine("please enter the id of the sender customer");
            Parcel1.ClientSendName = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter the id of the reciever customer");
            Parcel1.ClientGetName = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter the weight of your Parcel: 1 for light, 2 for middle and 3 for heavy");
            Parcel1.Weight = (WeightCategories)(int.Parse(Console.ReadLine()));
            Console.WriteLine("please enter the priority of your Parcel: 1 for Regular, 2 for Speed and 3 for Emergency");
            Parcel1.Priority = (Priorities)(int.Parse(Console.ReadLine()));
            Parcel1.DroneId = 0;
            Parcel1.CreateParcelTime = DateTime.Now;
            Parcel1.collectingDroneTime = new DateTime();
            Parcel1.MatchForDroneTime = new DateTime();
            Parcel1.ArrivingDroneTime = new DateTime();
            system.AddingParcel(Parcel1);
        }
        /// <summary>
        /// updating the time of matching of a sigle parcel
        /// </summary>
        /// <param name="system"></param>
        static public void MatchParcelToDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            Parcel temp = new Parcel() { IdNumber = int.Parse(Console.ReadLine()) };
            if (system.getParcel(temp).IdNumber == 0)
            {
                Console.WriteLine("error-invalid input");
                return;
            }
            system.ParcelToDrone(temp);

        }
        /// <summary>
        /// collecting parcel from the customer by drone
        /// </summary>
        /// <param name="system"></param>
        static public void CollectingFromCustomer(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            Parcel temp = new Parcel() { IdNumber = int.Parse(Console.ReadLine()) };
            if (system.getParcel(temp).IdNumber == 0)
            {
                Console.WriteLine("error-invalid input");
                return;
            }
            system.ParcelToCollecting(temp);
        }
        /// <summary>
        /// updating the time when the parcel arrived
        /// </summary>
        /// <param name="system"></param>
        static public void GivingToCustomer(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the parcel code");
            Parcel temp = new Parcel() { IdNumber = int.Parse(Console.ReadLine()) };
            if (system.getParcel(temp).IdNumber == 0)
            {
                Console.WriteLine("error-invalid input");
                return;
            }
            system.ParcelToCustomer(temp);
            //option to remove the parcel from the data base
            Console.WriteLine("would you agree to remove your parcel from the data base? press y or n");
            char tav = char.Parse(Console.ReadLine());
            switch (tav)
            {
                case 'y':
                    system.RemovePar(temp);
                    break;
                case 'n':
                default:
                    break;
            }

        }
        /// <summary>
        ///releasing drone from a charge slot
        /// </summary>
        /// <param name="system"></param>
        static public void RelaseCharge(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the Drone's code");
            Drone Dc = new Drone() { IdNumber = int.Parse(Console.ReadLine()) };
            if (system.getDrone(Dc).IdNumber == 0)
            {
                Console.WriteLine("error-invalid input");
                return;
            }

            system.releaseCharge(Dc);
        }
        /// <summary>
        /// sending drone for a charge slot
        /// </summary>
        /// <param name="system"></param>
        static public void ChargeDrone(DalObject.DalObject system)
        {
            Console.WriteLine("please enter the Drone's code");
            DroneCharge Dc = new DroneCharge() { DroneId = int.Parse(Console.ReadLine()) };

            Drone temp = system.getDrone(new Drone { IdNumber = Dc.DroneId });
            while (temp.IdNumber == 0)
            {
                Console.WriteLine("this id not already exist. please enter new one");
                Dc.DroneId = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("the list of the availible charge stations:");
            List<BaseStation> availibles = system.GetAvailibeStation();
            foreach (var item in availibles)
                Console.WriteLine("*" + item + "\n");
            Console.WriteLine("enter the code of the wanted station:");
            Dc.StationId = int.Parse(Console.ReadLine());
            system.SendToCharge(Dc);
        }

        static void Main(string[] args)
        {
            DalObject.DalObject DeliverySystem = new DalObject.DalObject();
            string choise;
            Console.WriteLine("Hi, welcome to the new system of Drone's delivery");
            Options choose = (Options)(1);
            while (choose != Options.exit)
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
                        Console.WriteLine("1.match parcel to a drone \n2.collecting parcel by a drone \n3.giving parcel to a customer \n4.sending a dorne to be charged \n5. release drone from charging");
                        choise = (Console.ReadLine());
                        while ((choise == ""))//the program wont crash if it enter will be pressed
                        {
                            choise = Console.ReadLine();
                        }
                        secondChoos = (Update)int.Parse(choise);
                        switch (secondChoos)
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
                                RelaseCharge(DeliverySystem);
                                break;
                            default:
                                Console.WriteLine("error-invalid output");
                                break;
                        }
                        break;
                    case Options.ShowItemp:
                        Console.WriteLine("1.show base station \n2.show drone \n3.show customer \n4.show parcel");
                        choise= (Console.ReadLine());
                        while ((choise == ""))//the program wont crash if it enter will be pressed
                        {
                            choise = Console.ReadLine();
                        }
                        internalChoose = (States)int.Parse(choise);
                        Console.WriteLine("please enter the id number");
                        int num = int.Parse(Console.ReadLine());
                        switch (internalChoose)
                        {
                            case States.BaseStation:
                                BaseStation help = new BaseStation() { IdNumber = num };
                                help = DeliverySystem.getBase(help);
                                if (help.IdNumber == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(help);
                                break;
                            case States.Drone:
                                Drone Dront = new Drone() { IdNumber = num };
                                Dront = DeliverySystem.getDrone(Dront);
                                if (Dront.IdNumber == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(Dront);
                                break;
                            case States.Customer:
                                Customer cust = new Customer() { Id = num };
                                cust = DeliverySystem.getCustomer(cust);
                                if (cust.Id == 0)
                                {
                                    Console.WriteLine("not exist");
                                }
                                else
                                    Console.WriteLine(cust);
                                break;
                            case States.Parcel:
                                Parcel parc = new Parcel() { IdNumber = num };
                                parc = DeliverySystem.getParcel(parc);
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
                                List<BaseStation> bases = DeliverySystem.GetBaseStations();
                                foreach (var item in bases)
                                    Console.WriteLine("*" + item + "\n");
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
                        Console.WriteLine("error- invalid input");
                        break;
                }
            }

        }

    }
}
