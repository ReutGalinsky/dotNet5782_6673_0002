//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;

//namespace PL
//{
//    public class DronePO : INotifyPropertyChanged
//        //PL drone- enable binding
//    {
//        private string _IdNumber; 
//        public string IdNumber
//        {
//            get { return _IdNumber; }
//            set
//            {
//                _IdNumber = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("IdNumber"));
//                }
//            }
//        }
//        private string _Model;
//        public string Model
//        {
//            get { return _Model; }
//            set
//            {
//                _Model = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
//                }
//            }
//        }
//        private BO.WeightCategories _MaxWeight;
//        public BO.WeightCategories MaxWeight
//        {
//            get { return _MaxWeight; }
//            set
//            {
//                _MaxWeight = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("MaxWeight"));
//                }
//            }
//        }
//        private string _Longitude;
//        public string Longitude
//        {
//            get { return _Longitude; }
//            set
//            {
//                _Longitude = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
//                }
//            }
//        }
//        private string _Latitude;
//        public string Latitude
//        {
//            get { return _Latitude; }
//            set
//            {
//                _Latitude = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
//                }
//            }
//        }
//        private int _Battery;
//        public int Battery
//        {
//            get { return _Battery; }
//            set
//            {
//                _Battery = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
//                }
//            }
//        }
//        private BO.DroneState _State;
//        public BO.DroneState State
//        {
//            get { return _State; }
//            set
//            {
//                _State = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("State"));
//                }
//            }
//        }
//        public string _NumberOfParcel;

//        public string NumberOfParcel
//        {
//            get { return _NumberOfParcel; }
//            set
//            {
//                _NumberOfParcel = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged(this, new PropertyChangedEventArgs("NumberOfParcel"));
//                }
//            }
//        }
//        public event PropertyChangedEventHandler PropertyChanged;}
//}
