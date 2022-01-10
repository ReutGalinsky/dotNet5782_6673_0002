using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace PL.PO
{
   public class ParcelPO :INotifyPropertyChanged
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
        private string _sender;
        public string Sender
        {
            get { return _sender; }
            set
            {
                _sender = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
                }
            }
        }
        private string _geter;
        public string Geter
        {
            get { return _geter; }
            set
            {
                _geter = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Geter"));
                }
            }
        }
        private BO.WeightCategories _weight;
        public BO.WeightCategories Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
                }
            }
        }

        private BO.ParcelState _parcelState;
        public BO.ParcelState ParcelState
        {
            get { return _parcelState; }
            set
            {
                _parcelState = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelState"));
                }
            }
        }
        private BO.Priorities _priority;
        public BO.Priorities Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
