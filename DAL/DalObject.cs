using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IDAL.DO;

namespace DalObject
{

    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        //מותר להשתמש בפונקציה של דן?
        public List<BaseStation> AddingBaseStation()
        {
            Console.WriteLine("please enter id number for the new base station");//האם צריך לבדוק שאכן ייחודי?
            BaseStation base1 = new BaseStation();
            base1.IdNumber = Console.Read();
            Console.WriteLine("please enter the name of the station");
            base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            base1.ChargeSlots = Console.Read();
            Console.WriteLine("please enter the location of your base station (longitude,latitude)");
            base1.Longitude = Console.Read();
            base1.Latitude = Console.Read();
            DataSource.stations.Add(base1);
            List<BaseStation> newStations = new List<BaseStation>();
            foreach (var item in DataSource.stations)
            {
                var temp = item.Clone();
                newStations.Add(temp);
            }
            return newStations;
           // return (List<BaseStation>) DataSource.stations.cloneColection<BaseStation>();
        }
        public List<Drone> AddingDrone()
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
            DataSource.Drones.Add(Drone1);
            List<Drone> newDrones = new List<Drone>();
            foreach (var item in DataSource.Drones)
            {
                var temp = item.Clone();
                newDrones.Add(temp);
            }
            return newDrones;
            // return (List<BaseStation>) DataSource.stations.cloneColection<BaseStation>();
        }
        //האם יש דרך לקצר את כל השאלות?
        //לשאול איזה מצב הרחפן או פשוט לרשום זמין?
        //מאיפה ההדפסות: מתוך המחלקה הזו?


        public List<Customer> addingCustomer()
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
            DataSource.Customers.Add(Customer1);
            List<Customer> newCustomers = new List<Customer>();
            foreach (var item in DataSource.Customers)
            {
                var temp = item.Clone();
                newCustomers.Add(temp);
            }
            return newCustomers;


        }
        public List<Parcel> AddingParcel()
        {
            Parcel Parcel1 = new Parcel();
            Parcel1.IdNumber = DataSource.Config.RunningNumber++;
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
            Parcel1.ArrivingDroneTime= new DateTime();
            DataSource.Parcels.Add(Parcel1);
            List<Parcel> newParcels = new List<Parcel>();
            foreach (var item in DataSource.Parcels)
            {
                var temp = item.Clone();
                newParcels.Add(temp);
            }
            return newParcels;
            // return (List<BaseStation>) DataSource.stations.cloneColection<BaseStation>();
        }

       public void ShowCustomers()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach(var item in DataSource.Customers)
            {
                Console.WriteLine("*"+item);
                Console.WriteLine("\n");
            }
        }
        public void ShowDrones()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach (var item in DataSource.Drones)
            {
                Console.WriteLine("*" + item);
                Console.WriteLine("\n");
            }
        }
        public void ShowBaseStations()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach (var item in DataSource.stations)
            {
                Console.WriteLine("*" + item);
                Console.WriteLine("\n");
            }
        }
        public void ShowParcels()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach (var item in DataSource.Parcels)
            {
                Console.WriteLine("*" + item);
                Console.WriteLine("\n");
            }
        }
        public void ShowEmptyBaseStations()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach (var item in DataSource.stations)
            {
                if (!(item.ChargeSlots==0))//איך בודקים אם יש לו עמדות פנויות?
                {
                    Console.WriteLine("*" + item);
                    Console.WriteLine("\n");
                }
            }
        }
        public void ShowNonMatchPacels()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            foreach (var item in DataSource.Parcels)
            {
                if(!(item.MatchForDroneTime.Day==0&&item.MatchForDroneTime.Month==0&&item.MatchForDroneTime.Year==0))//האם יש דרך יותר יפה ומדוייקת?
                
                {
                    Console.WriteLine("*" + item);
                    Console.WriteLine("\n"); 
                }
            }
        }
        //האם טעינה צריכה להחזיר רשימה כל שהיא?
        public void ShowAvailibeStation()//פה הפונקציה שמדפיסה את כל התחנות עם הטענה פנויה.
        {

        }
        public void ParcelToDrone()
        {
            Console.WriteLine("please enter the number of parcel you want to connect with drone");
            int numP = Console.Read();
            Parcel temp1 = new Parcel();
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == numP)
                {
                    temp1 = item;
                    break;
                }
            }

            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            Drone temp2 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp2 = item;
                    break;
                }
            }
           temp2.Status = (DroneStatus.Shipping);
           temp1.CreateParcelTime=DateTime.Now();
           temp1.DroneId=temp2.IdNumber;
        }
        public void ParcelToCollecting()
        {
            Console.WriteLine("please enter the number of parcel you want to collect");
            int numP = Console.Read();
            Parcel temp1 = new Parcel();
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == numP)
                {
                    temp1 = item;
                    break;
                }
            }
            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            Drone temp2 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp2 = item;
                    break;
                }
            }
            temp2.Status = (DroneStatus.Shipping);
            temp1.MatchForDroneTime=DateTime.Now();
           temp1.DroneId=temp2.IdNumber;
        }
        public void ParcelToCustomer()
        {
            Console.WriteLine("please enter the number of parcel you get");
            int numP = Console.Read();
            Parcel temp1 = new Parcel();
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == numP)
                {
                    temp1 = item;
                    break;
                }
            }
            Console.WriteLine("please enter the number of the wanted customer");
            int numD = Console.Read();
            Drone temp2 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp2 = item;
                    break;
                }
            }
            temp2.Status = (DroneStatus.Available);
            DataSource.Parcels.Remove(temp1);
            temp1.collectingDroneTime=DateTime.Now();
           temp1.DroneId=temp2.IdNumber;
        }
        public void SendToCharge()//האם צריך בדיקת תקינות? צריך לייעל יותר!
        {
            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            ShowAvailibeStation();
            Console.WriteLine("please enter the number of the wanted base station from those above");
            int numS = Console.Read();
            Drone temp1=new Drone();
            BaseStation temp2 = new BaseStation();
            foreach (var item in DataSource.Drones)
            {
                if(item.IdNumber==numD)
                {
                    temp1 = item;
                    break;
                }    
            }


            foreach (var item in DataSource.stations)
            {
                if (item.IdNumber == numS)
                {
                    temp2 = item;
                    break;
                }
            }
            temp1.Status = (DroneStatus.Maintenance);
            temp2.ChargeSlots--;
            DroneCharge newCharge = new DroneCharge();
            newCharge.DroneId = numD;
            newCharge.StationId = numS;
            DataSource.Charges.Add(newCharge);
        }
        public void releaseCharge()
        {
            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            DroneCharge temp = new DroneCharge();
            foreach(var item in DataSource.Charges)
            {
                if(item.DroneId==numD)
                {
                    temp = item;
                    break;
                }
            }
            DataSource.Charges.Remove(temp);
            Drone temp1 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp1 = item;
                    break;
                }
            }
            temp1.Status = (DroneStatus.Available);
            BaseStation temp2 = new BaseStation();
            foreach (var item in DataSource.stations)
            {
                if (item.IdNumber == temp.StationId)
                {
                    temp2 = item;
                    break;
                }
            }
            temp2.ChargeSlots++;
        }//לייעל הרבה!
    }

}
