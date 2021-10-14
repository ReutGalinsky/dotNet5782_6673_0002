using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    class DataSource
    {
        internal static BaseStation[] Stations= new BaseStation[5];
        internal static Drone[] Drones = new Drone[10];
        internal static Customer[] Customers= new Customer[100];
        internal static Parcel[] Parcels = new Parcel[1000];
        internal class Config
        {
            internal static int FirstCustomer=0;
            internal static int FirstParcel=0;
            internal static int FirstDrone = 0;
            internal static int FirstBaseStation=0;

            internal static int RunningNumber=0;// האם מתחיל מאפס?
        }   
        internal static void Initialize()///////////////////////////////////////////
        {
            int minimumClient=10,minimumDrone=5,minimumParcel=10,minimumBaseStation=2;

            for(int i=0;i<minimumClient;i++)
            {
                Console.WriteLine("enter your id,name,phone, longitude and latitude");
                Customers[i].Id=Console.Read();
                Customers[i].Name=Console.ReadLine();
                Customers[i].Phone=Console.ReadLine();
                Customers[i].Longitude=Console.Read();
                Customers[i].Latitude=Console.Read();
                Config.FirstCustomer++;
            }
            for(int i=0;i<minimumDrone;i++)
            {
                Console.WriteLine("enter your id,model,weight, charging and state drone");
                Drones[i].IdNumber=Console.Read();
                Drones[i].Model=Console.ReadLine();
                Drones[i].MaxWeight=(WeightCategories)Console.Read();//does it's ok the implicit?
                Drones[i].Battery=Console.Read();
                Drones[i].Status=(DroneStatus)Console.Read(); //by different station
                Config.FirstDrone++;
            }
            for(int i=0;i<minimumParcel;i++)
            {
                Console.WriteLine("enter details about the parcel");
                Parcels[i].IdNumber=Console.Read();
                Parcels[i].ClientSendName=Console.Read();
                Parcels[i].ClientGetName=Console.Read();
                Parcels[i].Weight= (WeightCategories)Console.Read();
                Parcels[i].Priority= (Priorities)Console.Read();
                //מזהה רחפן מבצע )0 אם לא הוקצה(
                //----------------------------צריך לקרוא את כל הזמנים!:---------------------------------------
                //string temp;
                //temp = Console.ReadLine();
                //Parcels[i].CreateParcelTime;
                //Parcels[i].CreateParcelTime=(System.DateTime)Console.();
                //Parcels[i].MatchForDroneTime=Console.Read();
                //Parcels[i].collectingDroneTime=Console.Read();
                //Parcels[i].ArrivingDroneTime=Console.Read();
                Parcels[i].DroneId=Console.Read(); //by different station
                Config.FirstParcel++;
                Config.RunningNumber+=1;
            }


            Random rand = new Random();
            for(int i=0;i<2;i++)
            {
             // example // Rachs[i].charge = rand.Next(0, 101);
             //   Rachs[i].Id = rand.Next()//??;

            }
        }

    }
}
