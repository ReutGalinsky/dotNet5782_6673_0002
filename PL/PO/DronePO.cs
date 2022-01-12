using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL.PO
{
    /// <summary>
    /// drone of the pl
    /// </summary>
    public class DronePO : INotifyPropertyChanged
    {
        /// <summary>
        /// the id of the drone
        /// </summary>
        private string _IdNumber;
        /// <summary>
        /// the id of the drone
        /// </summary>

        public string IdNumber
        {
            get { return _IdNumber; }
            set
            {
                _IdNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IdNumber"));
                }
            }
        }
        /// <summary>
        /// the model of the drone
        /// </summary>
        private string _Model;
        /// <summary>
        /// the model of the drone
        /// </summary>
        public string Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                }
            }
        }
        /// <summary>
        /// the weight of the drone
        /// </summary>

        private BO.WeightCategories _MaxWeight;
        /// <summary>
        /// the weight of the drone
        /// </summary>
        public BO.WeightCategories MaxWeight
        {
            get { return _MaxWeight; }
            set
            {
                _MaxWeight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MaxWeight"));
                }
            }
        }
        /// <summary>
        /// the longitude of the drone
        /// </summary>
        private string _Longitude;
        /// <summary>
        /// the longitude of the drone
        /// </summary>

        public string Longitude
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
                }
            }
        }
        /// <summary>
        /// the latitude of the drone
        /// </summary>
        private string _Latitude;
        /// <summary>
        /// the latitude of the drone
        /// </summary>

        public string Latitude
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
                }
            }
        }
        /// <summary>
        /// the battery of the drone
        /// </summary>
        private int _Battery;
        /// <summary>
        /// the battery of the drone
        /// </summary>

        public int Battery
        {
            get { return _Battery; }
            set
            {
                _Battery = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
                }
            }
        }
        /// <summary>
        /// the state of the drone
        /// </summary>
        private BO.DroneState _State;
        /// <summary>
        /// the state of the drone
        /// </summary>

        public BO.DroneState State
        {
            get { return _State; }
            set
            {
                _State = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("State"));
                }
            }
        }
        /// <summary>
        /// the number of the delivered parcel
        /// </summary>
        public string _NumberOfParcel;

        /// <summary>
        /// the number of the delivered parcel
        /// </summary>

        public string NumberOfParcel
        {
            get { return _NumberOfParcel; }
            set
            {
                _NumberOfParcel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NumberOfParcel"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
