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
        internal static BaseStation[] Station1= new BaseStation[5];
        internal static Client[] client1= new Client[100];
        internal static Parcel[] parcel1= new Parcel[1000]
        internal class Config
        {
            internal static int FirstClient=0;
            internal static int FirstParcel=0;
            internal static int FirstBaseStation=0;
            internal static int RunningNumber=0;
            internal static intFirstDrone=0;
        }   
        internal static void Initialize()
        {
            int minimumClient=10,minimumDrone=5,minimumParcel=10,minimumBaseStation=2;

            for(int i=0;i<minimumClient;i++)
            {
                Console.WriteLine("enter your id,name,phone, longitude and latitude");
                client1[i].IdNumber=Console.Read();
                client1[i].name=Console.ReadLine();
                client1[i].PhoneNumber=Console.Read();
                client1[i].Longitude=Console.Read();
                client1[i].Latitude=Console.Read();
                FirstClient++;
            }
            for(int i=0;i<minimumDrone;i++)
            {
                Console.WriteLine("enter your id,model,weight, charging and state drone");
                Drone[i].IdNumber=Console.Read();
                Drone[i].Model=Console.Read();
                Drone[i].Weight=Console.ReadLine();
                Drone[i].Charging=Console.ReadLine();
                Drone[i].State=Console.ReadLine(); //by different station
                FirstDrone++;
            }
            for(int i=0;i<minimumParcel;i++)
            {
                Console.WriteLine("enter details about the parcel");
                parcel1[i].IdNumber=Console.Read();
                parcel1[i].ClientSendName=Console.Readline();
                parcel1[i].ClientGetName=Console.ReadLine();
                parcel1[i].Weight=Console.ReadLine();
                parcel1[i].Priority=Console.ReadLine();
                //מזהה רחפן מבצע )0 אם לא הוקצה( 
                parcel1[i].CreateParcelTime=Console.Read();
                parcel1[i].MatchForDroneTime=Console.Read();
                parcel1[i].collectingDroneTime=Console.Read();
                parcel1[i].ArrivingDroneTime=Console.Read();
                parcel1[i].State=Console.ReadLine(); //by different station
                FirstParcel++;
                RunningNumber+=1;
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
