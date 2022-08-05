using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using PL.PO;
using BO;
using System.Windows;

namespace PL.Convert
{
 /// <summary>
 /// converter: if the given field is null, the tool won't be enable
 /// </summary>
    public class ConvertParcelToEnable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// converter: if the given field is null - the tool won't be visible
    /// </summary>
    public class ConvertToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// converter: if the given state is 'Define' -the tool will be enable
    /// </summary>
    public class ConvertStateToEnable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var state =(BO.ParcelState) (value);
            if (state ==  ParcelState.Define)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// converter: if the field is null, the text will be "coming soon" and not the value
    /// </summary>
    public class ConvertNotArriving : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Comming Soon";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// converter: if there is parcel on the drone-the tool will be visible
    /// </summary>
    public class ConvertParcelDrone : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parcel = value as BO.Parcel;
            if(parcel==null)
                return Visibility.Collapsed;
            if (parcel.Drone!=null&&parcel.ArrivingDroneTime==null)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// converter: if there isn't a drone- the tool will be visible
    /// </summary>
    public class ConvertTextDrone : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parcel = value as BO.Parcel;
            if(parcel==null)
                return Visibility.Collapsed;
            if (parcel.Drone == null || parcel.ArrivingDroneTime != null)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// converter: if the field is null, the tool will be visible
    /// </summary>
    public class ConveryDisVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}