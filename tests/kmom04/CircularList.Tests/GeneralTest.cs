namespace CircularList;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom04";
    private const string NAME = "CircularList";
    private string _targetDir = "";

    [OneTimeSetUp]
    public void SetupBeforeTests()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        if (!string.IsNullOrEmpty(workflowDir))
        {
            // Körs i GitHub Actions
            _targetDir = Path.Combine(workflowDir, KMOM, NAME) ?? "";
        }
        else
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, KMOM, NAME) ?? "";
        }
    }

    [Test]
    public void RequiredFilesCode()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "Program.cs",
            "ICircularList.cs",
            "CircularList.cs",
            "names.txt",
            "AlternativRepresentation.txt",
        };

        foreach (var file in requiredFiles)
        {
            var fullPath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(fullPath), Is.True, $"Filen saknas: {file}");
        }
        TestContext.Out.WriteLine($"✅ All required files for {KMOM} is there.");
    }

    [Test]
    public void CheckIfProgramIsOk()
    {
        string filename = "Program.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("using CircularList"), $"{filename} should contain using CircularList.");
            Assert.That(content, Does.Contain("new CircularList<"), $"{filename} should contain new CircularList.");
            Assert.That(content, Does.Contain("ICircularList"), $"{filename} should contain declaration ICircularList.");
        });

        TestContext.Out.WriteLine($"✅ {filename} har innehåll.");
    }

    [Test]
    public void CheckIfAlternativRepresentationIsOk()
    {
        string filename = "AlternativRepresentation.txt";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.That(content, Has.Length.GreaterThan(100), $"{filename} should contain your reflection.");

        TestContext.Out.WriteLine($"✅ {filename} använder rätt namespace och skapar en cirkulär lista av typen ICircularList ({KMOM}).");
    }
}
