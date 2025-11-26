using FitSammenDekstopClient.BusinessLogicLayer;
using FitSammenDekstopClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitSammenDekstopClient
{
    public partial class CreateClassForm : Form
    {
        private IEnumerable<Location> _locations = new List<Location>();

        private IEnumerable<Room> _roomsForCurrentLocation = new List<Room>();

        private IEnumerable<Employee> _employees = new List<Employee>();

        private readonly LocationLogic _locationLogic;

        private readonly ClassLogic _classLogic;

        public CreateClassForm(LocationLogic locationLogic, ClassLogic classLogic)
        {
            _locationLogic = locationLogic;
            _classLogic = classLogic;
            InitializeComponent();
            SetupStartTimeCombo();
            SetupClassTypeCombo();
            SetupLocationCombo();
        }

        private async void btnCreateClass_Click(object sender, EventArgs e)
        {
            //Valider input
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Navn på hold skal udfyldes.");
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                MessageBox.Show("Beskrivelse skal udfyldes.");
                textBoxDescription.Focus();
                return;
            }

            if (comboBoxEmployee.SelectedItem is not Employee instructor)
            {
                MessageBox.Show("Vælg en instruktør.");
                comboBoxEmployee.Focus();
                return;
            }

            if (comboBoxRoom.SelectedItem is not Room room)
            {
                MessageBox.Show("Vælg et lokale.");
                comboBoxRoom.Focus();
                return;
            }

            if (comboBoxClassType.SelectedItem is not ClassType classType)
            {
                MessageBox.Show("Vælg en holdtype.");
                comboBoxClassType.Focus();
                return;
            }

            if (comboBoxStartTime.SelectedItem is not TimeOnly startTime)
            {
                MessageBox.Show("Vælg et starttidspunkt.");
                comboBoxStartTime.Focus();
                return;
            }

            if ((int)UpDownCapacity.Value == 0)
            {
                MessageBox.Show("Kapacitet skal være større end 0.");
                UpDownCapacity.Focus();
                return;
            }

            if ((int)UpDownDuration.Value == 0)
            {
                MessageBox.Show("Varighed skal være større end 0 minutter.");
                UpDownDuration.Focus();
                return;
            }

            string name = textBoxName.Text.Trim();
            string description = textBoxDescription.Text.Trim();

            int capacity = (int)UpDownCapacity.Value;
            int duration = (int)UpDownDuration.Value;

            DateOnly trainingDate = DateOnly.FromDateTime(dateTimePickerTrainingDate.Value.Date);


            var request = new CreateClassRequest
            {
                TrainingDate = trainingDate,
                Instructor = instructor,
                Description = description,
                Room = room,
                Name = name,
                Capacity = capacity,
                DurationInMinutes = duration,
                StartTime = startTime,
                ClassType = classType
            };

            CreateClassResponse? Response = await _classLogic.CreateClassAsync(request);

            if (Response == null || Response.Status == CreateClassStatus.Error)
            {
                MessageBox.Show("Holdet kunne ikke oprettes. Prøv igen senere.");
                return;
            }
            else if (Response.Status == CreateClassStatus.Success)
            {
                MessageBox.Show($"Holdet er oprettet. {Response.Message}");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetupStartTimeCombo()
        {
            var times = new List<TimeOnly>();
            var start = new TimeOnly(6, 0);
            var end = new TimeOnly(22, 0);

            var current = start;
            while (current <= end)
            {
                times.Add(current);
                current = current.AddMinutes(60);
            }

            comboBoxStartTime.DataSource = times;
            comboBoxStartTime.Format += (s, e) =>
            {
                if (e.ListItem is TimeOnly t)
                {
                    e.Value = t.ToString("HH\\:mm");
                }
            };
            comboBoxStartTime.SelectedIndex = -1;
        }
        private void SetupClassTypeCombo()
        {
            comboBoxClassType.DataSource = Enum.GetValues(typeof(ClassType));
            comboBoxClassType.SelectedIndex = -1;
        }

        private async void SetupLocationCombo()
        {
            _locations = await _locationLogic.GetAllLocationsAsync();

            comboBoxLocation.DataSource = _locations;
            comboBoxLocation.Format += (s, e) =>
            {
                if (e.ListItem is Location loc)
                {
                    e.Value = $"{loc.StreetName} {loc.HouseNumber}, {loc.Zipcode.ZipcodeNumber} {loc.Zipcode.City.CityName}";
                }
            };
            comboBoxLocation.ValueMember = "LocationId";

            comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;

            comboBoxLocation.SelectedIndex = -1;
        }

        private void comboBoxLocation_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadRoomsForSelectedLocation();
            LoadEmployeesForSelectedLocation();
        }

        private async void LoadRoomsForSelectedLocation()
        {
            var selectedLocation = comboBoxLocation.SelectedItem as Location;
            if (selectedLocation == null)
                return;

            _roomsForCurrentLocation = await _locationLogic.GetAllRoomsFromLocationIdAsync(selectedLocation.LocationId);

            comboBoxRoom.DataSource = _roomsForCurrentLocation;
            comboBoxRoom.DisplayMember = "RoomName";
            comboBoxRoom.ValueMember = "RoomId";
        }

        private async void LoadEmployeesForSelectedLocation()
        {
            var selectedLocation = comboBoxLocation.SelectedItem as Location;
            if (selectedLocation == null)
                return;

            _employees = await _locationLogic.GetAllEmployeesFromLocationIdAsync(selectedLocation.LocationId);

            comboBoxEmployee.DataSource = _employees;
            comboBoxEmployee.DisplayMember = "FullName";
            comboBoxEmployee.ValueMember = "User_ID";
        }
    }
}
