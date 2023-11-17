using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BtcAverageCounter
{
    public partial class MainForm : Form
    {
        private const int DefaultCellValue = 0;
        private const string DefaultCellText = "0";

        private readonly List<BtcRecord> _btcRecords = new List<BtcRecord>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cells = dataGridView1.Rows[e.RowIndex].Cells;

            foreach (DataGridViewCell cell in cells)
            {
                ValidateCellValue(cell);
            }

            UpdateInfo();
        }

        private static void ValidateCellValue(DataGridViewCell cell)
        {
            var text = cell.Value?.ToString();

            if (!double.TryParse(text, out double v))
            {
                cell.Value = DefaultCellText;
            }
        }

        private void UpdateInfo()
        {
            UpdateRecords();
            DisplayNewValues();
        }

        private void UpdateRecords()
        {
            _btcRecords.Clear();

            _btcRecords.AddRange(
                dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Select(row => GetRecord(row)));
        }

        private void DisplayNewValues()
        {
            AllAmountTextBox.Text = _btcRecords.GetAllAmount().ToString();
            FullCostTextBox.Text = _btcRecords.GetFullCost().ToString();
            AverageCostTextBox.Text = _btcRecords.GetAverageCost().ToString();
        }

        private BtcRecord GetRecord(DataGridViewRow row)
        {
            var cells = row.Cells;

            return new BtcRecord(
                GetCellValue(cells[0]),
                GetCellValue(cells[1]));
        }

        private static double GetCellValue(DataGridViewCell cell)
        {
            return double.TryParse(cell.Value?.ToString(), out double value) ?
                value :
                DefaultCellValue;
        }
    }
}
