using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
   public interface IBL
    {
        public void AddBaseStation(BaseStation baseStationToAdd);
        public IEnumerable<BaseStationToList> GetBseStations();
        public IEnumerable<BaseStationToList> GetBaseStationsWithCharge();
        public void UpdatingDetailsOfBaseStation(string id, string Name, int numberOfCharge);
        public BaseStation GetBaseStation(string id);
        public void DroneToCharging(string number);
        public void DroneFromCharging(string number, TimeSpan charging);
        public void AddDrone(DroneToList droneToAdd, string number);
        public IEnumerable<DroneToList> GetDrones();
        public Drone GetDrone(string id);
        public void UpdatingDetailsOfDrone(string Model, string id);
        public void AddCustomer(Customer customerToAdd);
        public void AddParcelToDelivery(ParcelOfList parcel);
        public void UpdatingDetailsOfCustomer(string id, string Name, string phone);
        public IEnumerable<CustomerToList> GetCustomers();
        public Customer GetCustomer(string id);
        public void MatchingParcelToDrone(string id);
        public void PickingParcelByDrone(string id);
        public void SupplyingParcelByDrone(string id);
        public Parcel GetParcel(string id);
        public IEnumerable<ParcelOfList> GetParcels();
        public IEnumerable<ParcelOfList> GetParcelsNotMatching();















    }
}
