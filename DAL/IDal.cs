using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    interface IDal
    {
        public void AddDrone(Drone drone);
        public Drone GetDrone(int id);
        public void DeleteDrone(int id);
        public void UpdateDrone(Drone toUpdate);
        public void AddDroneCharge(DroneCharge dronecharge);
        public DroneCharge GetDroneCharge(int id);
        public void DeleteDroneCharge(int id);
        public void UpdateDroneCharge(DroneCharge toUpdate);
        public void AddParcel(Parcel parcel);
        public Parcel GetParcel(int id);
        public void DeleteParcel(int id);
        public void UpdateParcel(Parcel toUpdate);
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<DroneCharge> GetDroneCharges();
        public IEnumerable<Parcel> GetParcels();
        public void AddBaseStation(BaseStation station);
        public BaseStation GetBaseStation(int id);
        public void DeleteBaseStation(int id);
        public void UpdateBaseStation(BaseStation toUpdate); 
        public void AddCustomer(Customer customer);
        public Customer GetCustomer(int id);
        public void DeleteCustomer(int id);
        public void UpdateCustomer(Customer toUpdate);
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<BaseStation> GetBaseStations();

    }
}
