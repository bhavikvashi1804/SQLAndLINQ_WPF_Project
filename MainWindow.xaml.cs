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
using System.Configuration;

namespace SQLAndLINQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinqToDataClasses1DataContext linqToDataClasses1DataContext;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAndLINQ.Properties.Settings.DBDemoConnectionString"].ConnectionString;
            linqToDataClasses1DataContext = new LinqToDataClasses1DataContext(connectionString);

            InsertLectures();
           
        }


        public void InsertUniversity()
        {

            linqToDataClasses1DataContext.ExecuteCommand("delete from University");
            
            University university = new University { UniversityName = "GTU" };
            linqToDataClasses1DataContext.Universities.InsertOnSubmit(university);
            University university1 = new University { UniversityName = "DDU" };
            linqToDataClasses1DataContext.Universities.InsertOnSubmit(university1);
            linqToDataClasses1DataContext.SubmitChanges();

            MainDataGrid.ItemsSource = linqToDataClasses1DataContext.Universities;
        }


        public void InsertStudents()
        {

            linqToDataClasses1DataContext.ExecuteCommand("delete from Student");

            //get the GTU university using lambda funtion
            University gtuUniversity = linqToDataClasses1DataContext.Universities.First(university => university.UniversityName.Equals("GTU"));

            University dduUniversity = linqToDataClasses1DataContext.Universities.First(university => university.UniversityName.Equals("DDU"));



            List<STUDENT> studentList = new List<STUDENT>();
            studentList.Add(new STUDENT { Name = "Bhavik Vashi", Age = 22, Gender="Male",UniversityID=gtuUniversity.Id});
            studentList.Add(new STUDENT { Name = "Raj Patel", Age = 21, Gender = "Male", UniversityID = gtuUniversity.Id });
            studentList.Add(new STUDENT { Name = "Yash Patel", Age = 23, Gender = "Male", UniversityID = gtuUniversity.Id });
            studentList.Add(new STUDENT { Name = "Paras Patel", Age = 21, Gender = "Male", University=dduUniversity });
            studentList.Add(new STUDENT { Name = "Rani Patel", Age = 22, Gender = "Female", University=dduUniversity });
            studentList.Add(new STUDENT { Name = "Palak Patel", Age = 22, Gender = "Female", University=gtuUniversity });


            linqToDataClasses1DataContext.STUDENTs.InsertAllOnSubmit(studentList);

            linqToDataClasses1DataContext.SubmitChanges();
            MainDataGrid.ItemsSource = linqToDataClasses1DataContext.STUDENTs;
        }

        public void InsertLectures()
        {

            linqToDataClasses1DataContext.ExecuteCommand("delete from Lecture");

            linqToDataClasses1DataContext.Lectures.InsertOnSubmit(new Lecture { Name="Math"});
            linqToDataClasses1DataContext.Lectures.InsertOnSubmit(new Lecture { Name = "Phy" });
            linqToDataClasses1DataContext.Lectures.InsertOnSubmit(new Lecture { Name = "Chem" });
            linqToDataClasses1DataContext.Lectures.InsertOnSubmit(new Lecture { Name = "Bio" });

            linqToDataClasses1DataContext.SubmitChanges();
            MainDataGrid.ItemsSource = linqToDataClasses1DataContext.Lectures;

        }
    }
}
