﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDal
    {
        public double[] UsingElectricity();
        public void AddDrone(Drone drone);
        public Drone GetDrone(string id);
        public void DeleteDrone(string id);
        public void UpdateDrone(Drone toUpdate);
        public void AddDroneCharge(DroneCharge dronecharge);
        public DroneCharge GetDroneCharge(string id);
        public void DeleteDroneCharge(string id);
        public void UpdateDroneCharge(DroneCharge toUpdate);
        public string AddParcel(Parcel parcel);
        public Parcel GetParcel(string id);
        public void DeleteParcel(string id);
        public void UpdateParcel(Parcel toUpdate);
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<DroneCharge> GetDroneCharges();
        public IEnumerable<Parcel> GetParcels();
        public void AddBaseStation(BaseStation station);
        public BaseStation GetBaseStation(string id);
        public void DeleteBaseStation(string id);
        public void UpdateBaseStation(BaseStation toUpdate);
        public void AddCustomer(Customer customer);
        public Customer GetCustomer(string id);
        public void DeleteCustomer(string id);
        public void UpdateCustomer(Customer toUpdate);
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<BaseStation> GetBaseStations();
        public IEnumerable<Drone> PredicateDrone(Predicate<Drone> condition);
        public IEnumerable<DroneCharge> PredicateChargeDrone(Predicate<DroneCharge> condition);
        public IEnumerable<Customer> PredicateCustomer(Predicate<Customer> condition);
        public IEnumerable<Parcel> PredicateParcel(Predicate<Parcel> condition);
        public IEnumerable<BaseStation> PredicateBaseStation(Predicate<BaseStation> condition);
    }
}