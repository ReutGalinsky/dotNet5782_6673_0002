using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    class Tools
    {
        //--------------Improted Code (not our)-----------------
        internal static void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
            {
                //allow get out of the text box
                if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                    return;

                //allow list of system keys (add other key here if you want to allow)
                if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                    e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
                 || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                    return;

                char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

                //allow control system keys
                if (Char.IsControl(c)) return;

                //allow digits (without Shift or Alt)
                if (Char.IsDigit(c))
                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                        return; //let this key be written inside the textbox

                //forbid letters and signs (#,$, %, ...)
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            }
        }
        internal static void RemoveCharges(BLApi.IBL bl)
        {
            try
            {
                foreach (var item in bl.GetAllDronesBy(x => x.State == BO.DroneState.maintaince))
                    bl.DroneFromCharging(item.IdNumber);
            }
            catch (Exception)
            { MessageBox.Show("Error in removing the drones from charge"); }

        }

        public static string sexagesimalFormat(double d, bool flag)
        //function that convert the location's field to sexagesimal format
        {
            string num = "";
            float temp = 0;
            int biggerThanZero = (int)d;
            int count = 0;
            while (biggerThanZero != 0)//the values from left of the point
            {
                temp = ((float)biggerThanZero / 60);
                biggerThanZero = (int)(temp);
                num = string.Format((int)(((temp - biggerThanZero) * 60)) + num);
                count++;
            }
            string final = "";
            final = final + num + "* ";
            temp = (float)(d - (int)d);
            temp = temp * 60;
            final = final + string.Format((int)temp + "' ");
            temp = (temp - (int)temp) * 60;
            final = final + string.Format(temp + "'" + "'");//we check only for 2 values from right of the point
            if (flag)
            {
                if (d < 0) final = final + string.Format(" S ");
                else final = final + string.Format(" N ");
            }
            else
            {
                if (d >= 0) final = final + string.Format(" E ");
                else final = final + string.Format(" W ");
            }
            return final;
        }

    }
}
