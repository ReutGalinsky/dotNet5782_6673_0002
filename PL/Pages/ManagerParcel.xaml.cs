using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Markup;


namespace PL.Pages
{
    public partial class ManagerParcel : Page
    {
        public ManagerParcel(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            ParcelListView.DataContext = listParcels;
            update += updated;
            startDate.Language = XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfo(9).IetfLanguageTag);
            endDate.Language = XmlLanguage.GetLanguage(System.Globalization.CultureInfo.GetCultureInfo(9).IetfLanguageTag);

        }
        private BLApi.IBL bl;
        public EventHandler update;
        private BO.ParcelOfList selected;
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();
        PropertyGroupDescription groupGeter = new PropertyGroupDescription("Geter");
        PropertyGroupDescription groupSender = new PropertyGroupDescription("Sender");
        PropertyGroupDescription groupPriority = new PropertyGroupDescription("Priority");
        PropertyGroupDescription groupState = new PropertyGroupDescription("ParcelState");
        private void updated(object sender, EventArgs e)
        {
            startDate.SelectedDate = null;
            endDate.SelectedDate = null;
            listParcels.Clear();
            foreach (var item in bl.GetParcels())
                listParcels.Add(item);
        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                ManagerViewParcel showParcel = new ManagerViewParcel(bl, selected.IdNumber);
                showParcel.ShowDialog();
                selected = null;
            }
        }
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Parcel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.ParcelOfList ParcelToDelete = ((sender as Button).DataContext) as BO.ParcelOfList;
                    bl.RemoveParcel(ParcelToDelete.IdNumber);
                    update(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n you shuld try again later", "error", MessageBoxButton.OK, MessageBoxImage.Hand);

                }
            }
        }
        private void CancelGroupGeter(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupGeter);
        }
        private void CancelGroupSeter(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupSender);
        }
        private void CancelGroupState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupState);
        }
        private void CancelGroupPriority(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupPriority);
        }
        private void GroupGeter(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupGeter);
        }

        private void GroupSender(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupSender);
        }

        private void GroupState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupState);

        }

        private void GroupPriority(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupPriority);
        }
        private void changeTime(object sender, SelectionChangedEventArgs e)
        {
            if (endDate.SelectedDate == null && startDate.SelectedDate != null)
            {
                var tempList = from item in bl.GetParcels()
                               let parcel = bl.GetParcel(item.IdNumber)
                               where parcel.CreateParcelTime >= startDate.SelectedDate
                               select item;
                listParcels.Clear();
                foreach (var item in tempList)
                    listParcels.Add(item);
                return;
            }
            if (endDate.SelectedDate != null && startDate.SelectedDate == null)
            {
                var tempList = from item in bl.GetParcels()
                               let parcel = bl.GetParcel(item.IdNumber)
                               where parcel.CreateParcelTime <= endDate.SelectedDate
                               select item;
                listParcels.Clear();
                foreach (var item in tempList)
                    listParcels.Add(item);
                return;
            }
            if (endDate.SelectedDate != null && startDate.SelectedDate != null)
            {
                var tempList = from item in bl.GetParcels()
                               let parcel = bl.GetParcel(item.IdNumber)
                               where parcel.CreateParcelTime >= startDate.SelectedDate && parcel.CreateParcelTime <= endDate.SelectedDate
                               select item;
                listParcels.Clear();
                foreach (var item in tempList)
                    listParcels.Add(item);
                return;

            }
        }
    }

}


