using System;
using System.Windows.Forms;

namespace SemestralniPlanovac
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateCalendar(DateTime.Now.Year, DateTime.Now.Month); // Vytvo�en� kalend��e pro aktu�ln� m�s�c
        }

        // Funkce pro vytvo�en� kalend��e
        private void CreateCalendar(int year, int month)
        {
            // Vy�ist�me panel p�ed ka�d�m nov�m vytvo�en�m kalend��e
            tableLayoutPanel1.Controls.Clear();

            // Nastaven� n�zvu m�s�ce a roku
            this.Text = $"{new DateTime(year, month, 1):MMMM yyyy}";

            // Z�sk�n� po��te�n�ho dne v m�s�ci a po�tu dn� v m�s�ci
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDay = (int)firstDayOfMonth.DayOfWeek; // Z�sk�n� dne v t�dnu pro prvn� den m�s�ce

            // Zm�n�me startDay, aby t�den za��nal Pond�l�m (�prava pro Pond�l� jako prvn�)
            startDay = (startDay == 0) ? 6 : startDay - 1; // Pokud za��n� t�den ned�l� (0), p�epo�teme na 6 (Pond�l�)

            // Nastaven� velikosti sloupc� a ��dk� (7 sloupc� pro dny v t�dnu, max. 6 ��dk� pro t�dny)
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.RowCount = 6;

            // Nastaven� velikosti sloupc� na stejnou ���ku
            for (int i = 0; i < 7; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
            }

            // Nastaven� velikosti ��dk� na stejnou v��ku
            for (int i = 0; i < 6; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
            }

            // P�id�n� Label pro dny v t�dnu (Pond�l�, �ter�, St�eda, atd.)
            string[] weekDays = { "Pond�l�", "�ter�", "St�eda", "�tvrtek", "P�tek", "Sobota", "Ned�le" };
            for (int i = 0; i < weekDays.Length; i++)
            {
                Label headerLabel = new Label
                {
                    Text = weekDays[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = 60,
                    Height = 40,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = System.Drawing.Color.LightGray
                };
                tableLayoutPanel1.Controls.Add(headerLabel, i, 0);  // Prvn� ��dek pro n�zvy dn�
            }

            // P�id�n� pr�zdn�ch Label pro prvn� ��dek (pokud m�s�c neza��n� v pond�l�)
            for (int i = 0; i < startDay; i++)
            {
                tableLayoutPanel1.Controls.Add(new Label(), i, 1);  // Pr�zdn� bu�ky pro prvn� ��dek
            }

            // P�id�n� Label pro ka�d� den v m�s�ci
            int rowIndex = 1;  // Za�neme od druh�ho ��dku (po dni v t�dnu)
            for (int day = 1; day <= daysInMonth; day++)
            {
                Label dayLabel = new Label
                {
                    Text = day.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = 60,
                    Height = 60,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = System.Drawing.Color.White,
                    Font = new System.Drawing.Font("Arial", 12)
                };

                // P�id�me Label do spr�vn�ho sloupce a ��dku
                tableLayoutPanel1.Controls.Add(dayLabel, (startDay + day - 1) % 7, rowIndex);

                // Pokud je posledn� den v t�dnu (ned�le), posuneme na nov� ��dek
                if ((startDay + day) % 7 == 0)
                {
                    rowIndex++;
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // M��e� p�idat vlastn� logiku pro vykreslov�n� kalend��e, pokud je pot�eba
        }
    }
}
