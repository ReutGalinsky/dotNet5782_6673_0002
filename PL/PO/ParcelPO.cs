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

        /// <summary>
        /// the id of the parcel
        /// </summary>

        private string _IdNumber;
        /// <summary>
        /// the id of the parcel
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
        /// the id of the sender
        /// </summary>

        private string _sender;
        /// <summary>
        /// the id of the sender
        /// </summary>

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
        /// <summary>
        /// the id of the geter
        /// </summary>

        private string _geter;
        /// <summary>
        /// the id of the geter
        /// </summary>

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
        /// <summary>
        /// the weight of the parcel
        /// </summary>

        private BO.WeightCategories _weight;
        /// <summary>
        /// the weight of the parcel
        /// </summary>

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
        /// <summary>
        /// the state of the parcel
        /// </summary>

        private BO.ParcelState _parcelState;
        /// <summary>
        /// the state of the parcel
        /// </summary>

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
        /// <summary>
        /// the priority of the parcel
        /// </summary>

        private BO.Priorities _priority;
        /// <summary>
        /// the priority of the parcel
        /// </summary>

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
