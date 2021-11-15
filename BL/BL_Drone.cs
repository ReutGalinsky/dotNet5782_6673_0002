using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IDAL.DO;
using IBL.BO;


namespace BL
{
    class BL
    {
        public  void AddDrone(IBL.BO.Drone droneToAdd)
        {
            if (droneToAdd.MaxWeight!=IBL.BO.WeightCategories.Heavy && droneToAdd.MaxWeight != IBL.BO.WeightCategories.Middle&& droneToAdd.MaxWeight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            
            if (IDAL.DO. != 8 && busToAdd.LicensingDate.Year >= 2018
                || busToAdd.LicenseNumber.Length != 7 && busToAdd.LicensingDate.Year < 2018)
                throw new AddingProblemException("The number of digits in the license number is not valid");
            int tryParse;
            if (!int.TryParse(busToAdd.LicenseNumber, out tryParse))
                throw new AddingProblemException("The License number must be only with digits");
            if (tryParse < 0)
                throw new AddingProblemException("The License number must be a positive number");
            if (busToAdd.FuelTank < 0)
                throw new AddingProblemException("The amount of kilometers left until there is no fuel must be a positive number ");
            if (busToAdd.FuelTank > 1200)
                throw new AddingProblemException("The amount of kilometers left until there is no fuel can't be over 1200 km");
            if (busToAdd.Mileage < 0)
                throw new AddingProblemException("The mileage must be a positive number");
            try
            {
                IDAL.DO.AddDrone((IDAL.DO.BaseStation)droneToAdd.CopyPropertiesToNew(typeof(DO.Bus)));
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this bus", ex);
            }
        }
#endregion

    }
}
