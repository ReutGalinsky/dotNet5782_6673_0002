using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL.PO
{
   public class BaseStationPO: INotifyPropertyChanged
    {

        /// <summary>
        /// the id of the station
        /// </summary>
        private string _IdNumber;
        /// <summary>
        /// the id of the station
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
        /// the name of the station
        /// </summary>

        private string _Name;
        /// <summary>
        /// the id of the station
        /// </summary>

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        /// <summary>
        /// the amount of availible charge slots of the station
        /// </summary>

        private int _ChargeSlots;
        /// <summary>
        /// the amount of availible charge slots of the station
        /// </summary>

        public int ChargeSlots
        {
            get { return _ChargeSlots; }
            set
            {
                _ChargeSlots = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ChargeSlots"));
                }
            }
        }
        /// <summary>
        /// the longitude of the station
        /// </summary>
        private string _Longitude;
        /// <summary>
        /// the longitude of the station
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
        /// the latitude of the station
        /// </summary>

        private string _Latitude;
        /// <summary>
        /// the latitude of the station
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
