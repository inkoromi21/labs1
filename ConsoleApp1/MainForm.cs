using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApp1 {
  public partial class MainForm : Form {
    private List<Flower> _flowers;

    public MainForm() {
      InitializeComponent();
      _flowers = new List<Flower>();
    }

    private void CalculateMass(object sender, EventArgs eventArgs) {
      const double diameterToRadius = 2.0;
      const double sphereVolumeFactor = 4.0 / 3.0;
      const string inputError = "Enter positive numbers. The hole diameter must be smaller than the cube edge.";
      double cubeEdge;
      double holeDiameter;
      double density;
      double holeRadius;
      double cubeVolume;
      double holeVolume;
      double mass;
      bool isCubeEdgeValid;
      bool isHoleDiameterValid;
      bool isDensityValid;

      _massTextBox.Clear();
      isCubeEdgeValid = double.TryParse(_cubeEdgeTextBox.Text, out cubeEdge);
      isHoleDiameterValid = double.TryParse(_holeDiameterTextBox.Text, out holeDiameter);
      isDensityValid = double.TryParse(_densityTextBox.Text, out density);

      if (!isCubeEdgeValid || !isHoleDiameterValid || !isDensityValid ||
          (double.IsNaN(cubeEdge) || double.IsInfinity(cubeEdge)) || (double.IsNaN(holeDiameter) || double.IsInfinity(holeDiameter)) ||
          (double.IsNaN(density) || double.IsInfinity(density)) || cubeEdge <= 0.0 ||
          holeDiameter <= 0.0 || holeDiameter >= cubeEdge || density <= 0.0) {
        MessageBox.Show(inputError);
        return;
      }

      holeRadius = holeDiameter / diameterToRadius;
      cubeVolume = cubeEdge * cubeEdge * cubeEdge;
      holeVolume = sphereVolumeFactor * Math.PI * holeRadius * holeRadius * holeRadius;
      mass = density * (cubeVolume - holeVolume);

      if ((double.IsNaN(mass) || double.IsInfinity(mass)) || mass <= 0.0) {
        const string calculationError = "The values are too large or too small for this calculation.";
        MessageBox.Show(calculationError);
        return;
      }

      _massTextBox.Text = mass.ToString();
    }

    private void GenerateFlowers(object sender, EventArgs eventArgs) {
      const string fileFilter = "CSV files (*.csv)|*.csv";
      const string defaultFileName = "flowers.csv";
      const string csvHeader = "Name,FloweringMonth,Size";
      const string csvSeparator = ",";
      const string fileError = "Cannot save the file. Check the folder and file permissions.";
      string[] flowerNames;
      List<Flower> generatedFlowers;
      Random random;
      Flower flower;
      int flowerIndex;
      int firstMonth;
      int monthCount;
      int minimumSize;
      int maximumSize;

      flowerNames = new string[] {
        "Rose", "Tulip", "Lily", "Daisy", "Iris", "Peony",
        "Orchid", "Violet", "Poppy", "Dahlia", "Aster", "Carnation"
      };
      generatedFlowers = new List<Flower>();
      random = new Random();
      firstMonth = 1;
      monthCount = 12;
      minimumSize = 1;
      maximumSize = 20;

      for (flowerIndex = 0; flowerIndex < flowerNames.Length; ++flowerIndex) {
        flower = new Flower();
        flower.Name = flowerNames[flowerIndex];
        flower.FloweringMonth = random.Next(firstMonth, monthCount + 1);
        flower.Size = random.Next(minimumSize, maximumSize + 1);
        generatedFlowers.Add(flower);
      }

      using (SaveFileDialog saveDialog = new SaveFileDialog()) {
        saveDialog.Filter = fileFilter;
        saveDialog.FileName = defaultFileName;

        if (saveDialog.ShowDialog() != DialogResult.OK) {
          return;
        }

        try {
          using (StreamWriter writer = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8)) {
            writer.WriteLine(csvHeader);

            for (flowerIndex = 0; flowerIndex < generatedFlowers.Count; ++flowerIndex) {
              flower = generatedFlowers[flowerIndex];
              writer.WriteLine(flower.Name + csvSeparator +
                flower.FloweringMonth.ToString(CultureInfo.InvariantCulture) + csvSeparator +
                flower.Size.ToString(CultureInfo.InvariantCulture));
            }
          }

          _flowers = generatedFlowers;
          DisplayFlowers();
        } catch (IOException) {
          MessageBox.Show(fileError);
        } catch (UnauthorizedAccessException) {
          MessageBox.Show(fileError);
        }
      }
    }

    private void LoadFlowers(object sender, EventArgs eventArgs) {
      const string fileFilter = "CSV files (*.csv)|*.csv";
      const string csvHeader = "Name,FloweringMonth,Size";
      const char csvSeparator = ',';
      const int columnCount = 3;
      const int nameColumnIndex = 0;
      const int monthColumnIndex = 1;
      const int sizeColumnIndex = 2;
      const int firstMonth = 1;
      const int lastMonth = 12;
      const string formatError = "Invalid CSV. Expected: Name,FloweringMonth,Size; month 1-12 and a positive size.";
      const string fileError = "Cannot read the file. Check the file and its permissions.";
      List<Flower> loadedFlowers;
      string line;
      string[] values;
      Flower flower;
      int floweringMonth;
      double flowerSize;
      bool isMonthValid;
      bool isSizeValid;

      loadedFlowers = new List<Flower>();

      using (OpenFileDialog openDialog = new OpenFileDialog()) {
        openDialog.Filter = fileFilter;

        if (openDialog.ShowDialog() != DialogResult.OK) {
          return;
        }

        try {
          using (StreamReader reader = new StreamReader(openDialog.FileName, Encoding.UTF8)) {
            line = reader.ReadLine();

            if (line != csvHeader) {
              MessageBox.Show(formatError);
              return;
            }

            line = reader.ReadLine();

            while (line != null) {
              values = line.Split(csvSeparator);

              if (values.Length != columnCount) {
                MessageBox.Show(formatError);
                return;
              }

              isMonthValid = int.TryParse(values[monthColumnIndex], out floweringMonth);
              isSizeValid = double.TryParse(values[sizeColumnIndex], NumberStyles.Float,
                CultureInfo.InvariantCulture, out flowerSize);

              if (string.IsNullOrWhiteSpace(values[nameColumnIndex]) || !isMonthValid ||
                  floweringMonth < firstMonth || floweringMonth > lastMonth ||
                  !isSizeValid || (double.IsNaN(flowerSize) || double.IsInfinity(flowerSize)) || flowerSize <= 0.0) {
                MessageBox.Show(formatError);
                return;
              }

              flower = new Flower();
              flower.Name = values[nameColumnIndex];
              flower.FloweringMonth = floweringMonth;
              flower.Size = flowerSize;
              loadedFlowers.Add(flower);
              line = reader.ReadLine();
            }
          }

          _flowers = loadedFlowers;
          DisplayFlowers();
        } catch (IOException) {
          MessageBox.Show(fileError);
        } catch (UnauthorizedAccessException) {
          MessageBox.Show(fileError);
        }
      }
    }

    private void DisplayFlowers() {
      const string tableHeader = "Name | Flowering month | Size (cm)";
      const string separator = " | ";
      int flowerIndex;
      Flower flower;

      _flowersTextBox.Clear();
      _searchResultTextBox.Clear();
      _flowersTextBox.AppendText(tableHeader + Environment.NewLine);

      for (flowerIndex = 0; flowerIndex < _flowers.Count; ++flowerIndex) {
        flower = _flowers[flowerIndex];
        _flowersTextBox.AppendText(flower.Name + separator +
          flower.FloweringMonth + separator + flower.Size + Environment.NewLine);
      }
    }

    private void FindFlowers(object sender, EventArgs eventArgs) {
      const int firstMonth = 1;
      const int lastMonth = 12;
      const string monthError = "Enter a month number from 1 to 12.";
      const string emptyDataError = "Generate or load flower data first.";
      const string noMatches = "No flowers bloom in this month.";
      const string tableHeader = "Name | Flowering month | Size (cm)";
      const string separator = " | ";
      const string smallestLabel = "Smallest flower: ";
      const string sizeUnit = " cm";
      int selectedMonth;
      int flowerIndex;
      Flower flower;
      Flower smallestFlower;
      bool isMonthValid;

      _searchResultTextBox.Clear();
      smallestFlower = null;
      isMonthValid = int.TryParse(_monthTextBox.Text, out selectedMonth);

      if (!isMonthValid || selectedMonth < firstMonth || selectedMonth > lastMonth) {
        MessageBox.Show(monthError);
        return;
      }

      if (_flowers.Count == 0) {
        MessageBox.Show(emptyDataError);
        return;
      }

      _searchResultTextBox.AppendText(tableHeader + Environment.NewLine);

      for (flowerIndex = 0; flowerIndex < _flowers.Count; ++flowerIndex) {
        flower = _flowers[flowerIndex];

        if (flower.FloweringMonth == selectedMonth) {
          _searchResultTextBox.AppendText(flower.Name + separator +
            flower.FloweringMonth + separator + flower.Size + Environment.NewLine);

          if (smallestFlower == null || flower.Size < smallestFlower.Size) {
            smallestFlower = flower;
          }
        }
      }

      if (smallestFlower == null) {
        _searchResultTextBox.Text = noMatches;
      } else {
        _searchResultTextBox.AppendText(Environment.NewLine + smallestLabel +
          smallestFlower.Name + separator + smallestFlower.Size + sizeUnit);
      }
    }
  }
}
