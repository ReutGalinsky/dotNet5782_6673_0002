using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IDAL.DO;
using IDAL;
using IBL.BO;


namespace BL
{
    partial class BL
    {
        public IDAL.IDal dal;
        public List<IBL.BO.DroneToList> Drones;
        public double availible;
        public double heavy;
        public double light;
        public double medium;
        public double speed;

        BL()
        {
            dal = new DalObject.DalObject();
            double[] arr = dal.UsingElectricity();
            availible = arr[0];
            heavy = arr[1];
            light = arr[2];
            medium = arr[3];
            speed = arr[4];
            foreach(var item in dal.GetDrones())
            {
                
            }
        }

        #region AddDrone
        public void AddDrone(IBL.BO.DroneToList droneToAdd, int number)
        {
            if (droneToAdd.MaxWeight!=IBL.BO.WeightCategories.Heavy && droneToAdd.MaxWeight != IBL.BO.WeightCategories.Middle&& droneToAdd.MaxWeight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (droneToAdd.Model=="")
                throw new AddingProblemException("A model wasn't entered");
            try
            {
                dal.AddDrone((IDAL.DO.Drone)droneToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Drone)));
                Random r= new Random();
                droneToAdd.Battery=r.Next(20,40);
                droneToAdd.State=DroneState.maintaince;
                Location l=(Location)GetBaseStation(number).Local.CopyPropertiesToNew(typeof(Location));
                droneToAdd.Current=l;
                Drones.Add(droneToAdd);
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this drone", ex);
            }
        }
        #endregion

        #region GetDrones
        public IEnumerable<IBL.BO.DroneToList> GetDrones()
        {
            return Drones;
        }
        #endregion

        #region GetDrone
        public IBL.BO.Drone GetDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new GettingProblemException("the drone is not exist");
            IBL.BO.Drone drone = (IBL.BO.Drone)d.CopyPropertiesToNew(typeof(IBL.BO.Drone));
            drone.Current.Latitude = d.Current.Latitude;
            drone.Current.Longitude = d.Current.Longitude;

        }
        #endregion

        #region UpdatingNameOfDrone
        public void UpdatingModelOfDrone(string Model, int id)
        {
            if (Model == "")
                throw new UpdatingException("the model is illegal");
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if(d==null)
                throw new UpdatingException("the drone is not existing");
            d.Model = Model; //לוודא ששינה בBL של רחפנים
            IDAL.DO.Drone updatedDrone = (IDAL.DO.Drone)d.CopyPropertiesToNew(typeof(IDAL.DO.Drone));
            try
            {
                dal.UpdateDrone(updatedDrone);
            }
            catch(Exception e)
            {
                throw new UpdatingException("can't update the drone", e);
            }
            
        }
        #endregion



    }
}
