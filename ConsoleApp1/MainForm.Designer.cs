using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp1 {
  public partial class MainForm {
    private TextBox _cubeEdgeTextBox;
    private TextBox _holeDiameterTextBox;
    private TextBox _densityTextBox;
    private TextBox _massTextBox;
    private TextBox _flowersTextBox;
    private TextBox _monthTextBox;
    private TextBox _searchResultTextBox;

    private System.ComponentModel.IContainer _components = null;
    private GroupBox _massGroup;
    private Label _cubeEdgeLabel;
    private Label _holeDiameterLabel;
    private Label _densityLabel;
    private Button _calculateButton;
    private Label _massLabel;
    private GroupBox _flowersGroup;
    private Button _generateButton;
    private Button _loadButton;
    private Label _monthLabel;
    private Button _findButton;

    protected override void Dispose(bool disposing) {
      if (disposing && _components != null) {
        _components.Dispose();
      }

      base.Dispose(disposing);
    }

    private void InitializeComponent() {
      _massGroup = new GroupBox();
      _cubeEdgeLabel = new Label();
      _cubeEdgeTextBox = new TextBox();
      _holeDiameterLabel = new Label();
      _holeDiameterTextBox = new TextBox();
      _densityLabel = new Label();
      _densityTextBox = new TextBox();
      _calculateButton = new Button();
      _massLabel = new Label();
      _massTextBox = new TextBox();
      _flowersGroup = new GroupBox();
      _generateButton = new Button();
      _loadButton = new Button();
      _flowersTextBox = new TextBox();
      _monthLabel = new Label();
      _monthTextBox = new TextBox();
      _findButton = new Button();
      _searchResultTextBox = new TextBox();

      SuspendLayout();
      _massGroup.SuspendLayout();
      _flowersGroup.SuspendLayout();

      _massGroup.Location = new Point(15, 15);
      _massGroup.Size = new Size(750, 210);
      _massGroup.Text = "Mass of a cube with a spherical hole";

      _cubeEdgeLabel.Location = new Point(15, 30);
      _cubeEdgeLabel.Size = new Size(240, 23);
      _cubeEdgeLabel.Text = "Cube edge (m)";

      _cubeEdgeTextBox.Location = new Point(280, 30);
      _cubeEdgeTextBox.Size = new Size(200, 23);

      _holeDiameterLabel.Location = new Point(15, 65);
      _holeDiameterLabel.Size = new Size(240, 23);
      _holeDiameterLabel.Text = "Hole diameter (m)";

      _holeDiameterTextBox.Location = new Point(280, 65);
      _holeDiameterTextBox.Size = new Size(200, 23);

      _densityLabel.Location = new Point(15, 100);
      _densityLabel.Size = new Size(240, 23);
      _densityLabel.Text = "Density (kg/m^3)";

      _densityTextBox.Location = new Point(280, 100);
      _densityTextBox.Size = new Size(200, 23);

      _calculateButton.Location = new Point(510, 30);
      _calculateButton.Size = new Size(210, 35);
      _calculateButton.Text = "Calculate";
      _calculateButton.Click += CalculateMass;

      _massLabel.Location = new Point(15, 145);
      _massLabel.Size = new Size(240, 23);
      _massLabel.Text = "Mass (kg)";

      _massTextBox.Location = new Point(280, 145);
      _massTextBox.Size = new Size(440, 23);
      _massTextBox.ReadOnly = true;

      _flowersGroup.Location = new Point(15, 240);
      _flowersGroup.Size = new Size(750, 465);
      _flowersGroup.Text = "Flower data";

      _generateButton.Location = new Point(15, 30);
      _generateButton.Size = new Size(230, 35);
      _generateButton.Text = "Generate and save CSV";
      _generateButton.Click += GenerateFlowers;

      _loadButton.Location = new Point(260, 30);
      _loadButton.Size = new Size(180, 35);
      _loadButton.Text = "Load CSV";
      _loadButton.Click += LoadFlowers;

      _flowersTextBox.Location = new Point(15, 80);
      _flowersTextBox.Size = new Size(705, 140);
      _flowersTextBox.ReadOnly = true;
      _flowersTextBox.Multiline = true;
      _flowersTextBox.ScrollBars = ScrollBars.Vertical;

      _monthLabel.Location = new Point(15, 240);
      _monthLabel.Size = new Size(130, 23);
      _monthLabel.Text = "Month (1-12)";

      _monthTextBox.Location = new Point(155, 240);
      _monthTextBox.Size = new Size(120, 23);

      _findButton.Location = new Point(295, 235);
      _findButton.Size = new Size(180, 35);
      _findButton.Text = "Find flowers";
      _findButton.Click += FindFlowers;

      _searchResultTextBox.Location = new Point(15, 285);
      _searchResultTextBox.Size = new Size(705, 160);
      _searchResultTextBox.ReadOnly = true;
      _searchResultTextBox.Multiline = true;
      _searchResultTextBox.ScrollBars = ScrollBars.Vertical;

      Controls.Add(_massGroup);
      _massGroup.Controls.Add(_cubeEdgeLabel);
      _massGroup.Controls.Add(_cubeEdgeTextBox);
      _massGroup.Controls.Add(_holeDiameterLabel);
      _massGroup.Controls.Add(_holeDiameterTextBox);
      _massGroup.Controls.Add(_densityLabel);
      _massGroup.Controls.Add(_densityTextBox);
      _massGroup.Controls.Add(_calculateButton);
      _massGroup.Controls.Add(_massLabel);
      _massGroup.Controls.Add(_massTextBox);
      Controls.Add(_flowersGroup);
      _flowersGroup.Controls.Add(_generateButton);
      _flowersGroup.Controls.Add(_loadButton);
      _flowersGroup.Controls.Add(_flowersTextBox);
      _flowersGroup.Controls.Add(_monthLabel);
      _flowersGroup.Controls.Add(_monthTextBox);
      _flowersGroup.Controls.Add(_findButton);
      _flowersGroup.Controls.Add(_searchResultTextBox);

      AutoScaleDimensions = new SizeF(7.0f, 15.0f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(780, 720);
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      StartPosition = FormStartPosition.CenterScreen;
      Name = "MainForm";
      Text = "Laboratory work - Variant 10";

      _massGroup.ResumeLayout(false);
      _massGroup.PerformLayout();
      _flowersGroup.ResumeLayout(false);
      _flowersGroup.PerformLayout();
      ResumeLayout(false);
    }
  }
}
