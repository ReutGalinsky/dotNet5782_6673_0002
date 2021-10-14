using System;

namespace IDAL.DO
{

    public class PDS
    {
    }
    public struct BaseStation
    {
        public int IdNumber{get;set;}
        public override string ToString()
        {
            return "BaseStation";
        }
    }
     public struct Drone
    {
        public int IdNumber{get;set;}
        public string StationName{get;set;}
        public int StationChargeNumber{get;set;}
        public int Longitude{get;set;}
        public int Latitude{get;set;}
        public override string ToString()
        {
            return "Drone";
        }
    }
      public struct Parcel
    {
        public int IdNumber{get;set;}
        public string ClientSendName{get;set;}
        public string ClientGetName{get;set;}
        public int StationChargeNumber{get;set;}
        public int Weight{get;set;}
        public int Priority{get;set;}
        //o מזהה רחפן מבצע )0 אם לא הוקצה( 
        public int CreateParcelTime{get;set;}
        public int MatchForDroneTime{get;set;}
        public int collectingDroneTime{get;set;}
        public int ArrivingDroneTime{get;set;}


        public override string ToString()
        {
            return "Parcel";
        }
    }

}
