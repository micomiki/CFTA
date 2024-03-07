using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace CarbonFootprintCalculator
{
    public partial class DataHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the data grid with stored data
                PopulateTransportGrid();
                PopulateElectricityGrid();
            }
        }

        private void PopulateTransportGrid()
        {
            // Retrieve transportation data from session
            List<TransportData> transportData = Session["TransportData"] as List<TransportData>;
            if (transportData != null)
            {
                // Get the tbody element for transport data
                var tbody = transportDataGrid;

                // Clear existing rows
                tbody.Controls.Clear();

                // Iterate through the data and generate table rows
                foreach (var data in transportData)
                {
                    TableRow row = new TableRow();

                    TableCell vehicleTypeCell = new TableCell();
                    vehicleTypeCell.Text = data.VehicleType;
                    row.Cells.Add(vehicleTypeCell);

                    TableCell distanceCell = new TableCell();
                    distanceCell.Text = data.Distance.ToString();
                    row.Cells.Add(distanceCell);

                    TableCell fuelTypeCell = new TableCell();
                    fuelTypeCell.Text = data.FuelType;
                    row.Cells.Add(fuelTypeCell);

                    TableCell fuelEfficiencyCell = new TableCell();
                    fuelEfficiencyCell.Text = data.FuelEfficiency.ToString();
                    row.Cells.Add(fuelEfficiencyCell);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = data.Date;
                    row.Cells.Add(dateCell);

                    TableCell CarbonFootprint = new TableCell();
                    CarbonFootprint.Text = data.CarbonFootprint.ToString();
                    row.Cells.Add(CarbonFootprint);
                    // Add the row to the tbody element
                    tbody.Controls.Add(row);
                }
            }
        }

        private void PopulateElectricityGrid()
        {
            // Retrieve electricity consumption data from session
            List<ElectricityData> electricityData = Session["ElectricityData"] as List<ElectricityData>;
            if (electricityData != null)
            {
                // Get the tbody element for electricity data
                var tbody = electricityDataGrid;

                // Clear existing rows
                tbody.Controls.Clear();

                // Iterate through the data and generate table rows
                foreach (var data in electricityData)
                {
                    TableRow row = new TableRow();

                    TableCell energySourceCell = new TableCell();
                    energySourceCell.Text = data.EnergySource;
                    row.Cells.Add(energySourceCell);

                    TableCell electricityUsageCell = new TableCell();
                    electricityUsageCell.Text = data.ElectricityUsage.ToString();
                    row.Cells.Add(electricityUsageCell);

                    TableCell dateCell = new TableCell();
                    dateCell.Text = data.Date;
                    row.Cells.Add(dateCell);

                    TableCell CarbonFootprint = new TableCell();
                    CarbonFootprint.Text = data.CarbonFootprint.ToString();
                    row.Cells.Add(CarbonFootprint);

                    // Add the row to the tbody element
                    tbody.Controls.Add(row);
                }
            }
        }
    }
}
