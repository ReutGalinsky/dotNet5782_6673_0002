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

        private string _IdNumber;
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
        private string _Name;
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
        private int _ChargeSlots;
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
        private string _Longitude;
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
        private string _Latitude;
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
