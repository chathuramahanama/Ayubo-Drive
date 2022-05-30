using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AyuboDriveApplication
{
    public class Operation
    {

        public int calculateVehicleRent(string VehicleNumber, DateTime rentedDate, DateTime returnDate, bool withDriver)
        {

            int totalRent = 0;
            int noOfDays = Convert.ToInt32((returnDate - rentedDate).TotalDays);

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-TDBL4OP;Initial Catalog=AyuboDriveApplication;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from VEHICLE_DEATILS where VehicleNumber = '" + VehicleNumber + "'";

            SqlDataReader sdr = cmd.ExecuteReader();

            int monthlyRent = 0;
            int weeklyRent = 0;
            int dailyRent = 0;
            int dailyDriverCost = 0;

            while (sdr.Read())
            {
                monthlyRent = Convert.ToInt32(sdr["MonthlyRent"].ToString());
                weeklyRent = Convert.ToInt32(sdr["WeeklyRent"].ToString());
                dailyRent = Convert.ToInt32(sdr["DailyRent"].ToString());
                dailyDriverCost = Convert.ToInt32(sdr["DriverCost"].ToString());
            }

            con.Close();

            int months = noOfDays / 30;
            int weeks = 0;
            int days = 0;

            if ((noOfDays % 30) > 0)
            {
                int restDays = noOfDays % 30;
                weeks = restDays / 7;

                if ((restDays % 7) > 0)
                {
                    days = restDays % 7;
                }
            }

            if (months == 0)
            {
                weeks = noOfDays / 7;
                days = noOfDays % 7;
            }

            if (withDriver == false)
            {
                dailyDriverCost = 0;
            }

            totalRent = (monthlyRent * months) + (weeklyRent * weeks) + (dailyRent * days) + (dailyDriverCost * noOfDays);

            return totalRent;
        }

        public int[] calculateDayTourHire(string vehicleNo, string pkgType, DateTime startTime, DateTime endTime, int startKMreading, int endKMreading)
        {

            int[] allCharges = new int[4];

            int enteredNoOfHours = Convert.ToInt32((endTime - startTime).Hours);
            int noOfKMs = endKMreading - startKMreading;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-TDBL4OP;Initial Catalog=AyuboDriveApplication;Integrated Security=True");
            con.Open();

            SqlCommand cmd1 = new SqlCommand("select VehicleID from VEHICLE_DEATILS where VehicleNumber = '" + vehicleNo + "'", con);
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            sda1.Fill(dt1);

            int VehicleTypeID = 0;

            foreach (DataRow dr in dt1.Rows)
            {
                VehicleTypeID = Convert.ToInt32(dr["VehicleID"].ToString());
            }

            SqlCommand cmd2 = new SqlCommand("select ID from PACKAGE_TYPE_DROP_AND_PICKUP where PackageType = '" + pkgType + "'", con);
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            sda2.Fill(dt2);

            int PackageTypeID = 0;

            foreach (DataRow dr in dt2.Rows)
            {
                PackageTypeID = Convert.ToInt32(dr["ID"].ToString());
            }

            SqlCommand cmd3 = new SqlCommand("select * from DAY_TOUR where VehicleID = '" + VehicleTypeID + "' and PackageID = '" + PackageTypeID + "'", con);
            cmd3.ExecuteNonQuery();

            DataTable dt3 = new DataTable();
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            sda3.Fill(dt3);

            int packagePrice = 0;
            int maxKMLimit = 0;
            int extraKMRate = 0;
            int maxNoOfHours = 0;
            int extraHourRate = 0;

            foreach (DataRow dr in dt3.Rows)
            {
                packagePrice = Convert.ToInt32(dr["PackagePrice"].ToString());
                maxKMLimit = Convert.ToInt32(dr["KMLimiteMAX"].ToString());
                extraKMRate = Convert.ToInt32(dr["ExtraKMRate"].ToString());
                maxNoOfHours = Convert.ToInt32(dr["MaxNumOfHours"].ToString());
                extraHourRate = Convert.ToInt32(dr["ExtraHourRate"].ToString());
            }

            con.Close();

            int noOfHours = 0;
            int extraHours = 0;

            if (enteredNoOfHours > maxNoOfHours)
            {
                noOfHours = enteredNoOfHours;
                extraHours = noOfHours - maxNoOfHours;
            }

            int extraKMs = 0;

            if (noOfKMs > maxKMLimit)
            {
                extraKMs = noOfKMs - maxKMLimit;
            }

            int baseHireCharge = packagePrice;
            int waitingCharge = extraHourRate * extraHours;
            int extraKMCharge = extraKMRate * extraKMs;
            int totalHireValue = baseHireCharge + waitingCharge + extraKMCharge;

            allCharges[0] = baseHireCharge;
            allCharges[1] = waitingCharge;
            allCharges[2] = extraKMCharge;
            allCharges[3] = totalHireValue;

            return allCharges;
        }
        public int[] calculateLongTourHire(string vehicleNo, string pkgType, DateTime startDate, DateTime endDate, int startKMreading, int endKMreading)
        {

            int[] allCharges = new int[4];

            int NoOfDays = Convert.ToInt32((endDate - startDate).TotalDays);
            int noOfKMs = endKMreading - startKMreading;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-TDBL4OP;Initial Catalog=AyuboDriveApplication;Integrated Security=True");
            con.Open();

            SqlCommand cmd1 = new SqlCommand("select VehicleID from VEHICLE_DEATILS where VehicleNumber = '" + vehicleNo + "'", con);
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            sda1.Fill(dt1);

            int VehicleTypeID = 0;

            foreach (DataRow dr in dt1.Rows)
            {
                VehicleTypeID = Convert.ToInt32(dr["VehicleID"].ToString());
            }

            SqlCommand cmd2 = new SqlCommand("select ID from PACKAGE_TYPE_KM where PackageType = '" + pkgType + "'", con);
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            sda2.Fill(dt2);

            int PackageTypeID = 0;

            foreach (DataRow dr in dt2.Rows)
            {
                PackageTypeID = Convert.ToInt32(dr["ID"].ToString());
            }

            SqlCommand cmd3 = new SqlCommand("select * from LONG_TOUR where VehicleID = '" + VehicleTypeID + "' and PackageID = '" + PackageTypeID + "'", con);
            cmd3.ExecuteNonQuery();

            DataTable dt3 = new DataTable();
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            sda3.Fill(dt3);

            int packagePrice = 0;
            int maxKMLimit = 0;
            int extraKMRate = 0;
            int driverOvernightRate = 0;
            int vehicleNightParkRate = 0;

            foreach (DataRow dr in dt3.Rows)
            {
                packagePrice = Convert.ToInt32(dr["PackagePrice"].ToString());
                maxKMLimit = Convert.ToInt32(dr["KMLimitMAX"].ToString());
                extraKMRate = Convert.ToInt32(dr["ExtraKMRate"].ToString());
                driverOvernightRate = Convert.ToInt32(dr["DriverOvernightRate"].ToString());
                vehicleNightParkRate = Convert.ToInt32(dr["VehicleNightParkRate"].ToString());
            }

            con.Close();

            int noOfNights = 0;

            if (NoOfDays >= 2)
            {
                noOfNights = NoOfDays - 1;
            }

            int extraKMs = 0;

            if (noOfKMs > maxKMLimit)
            {
                extraKMs = noOfKMs - maxKMLimit;
            }

            int baseHireCharge = packagePrice;
            int overnightStayCharge = (driverOvernightRate * noOfNights) + (vehicleNightParkRate * noOfNights);
            int extraKMCharge = extraKMRate * extraKMs;
            int totalHireValue = baseHireCharge + overnightStayCharge + extraKMCharge;

            allCharges[0] = baseHireCharge;
            allCharges[1] = overnightStayCharge;
            allCharges[2] = extraKMCharge;
            allCharges[3] = totalHireValue;

            return allCharges;
        }

    }
}
