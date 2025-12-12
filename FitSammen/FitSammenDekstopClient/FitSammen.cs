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
                    
                }
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
            using (CreateClassForm createClassForm = new CreateClassForm(_locationLogic, _classLogic))
            {
                DialogResult result = createClassForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    GetAllClasses(this, EventArgs.Empty);
                }
            }
        }

        private void refreshClassesBtn_Click(object sender, EventArgs e)
        {
            GetAllClasses(sender, e);
        }
    }
}
