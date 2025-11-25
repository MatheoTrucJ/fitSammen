using FitSammenDekstopClient.Model;
using System.Collections.Generic;

namespace FitSammenDekstopClient
{
    public partial class FitSammen : Form
    {
        // private readonly ClassLogic _classLogic; // Omid du bestemmer nanvet på vores BLL klasse
        public FitSammen()
        {
            InitializeComponent();
            SetupClassListView();
            GetAllClasses(this, EventArgs.Empty);
            //listViewAllClasses.DoubleClick += listViewAllClasses_DoubleClick;

            // _classLogic = new ClassLogic();
        }

        private void GetAllClasses(object sender, EventArgs e)
        {
            // IEnumerable<Class> classes = await _classLogic.GetAllClassesAsync(); // Omids metode 
            IEnumerable<Class> classes = TestDataFactory.CreateAllTestClasses();

            if (classes != null)
            {
                listViewAllClasses.Items.Clear();

                foreach (Class c in classes)
                {
                    ListViewItem item = new ListViewItem(c.Name);

                    item.SubItems.Add(c.MemberCount.ToString());
                    item.SubItems.Add(c.Capacity.ToString());
                    item.SubItems.Add(c.StartTime.ToString("HH\\:mm"));
                    item.SubItems.Add(c.Room.Location.Zipcode.City.CityName);
                    item.SubItems.Add(c.Room.RoomName);

                    item.Tag = c;

                    listViewAllClasses.Items.Add(item);
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
            listViewAllClasses.Columns.Add("Navn", 130);
            listViewAllClasses.Columns.Add("Tilmeldte", 80);
            listViewAllClasses.Columns.Add("Kapacitet", 80);
            listViewAllClasses.Columns.Add("Starttid", 80);
            listViewAllClasses.Columns.Add("By", 80);
            listViewAllClasses.Columns.Add("Lokale", 120);
        }

        private void createNewClassBtn_Click(object sender, EventArgs e)
        {
            CreateClassForm detailsForm = new CreateClassForm();
            detailsForm.Show(); // modeless (brugeren kan stadig bruge hovedvinduet)
            // detailsForm.ShowDialog(); // modal (brugeren skal lukke den først)
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


        // Alt under her er for teste mit GetAllClasses med test data (skal sluttes efter omid har fået sit til at virke :D)
        public static class TestDataFactory
        {
            public static Employee CreateTestInstructor(
                string firstName = "Lise",
                string lastName = "Hansen",
                int userId = 1)
            {
                return new Employee(
                    firstName: firstName,
                    lastName: lastName,
                    email: "lise.hansen@fitsammen.dk",
                    phone: "12345678",
                    birthDate: new DateOnly(1990, 5, 12),
                    userID: userId,
                    userType: UserType.Employee,
                    CPRNumber: "123456-7890"
                );
            }

            public static Location CreateTestLocation()
            {
                return new Location(
                    streetName: "Fitnessvej",
                    housenumber: 10,
                    zipCodeNumber: 9000,
                    cityName: "Aalborg",
                    countryName: "Danmark"
                );
            }

            public static Room CreateTestRoom(
                int roomId = 1,
                string roomName = "Sal 1",
                int capacity = 20)
            {
                return new Room(
                    roomId: roomId,
                    roomName: roomName,
                    capacity: capacity,
                    location: CreateTestLocation()
                );
            }

            public static Class CreateYogaClass(
                int id = 1,
                DateOnly? date = null,
                TimeOnly? startTime = null)
            {
                var trainingDate = date ?? DateOnly.FromDateTime(DateTime.Today);
                var time = startTime ?? new TimeOnly(17, 0);

                return new Class(
                    id: id,
                    trainingDate: trainingDate,
                    instructor: CreateTestInstructor(),
                    description: "Rolig yoga med fokus på smidighed og vejrtrækning.",
                    room: CreateTestRoom(),
                    name: "Aften Yoga",
                    capacity: 20,
                    memberCount: 0,
                    durationInMinutes: 60,
                    startTime: time,
                    classType: ClassType.Yoga
                );
            }

            public static Class CreateSpinningClass(
                int id = 2,
                DateOnly? date = null,
                TimeOnly? startTime = null)
            {
                var trainingDate = date ?? DateOnly.FromDateTime(DateTime.Today.AddDays(1));
                var time = startTime ?? new TimeOnly(18, 30);

                return new Class(
                    id: id,
                    trainingDate: trainingDate,
                    instructor: CreateTestInstructor("Mads", "Jensen", userId: 2),
                    description: "Intens spinning med høj puls.",
                    room: CreateTestRoom(roomId: 2, roomName: "Spinningsal", capacity: 25),
                    name: "Power Spinning",
                    capacity: 25,
                    memberCount: 0,
                    durationInMinutes: 45,
                    startTime: time,
                    classType: ClassType.Spinning
                );
            }

            public static List<Class> CreateAllTestClasses()
            {
                var list = new List<Class>
            {
                CreateYogaClass(),
                CreateSpinningClass(),
                new Class(
                    id: 3,
                    trainingDate: DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                    instructor: CreateTestInstructor("Sofie", "Lund", userId: 3),
                    description: "Styrketræning med frie vægte.",
                    room: CreateTestRoom(roomId: 3, roomName: "Styrkesal", capacity: 15),
                    name: "Full Body Strength",
                    capacity: 15,
                    memberCount: 0,
                    durationInMinutes: 55,
                    startTime: new TimeOnly(19, 0),
                    classType: ClassType.StrengthTraining
                )
            };

                return list;
            }
        }
    }
}
