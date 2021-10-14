using System;
namespace IDAL.DO
{

    public class PDS
    {
    }
    public struct BaseStation
    {
        public int IdNumber{get;set;}
        public int ChargeSlots { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return "BaseStation";
        }
    }
    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return string.Format("Customer");///האם צריך גם את המילה פורמט?
        }

    }
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public override string ToString()
        {
            return string.Format("DroneCharge");///האם צריך גם את המילה פורמט?
        }

    }

    public struct Drone
    {
        public int IdNumber{get;set;}
        public string Model{get;set;}
        public WeightCategories MaxWeight{get;set;}
        public double Battery{get;set;}
        public DroneStatus Status{get;set;}
        public override string ToString()
        {
            return "Drone";
        }
    }
      public struct Parcel
    {
        public int IdNumber{get;set;}
        public int ClientSendName{get;set;}
        public int ClientGetName{get;set;}
        public int DroneId{get;set; }        //o מזהה רחפן מבצע )0 אם לא הוקצה( 
        public WeightCategories Weight {get;set;}
        public Priorities Priority {get;set;}
        public System.DateTime CreateParcelTime{get;set;}
        public System.DateTime MatchForDroneTime {get;set;}
        public System.DateTime collectingDroneTime {get;set;}
        public System.DateTime ArrivingDroneTime {get;set;}


        public override string ToString()
        {
            return "Parcel";
        }
    }

}
