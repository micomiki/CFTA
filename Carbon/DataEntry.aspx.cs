using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CarbonFootprintCalculator
{
    public partial class DataEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the current date for date inputs
                txtDateTransport.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                txtDateElectricity.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
        }

        protected void btnSubmitTransport_Click(object sender, EventArgs e)
        {
            // Calculate carbon footprint for transportation
            double distance = Convert.ToDouble(txtDistance.Text);
            double fuelEfficiency = Convert.ToDouble(txtFuelEfficiency.Text);
            double carbonFootprint = CalculateTransportCarbonFootprint(distance, fuelEfficiency, ddlFuelType.SelectedValue);

            // Store the data temporarily
            StoreTransportData(ddlVehicleType.SelectedValue, distance, ddlFuelType.SelectedValue, fuelEfficiency, txtDateTransport.Text, carbonFootprint);

            // Clear the form after submission
            ClearTransportForm();
        }

        protected void btnSubmitElectricity_Click(object sender, EventArgs e)
        {
            // Calculate carbon footprint for electricity consumption
            double electricityUsage = Convert.ToDouble(txtElectricityUsage.Text);
            double carbonFootprint = CalculateElectricityCarbonFootprint(electricityUsage, ddlEnergySource.SelectedValue);

            // Store the data temporarily
            StoreElectricityData(ddlEnergySource.SelectedValue, electricityUsage, txtDateElectricity.Text, carbonFootprint);

            // Clear the form after submission
            ClearElectricityForm();
        }

        private double CalculateTransportCarbonFootprint(double distance, double fuelEfficiency, string fuelType)
        {
            // Define fuel type emission factors
            Dictionary<string, double> emissionFactors = new Dictionary<string, double>()
            {
                { "Gasoline", 2.3 },
                { "Diesel", 2.7 },
                { "Petrol", 1.5 },
                { "Electric", 0 } // No emissions for electric vehicles
            };

            // Calculate carbon footprint based on the selected fuel type
            double emissionFactor = emissionFactors[fuelType];
            return (distance * fuelEfficiency) / emissionFactor;
        }

        private double CalculateElectricityCarbonFootprint(double electricityUsage, string energySource)
        {
            // Define energy source carbon intensity factors
            Dictionary<string, double> carbonIntensityFactors = new Dictionary<string, double>()
            {
                { "Grid", 0.5 },
                { "Wind", 0.01 },
                { "Solar", 0.05 }
            };

            // Calculate carbon footprint based on the selected energy source
            double carbonIntensity = carbonIntensityFactors[energySource];
            return electricityUsage * carbonIntensity;
        }

        private void StoreTransportData(string vehicleType, double distance, string fuelType, double fuelEfficiency, string date, double carbonFootprint)
        {
            // Store the transportation data in a temporary storage (e.g., session variable or database table)
            List<TransportData> transportData = GetTransportDataFromSession();
            transportData.Add(new TransportData(vehicleType, distance, fuelType, fuelEfficiency, date, carbonFootprint));
            Session["TransportData"] = transportData;
        }

        private void StoreElectricityData(string energySource, double electricityUsage, string date, double carbonFootprint)
        {
            // Store the electricity consumption data in a temporary storage (e.g., session variable or database table)
            List<ElectricityData> electricityData = GetElectricityDataFromSession();
            electricityData.Add(new ElectricityData(energySource, electricityUsage, date, carbonFootprint));
            Session["ElectricityData"] = electricityData;
        }

        private void ClearTransportForm()
        {
            // Clear the input fields for transportation data
            txtDistance.Text = string.Empty;
            txtFuelEfficiency.Text = string.Empty;
        }

        private void ClearElectricityForm()
        {
            // Clear the input fields for electricity consumption data
            txtElectricityUsage.Text = string.Empty;
        }

        private List<TransportData> GetTransportDataFromSession()
        {
            // Retrieve transportation data from session variable or return a new list if not found
            return Session["TransportData"] as List<TransportData> ?? new List<TransportData>();
        }

        private List<ElectricityData> GetElectricityDataFromSession()
        {
            // Retrieve electricity consumption data from session variable or return a new list if not found
            return Session["ElectricityData"] as List<ElectricityData> ?? new List<ElectricityData>();
        }
    }

    // Class to represent transportation data model
    public class TransportData
    {
        public string VehicleType { get; set; }
        public double Distance { get; set; }
        public string FuelType { get; set; }
        public double FuelEfficiency { get; set; }
        public string Date { get; set; }
        public double CarbonFootprint { get; set; }

        public TransportData(string vehicleType, double distance, string fuelType, double fuelEfficiency, string date, double carbonFootprint)
        {
            VehicleType = vehicleType;
            Distance = distance;
            FuelType = fuelType;
            FuelEfficiency = fuelEfficiency;
            Date = date;
            CarbonFootprint = carbonFootprint;
        }
    }

    // Class to represent electricity consumption data model
    public class ElectricityData
    {
        public string EnergySource { get; set; }
        public double ElectricityUsage { get; set; }
        public string Date { get; set; }
        public double CarbonFootprint { get; set; }

        public ElectricityData(string energySource, double electricityUsage, string date, double carbonFootprint)
        {
            EnergySource = energySource;
            ElectricityUsage = electricityUsage;
            Date = date;
            CarbonFootprint = carbonFootprint;
        }
    }
}
