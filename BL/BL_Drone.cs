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
            //try
            //{
            //    dal.GetBaseStation(droneToAdd.s)
            //}
            if (droneToAdd.Model=="")
                throw new AddingProblemException("A model wasn't entered");
            try
            {
                IDAL.DO.Drone d=new IDAL.DO.Drone() { IdNumber=droneToAdd.IdNumber, Model=droneToAdd.Model,MaxWeight=(IDAL.DO.WeightCategories)droneToAdd.MaxWeight};
                dal.AddDrone(d);
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
        public IBL.BO.DroneToList GetDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new GettingProblemException("the drone is not exist");
            return d;
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
            d.Model = Model;
            IDAL.DO.Drone updatedDrone = new IDAL.DO.Drone() { IdNumber=d.IdNumber,MaxWeight=(IDAL.DO.WeightCategories)d.MaxWeight,Model=d.Model};
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
