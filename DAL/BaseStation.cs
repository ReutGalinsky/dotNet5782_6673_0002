using System;
using DalObject;
namespace IDAL.DO
{
    public struct BaseStation
    {
        //public string sexagesimalFormat(double d,bool flag)
        //{
        //    string num="";
        //    float temp = 0;
        //    int biggerThanZero = (int)d;
        //    int count = 0;
        //    while(biggerThanZero!=0)
        //    {
        //        temp = ((float)biggerThanZero / 60);
        //        biggerThanZero = (int)(temp);
        //        num = string.Format((int)(((temp - biggerThanZero) * 60))+num);
        //        count++;
        //    }
        //    string final="";
        //    final = final + num+"* ";
        //    temp = (float)(d - (int)d);
        //    temp = temp * 60;
        //    final = final + string.Format((int)temp + "' ");
        //    temp = (temp - (int)temp) * 60;
        //    final = final + string.Format(temp + "'"+"'");
        //    if (flag)
        //    {
        //        if (d < 0) final = final + string.Format(" S ");
        //        else final = final + string.Format(" N ");
        //    }
        //    else
        //    {
        //        if (d>= 0) final = final + string.Format(" E ");
        //        else final = final + string.Format(" W ");
        //    }
        //    return final;


        //}
        public int IdNumber{get;set;}
        public int ChargeSlots { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return string.Format(@$"station number {IdNumber}, name: {Name}
number of charge slots: {ChargeSlots}
Longitude: "+DalObject.Tools.sexagesimalFormat(Longitude,true)+'\n'+"Latitude: "+ DalObject.Tools.sexagesimalFormat(Latitude,false));
        }
    }
}
