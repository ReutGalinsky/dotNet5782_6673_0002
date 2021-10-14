using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public Drone[] AddingDrone()
        {
            if(DataSource.Config.FirstDrone==10)
                Console.WriteLine("sorry! our system is full right now, please try later...");
            else
            {
                int temp=0;
                bool flag = false;
                Console.WriteLine("please enter your drone's id");
                while (flag == false)
                {
                    flag = true;
                    temp= Console.Read();
                    foreach (var item in DataSource.Drones)
                    {
                        if (item.IdNumber == temp)
                        {
                            Console.WriteLine("this id is already in our system, please enter different numbe");
                            flag = false;
                        }
                    }
                }
                DataSource.Drones[DataSource.Config.FirstDrone].IdNumber = temp;
                Console.WriteLine("please enter your drone's model");
                DataSource.Drones[DataSource.Config.FirstDrone].Model=Console.ReadLine();
                Console.WriteLine("please enter the weight-category of your drone:1:Light. 2:Middle. 3:heavy. ");
                while (flag == true)
                {
                    flag = false;
                    temp = Console.Read();
                    switch (temp)
                    {
                        case 1:
                        case 2:
                        case 3:
                            DataSource.Drones[DataSource.Config.FirstDrone].MaxWeight = (WeightCategories)(temp-1);
                            break;
                        default:
                            Console.WriteLine("your answer it's invalid. please enter again");
                            flag = true;
                            break;
                    }
                }
                Console.WriteLine("please enter your drone's battery status");
                DataSource.Drones[DataSource.Config.FirstDrone].Battery = Console.Read();
                Console.WriteLine("please enter the drone's status:1: Availible. 2:maintenace. 3:shipping. ");
                while (flag == false)
                {
                    flag = true;
                    temp = Console.Read();
                    switch (temp)
                    {
                        case 1:
                        case 2:
                        case 3:
                            DataSource.Drones[DataSource.Config.FirstDrone].Status = (DroneStatus)(temp-1);
                            break;
                        default:
                            Console.WriteLine("your answer it's invalid. please enter again");
                            flag = false;
                            break;
                    }
                }
            }
            DataSource.Config.FirstDrone++;
            //העתקה והחזרה
        }

        public Parcel[] GettingParcel()
        {
            if (DataSource.Config.FirstParcel == 1000)
                Console.WriteLine("sorry! our system is full right now, please try later...");
            else
            {
                DataSource.Parcels[DataSource.Config.FirstParcel].IdNumber = DataSource.Config.RunningNumber;
                Console.WriteLine($"your parcel's id is {DataSource.Config.RunningNumber++}");
                Console.WriteLine("please enter the sender id");
                DataSource.Parcels[DataSource.Config.FirstParcel].ClientSendName = Console.Read();
                Console.WriteLine("please enter the target-customer's id");
                DataSource.Parcels[DataSource.Config.FirstParcel].ClientGetName = Console.Read();
                bool flag = true;
                int temp = 0;
                Console.WriteLine("please enter the weight-category of your parcel:1:Light. 2:Middle. 3:heavy. ");
                while (flag == true)
                {
                    flag = false;
                    temp = Console.Read();
                    switch (temp)
                    {
                        case 1:
                        case 2:
                        case 3:
                            DataSource.Parcels[DataSource.Config.FirstParcel].Weight = (WeightCategories)(temp - 1);
                            break;
                        default:
                            Console.WriteLine("your answer it's invalid. please enter again");
                            flag = true;
                            break;
                    }
                }
                Console.WriteLine("please enter the priority of your parcel:1: Regular. 2:Speed. 3:Emergency.");
                while (flag == false)
                {
                    flag = true;
                    temp = Console.Read();
                    switch (temp)
                    {
                        case 1:
                        case 2:
                        case 3:
                            DataSource.Parcels[DataSource.Config.FirstParcel].Priority = (Priorities)(temp - 1);
                            break;
                        default:
                            Console.WriteLine("your answer it's invalid. please enter again");
                            flag = false;
                            break;
                    }
                }
                Console.WriteLine("are your parcel is on-air?");
                string ans = Console.ReadLine();
                switch (ans)
                {
                    case "yes":
                    case "YES":
                    case "Yes":
                        Console.WriteLine("what is the drone's id?");
                        DataSource.Parcels[DataSource.Config.FirstParcel].DroneId =Console.Read();
                        Console.WriteLine("when did you get the Drone's id?");
                        DataSource.Parcels[DataSource.Config.FirstParcel].MatchForDroneTime = Console.Read();//!
                        Console.WriteLine("did the drone get to the sender?");
                        ans = Console.ReadLine();
                        switch (ans)
                        {
                            case "yes":
                            case "YES":
                            case "Yes":
                                Console.WriteLine("when did it get to the sender?");
                                DataSource.Parcels[DataSource.Config.FirstParcel].MatchForDroneTime = Console.Read();//!
                                Console.WriteLine("did the drone get to the sender?");
                                ans = Console.ReadLine();
                                switch (ans)
                                {
                                    case "yes":
                                    case "YES":
                                    case "Yes":
                                        Console.WriteLine("when did it get to the sender?");
                                        DataSource.Parcels[DataSource.Config.FirstParcel].MatchForDroneTime = Console.Read();//!
                                        //...
                                        break;
                            default:
                                DataSource.Parcels[DataSource.Config.FirstParcel].collectingDroneTime =(0,0,0);//!

                                break;
                        }

                        break;
                    default:
                        DataSource.Parcels[DataSource.Config.FirstParcel].DroneId = 0;
                        break;
                }
                DataSource.Parcels[DataSource.Config.FirstParcel].CreateParcelTime = System.DateTime.Now;


                DataSource.Config.FirstParcel++;

            }
            //העתקה והחזרה
        }
        //        public static BaseStation[] AddingStation()//static?
        //        {
        //            Console.WriteLine("please enter youe station name:");
        //            string name=Console.ReadLine();
        //            Console.WriteLine("please enter youe station id:");
        //            int id = Console.Read();
        //            Console.WriteLine("please enter lenthLIne and WidthLine:");
        //            double lengthLine=(double) Console.Read();
        //            double WidthLine = (double)Console.Read();
        //            DataSource.Station1[DataSource.Config.FirstBaseStation++].IdNumber = id;
        ////=           BaseStation[] s = new BaseStation[10];
        ////            return s;
        //}
    }

        public void ShowCustomer()
        {
            Console.WriteLine("please enter the customer's id");
            int temp = Console.Read();
            bool flag = false;
            foreach(var item in DataSource.Customers)
            {
                if(item.Id==temp)
                {
                    Console.WriteLine($" {item.Name}, {temp}");
                    Console.WriteLine($" phone number: {item.Phone}");
                    Console.WriteLine($" {item.Name}, {temp}");
                    Console.WriteLine($" longitude: {item.Longitude}, latitude: {item.Latitude}");
                    return;
                }
            }
            Console.WriteLine("error! the customer is not exiting in our system");
        }
        public void ShowParcel()
        {
            Console.WriteLine("please enter the parcel's id");
            int temp = Console.Read();
            bool flag = false;
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == temp)
                {
                    //is this suppose to be the function: toString?
                    return;
                }
            }
            Console.WriteLine("error! the parcel is not exiting in our system");
        }
