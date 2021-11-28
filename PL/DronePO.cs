using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL
{
    public class DronePO : INotifyPropertyChanged
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
        private string _Model;
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
        private IBL.BO.WeightCategories _MaxWeight;
        public IBL.BO.WeightCategories MaxWeight
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
        private int _Battery;
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
        private IBL.BO.DroneState _State;
        public IBL.BO.DroneState State
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
        public string _NumberOfParcel;

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
        public event PropertyChangedEventHandler PropertyChanged;}
}
