using System;
using BL;
using BO;
using System.Linq;


namespace ConsoleUI_BL
{
    public enum Options { Adding = 1, Updating, ShowItemp, ShowList, exit }//enum for optional actions
    public enum States { BaseStation = 1, Drone, Customer, Parcel, Unmatched, Available }//enum for internal choices
    public enum Update { Match = 1, Collect, Giving, Sending, Release, Drone, BaseStation, Customer }//enum for updating options

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
        #region AddBaseStation
        static public void AddBaseStation(BLApi.IBL system)
        {
            Console.WriteLine("please enter id number for the new base station");
            BO.BaseStation Base1 = new BaseStation();
            Base1.IdNumber = Console.ReadLine();
            Console.WriteLine("please enter the name of the station");
            Base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            try
            {
                Base1.ChargeSlots = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("invalid value for amount of charge slots");
                return;
            }
            Console.WriteLine("please enter the location of your base station in israel in format Latitude:Longitude");
            string temp = Console.ReadLine();
            var list = temp.Split(":");
            if (list.Length != 2)
            { Console.WriteLine("invalid Location"); return; }
            try
            {
                Base1.Location = new Location();
                Base1.Location.Latitude = double.Parse(list[0]);
                Base1.Location.Longitude = double.Parse(list[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Latitude or Longitude is invalid"); return;
            }
            try
            {
                system.AddBaseStation(Base1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region AddDrone

        static public void AddDrone(BLApi.IBL system)
        {
            Console.WriteLine("please enter id number for the new drone");
            BO.DroneToList Drone1 = new BO.DroneToList();
            Drone1.IdNumber = Console.ReadLine();
            Console.WriteLine("please enter the model of the drone");
            Drone1.Model = Console.ReadLine();
            Console.WriteLine("please enter the weight of your drone: 1 for light, 2 for middle and 3 for heavy");
            Drone1.MaxWeight = (WeightCategories)(int.Parse(Console.ReadLine()));
            Console.WriteLine("please enter id number of the base station for first charge");
            string num = Console.ReadLine();
            try
            {
                system.AddDrone(Drone1, num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region AddCustomer
        static public void AddCustomer(BLApi.IBL system)
        {
            Console.WriteLine("please enter id number for the new customer");
            BO.Customer Customer1 = new Customer();
            Customer1.IdNumber = Console.ReadLine();
            Console.WriteLine("please enter the name of customer");
            Customer1.Name = Console.ReadLine();
            Console.WriteLine("plesae enter the phone number of the new customer");
            Customer1.Phone = Console.ReadLine();
            Console.WriteLine("please enter the location of your customer in israel in format Latitude:Longitude");
            string temp = Console.ReadLine();
            var list = temp.Split(":");
            if (list.Length != 2)
            { Console.WriteLine("invalid Location"); return; }
            try
            {
                Customer1.Location = new Location();
                Customer1.Location.Latitude = double.Parse(list[0]);
                Customer1.Location.Longitude = double.Parse(list[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Latitude or Longitude is invalid"); return;
            }
            try
            {
                system.AddCustomer(Customer1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region AddParcel
        static public void AddParcel(BLApi.IBL system)
        {
            BO.ParcelOfList Parcel1 = new BO.ParcelOfList();
            Parcel1.IdNumber = null;
            Console.WriteLine("please enter the id of the sender customer");
            Parcel1.Sender = Console.ReadLine();
            Console.WriteLine("please enter the id of the reciever customer");
            Parcel1.Geter = Console.ReadLine();
            Console.WriteLine("please enter the weight of your Parcel: 1 for light, 2 for middle and 3 for heavy");
            Parcel1.Weight = (WeightCategories)(int.Parse(Console.ReadLine()));
            Console.WriteLine("please enter the priority of your Parcel: 1 for Regular, 2 for Speed and 3 for Emergency");
            Parcel1.Priority = (Priorities)(int.Parse(Console.ReadLine()));
            try
            {
                string id = system.AddParcelToDelivery(Parcel1);
                Console.WriteLine("your pacel's id is {0}", id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        # region MatchParcelToDrone
        static public void MatchParcelToDrone(BLApi.IBL system)
        {
            Console.WriteLine("please enter the drone code");
            string num = Console.ReadLine();
            try
            {
                system.MatchingParcelToDrone(num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region CollectingFromCustomer
        static public void CollectingFromCustomer(BLApi.IBL system)
        {
            Console.WriteLine("please enter the drone code");
            string num = Console.ReadLine();
            try
            {
                system.PickingParcelByDrone(num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region GivingToCustomer
        static public void GivingToCustomer(BLApi.IBL system)
        {
            Console.WriteLine("please enter the drone code");
            string num = Console.ReadLine();
            try
            {
                system.SupplyingParcelByDrone(num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region ReleaseCharge
        static public void RelaseCharge(BLApi.IBL system)
        {
            Console.WriteLine("please enter the Drone's id");
            string num = Console.ReadLine();
            Console.WriteLine("please enter the charging time in the Format: H:M:S");
            string item = Console.ReadLine();
            var list = item.Split(':');
            if (list.Length != 3)
            { Console.WriteLine("invalid time"); return; }
            int Hours = 0, Min = 0, Sec = 0;
            try
            {
                Hours = int.Parse(list[0]);
                Min = int.Parse(list[1]);
                Sec = int.Parse(list[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine("invalid input of time");
                return;
            }
            TimeSpan t = new TimeSpan(Hours, Min, Sec);
            try
            {
                system.DroneFromCharging(num, t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region ChargeDrone
        static public void ChargeDrone(BLApi.IBL system)
        {
            Console.WriteLine("please enter the Drone's code");
            string num = Console.ReadLine();
            try
            {
                system.DroneToCharging(num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion
        #region UpdatingDrone
        static public void UpdatingDrone(BLApi.IBL system)
        {
            Console.WriteLine("please enter the Drone's id");
            string num = Console.ReadLine();
            Console.WriteLine("please enter new model");
            string model = Console.ReadLine();
            try
            {
                system.UpdatingDetailsOfDrone(model, num);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }

        }

        #endregion
        #region UpdatingBaseStation
        static public void UpdatingBaseStation(BLApi.IBL system)
        {
            Console.WriteLine("please enter the station's id");
            string num = Console.ReadLine();
            Console.WriteLine("please enter new name");
            string model = Console.ReadLine();
            Console.WriteLine("please enter new amount if charge slots code");
            string amount = Console.ReadLine();
            try
            {
                system.UpdatingDetailsOfBaseStation(num, model, amount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }

        #endregion
        #region UpdatingCustomer
        static public void UpdatingCustomer(BLApi.IBL system)
        {
            Console.WriteLine("please enter the Customer's id");
            string id = Console.ReadLine();
            Console.WriteLine("please enter new name");
            string name = Console.ReadLine();
            Console.WriteLine("please enter new phone number");
            string phone = Console.ReadLine();
            try
            {
                system.UpdatingDetailsOfCustomer(id, name, phone);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            BLApi.IBL system = null;
            try
            {
                system = BLApi.BLFactory.GetBl();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return;
            }
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
                                    AddBaseStation(system);
                                    break;
                                case States.Drone:
                                    AddDrone(system);
                                    break;
                                case States.Customer:
                                    AddCustomer(system);
                                    break;
                                case States.Parcel:
                                    AddParcel(system);
                                    break;
                                default:
                                    Console.WriteLine("error-invalid input");
                                    break;
                            }
                            break;
                        case Options.Updating:
                            Console.WriteLine("1.match parcel to a drone \n2.collecting parcel by a drone \n3.giving parcel to a customer \n4.sending a dorne to be charged \n5. release drone from charging\n6. Updating Drone's name \n7. Updating model and charge slot of base station\n8. Updating name and phone of a customer");
                            choise = (Console.ReadLine());
                            while ((choise == ""))//the program wont crash if it enter will be pressed
                            {
                                choise = Console.ReadLine();
                            }
                            secondChoos = (Update)int.Parse(choise);
                            switch (secondChoos)
                            {
                                case Update.Match:
                                    MatchParcelToDrone(system);
                                    break;
                                case Update.Collect:
                                    CollectingFromCustomer(system);
                                    break;
                                case Update.Giving:
                                    GivingToCustomer(system);
                                    break;
                                case Update.Sending:
                                    ChargeDrone(system);
                                    break;
                                case Update.Release:
                                    RelaseCharge(system);
                                    break;
                                case Update.Drone:
                                    UpdatingDrone(system);
                                    break;
                                case Update.BaseStation:
                                    UpdatingBaseStation(system);
                                    break;
                                case Update.Customer:
                                    UpdatingCustomer(system);
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
                            string num = Console.ReadLine();
                            switch (internalChoose)
                            {
                                case States.BaseStation:
                                    try
                                    {
                                        Console.WriteLine(system.GetBaseStation(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
                                    }
                                    break;
                                case States.Drone:
                                    try
                                    {
                                        Console.WriteLine(system.GetDrone(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
                                    }
                                    break;
                                case States.Customer:
                                    try
                                    {
                                        Console.WriteLine(system.GetCustomer(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
                                    }
                                    break;
                                case States.Parcel:
                                    try
                                    {
                                        Console.WriteLine(system.GetParcel(num));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.InnerException == null ? e.Message : e.InnerException.Message);
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
                                    foreach (var item in system.GetBaseStations())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Drone:
                                    foreach (var item in system.GetDrones())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Customer:
                                    foreach (var item in system.GetCustomers())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Parcel:
                                    foreach (var item in system.GetParcels())
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Unmatched:
                                    foreach (var item in system.PredicateParcel(x => x.State == State.Define))
                                        Console.WriteLine("*" + item + "\n");
                                    break;
                                case States.Available:
                                    foreach (var item in system.PredicateBaseStation(x => x.ChargeSlots > 0))
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