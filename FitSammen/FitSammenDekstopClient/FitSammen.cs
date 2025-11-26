using FitSammenDekstopClient.BusinessLogicLayer;
using FitSammenDekstopClient.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FitSammenDekstopClient
{
    public partial class FitSammen : Form
    {
        private readonly ClassLogic _classLogic;
        private readonly LocationLogic _locationLogic;

        public FitSammen(ClassLogic classLogic, LocationLogic locationLogic)
        {
            _classLogic = classLogic;
            _locationLogic = locationLogic;
            InitializeComponent();
            SetupClassListView();
            GetAllClasses(this, EventArgs.Empty);
        }

        private async void GetAllClasses(object sender, EventArgs e)
        {
            IEnumerable<Class>? classes = await _classLogic.GetAllClassesAsync();

            if (classes != null)
            {
                listViewAllClasses.Items.Clear();

                foreach (Class c in classes)
                {
                    ListViewItem item = new ListViewItem(c.Name);

                    item.SubItems.Add(c.MemberCount.ToString());
                    item.SubItems.Add(c.Capacity.ToString());
                    item.SubItems.Add(c.StartTime.ToString("HH\\:mm"));
                    item.SubItems.Add(c.DurationInMinutes.ToString() + " Minutter");
                    item.SubItems.Add(c.Room.Location.Zipcode.City.CityName);

                    item.Tag = c;

                    listViewAllClasses.Items.Add(item);
                    string processText = "Virkede";
                    labelProcessText.Text = processText;

                }
            }
            else
            {
                string processText = "Ingen træningshold fundet.";
                labelProcessText.Text = processText;
            }
        }

        private void SetupClassListView()
        {
            listViewAllClasses.View = View.Details;
            listViewAllClasses.FullRowSelect = true;
            listViewAllClasses.GridLines = true;



            listViewAllClasses.Columns.Clear();
            listViewAllClasses.Columns.Add("Hold navn", 130);
            listViewAllClasses.Columns.Add("Tilmeldte", 80);
            listViewAllClasses.Columns.Add("Kapacitet", 80);
            listViewAllClasses.Columns.Add("Starttid", 80);
            listViewAllClasses.Columns.Add("Varighed", 100);
            listViewAllClasses.Columns.Add("By", 80);
        }

        private void createNewClassBtn_Click(object sender, EventArgs e)
        {
            //CreateClassForm detailsForm = new CreateClassForm(_locationLogic, _classLogic);
            //detailsForm.ShowDialog();

            using (CreateClassForm createClassForm = new CreateClassForm(_locationLogic, _classLogic))
            {
                DialogResult result = createClassForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    GetAllClasses(this, EventArgs.Empty);
                }
            }
        }

        private void listViewAllClasses_DoubleClick(object sender, EventArgs e)
        {
            // sikrer at vi går videre kun hvis der er valgt præcis én række
            if (listViewAllClasses.SelectedItems.Count != 1)
                return;

            // Henter den valgte række
            ListViewItem listView = listViewAllClasses.SelectedItems[0];

            // Henter det tilknyttede Class objekt fra Tag egenskaben
            if (listView.Tag is not Class selectedClass)
                return;

            // Udfylder detalje sektionen med oplysninger fra det valgte Class objekt
            lblTextClassType.Text = selectedClass.ClassType.ToString();
            lblTextDescription.Text = selectedClass.Description;
            txtBoxEmployee.Text = $"{selectedClass.Instructor.FirstName} {selectedClass.Instructor.LastName}";
            txtBoxDateTime.Text = $"{selectedClass.TrainingDate:dd/MM/yyyy} kl. {selectedClass.StartTime:HH\\:mm}";
            txtBoxStartTime.Text = $"{selectedClass.StartTime:HH\\:mm}";
            txtBoxDuration.Text = $"{selectedClass.DurationInMinutes} minutter";
            txtBoxLocation.Text = $"{selectedClass.Room.Location.StreetName} {selectedClass.Room.Location.HouseNumber}, {selectedClass.Room.Location.Zipcode.ZipcodeNumber} {selectedClass.Room.Location.Zipcode.City.CityName}";
            txtBoxCapacity.Text = $"{selectedClass.Capacity}";
            txtBoxMemberCount.Text = $"{selectedClass.MemberCount}";
        }
    }
}
