using System;
using System.Windows.Forms;

namespace SemestralniPlanovac
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateCalendar(DateTime.Now.Year, DateTime.Now.Month); // Vytvoøení kalendáøe pro aktuální mìsíc
        }

        // Funkce pro vytvoøení kalendáøe
        private void CreateCalendar(int year, int month)
        {
            // Vyèistíme panel pøed každým novým vytvoøením kalendáøe
            tableLayoutPanel1.Controls.Clear();

            // Nastavení názvu mìsíce a roku
            this.Text = $"{new DateTime(year, month, 1):MMMM yyyy}";

            // Získání poèáteèního dne v mìsíci a poètu dní v mìsíci
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDay = (int)firstDayOfMonth.DayOfWeek; // Získání dne v týdnu pro první den mìsíce

            // Zmìníme startDay, aby týden zaèínal Pondìlím (úprava pro Pondìlí jako první)
            startDay = (startDay == 0) ? 6 : startDay - 1; // Pokud zaèíná týden nedìlí (0), pøepoèteme na 6 (Pondìlí)

            // Nastavení velikosti sloupcù a øádkù (7 sloupcù pro dny v týdnu, max. 6 øádkù pro týdny)
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.RowCount = 6;

            // Nastavení velikosti sloupcù na stejnou šíøku
            for (int i = 0; i < 7; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
            }

            // Nastavení velikosti øádkù na stejnou výšku
            for (int i = 0; i < 6; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
            }

            // Pøidání Label pro dny v týdnu (Pondìlí, Úterý, Støeda, atd.)
            string[] weekDays = { "Pondìlí", "Úterý", "Støeda", "Ètvrtek", "Pátek", "Sobota", "Nedìle" };
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
                tableLayoutPanel1.Controls.Add(headerLabel, i, 0);  // První øádek pro názvy dní
            }

            // Pøidání prázdných Label pro první øádek (pokud mìsíc nezaèíná v pondìlí)
            for (int i = 0; i < startDay; i++)
            {
                tableLayoutPanel1.Controls.Add(new Label(), i, 1);  // Prázdné buòky pro první øádek
            }

            // Pøidání Label pro každý den v mìsíci
            int rowIndex = 1;  // Zaèneme od druhého øádku (po dni v týdnu)
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

                // Pøidáme Label do správného sloupce a øádku
                tableLayoutPanel1.Controls.Add(dayLabel, (startDay + day - 1) % 7, rowIndex);

                // Pokud je poslední den v týdnu (nedìle), posuneme na nový øádek
                if ((startDay + day) % 7 == 0)
                {
                    rowIndex++;
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Mùžeš pøidat vlastní logiku pro vykreslování kalendáøe, pokud je potøeba
        }
    }
}
