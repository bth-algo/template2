namespace Searching;
using Searching;
using System.Reflection;
using System.Threading.Tasks;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom02";
    private const string NAME = "Sorting";
    //private string _testDirectory = "";
    private string _targetDir = "";

    [OneTimeSetUp]
    public void SetupBeforeTests()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        //this._testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "./";
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
            "SortingAlgorithms.cs",
            "BusStop.cs",
            "resultat.txt",
            "stops.txt",
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
            Assert.That(content, Does.Contain("using Sorting"), $"{filename} should contain using Sorting.");
            Assert.That(content, Does.Contain("Insertionsort"), $"{filename} should contain Inserttionsort.");
            Assert.That(content, Does.Contain("Quicksort"), $"{filename} should contain Quicksort.");
        });

        TestContext.Out.WriteLine($"✅ {filename} använder rätt namespace och sorteringsmetoder ({KMOM}).");
    }

    [Test]
    public void CheckIfResultatIsOk()
    {
        string filename = "resultat.txt";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("Osorterad array"), $"{filename} should contain Ordnad array.");
            Assert.That(content, Does.Contain("Sorterad array"), $"{filename} should contain Sorterad array.");
            Assert.That(content, Does.Contain("Omvänt sorterad array"), $"{filename} should contain Omvänt sorterad array.");
            Assert.That(content, Does.Contain("Insertionsort"), $"{filename} should contain Insertionsort.");
            Assert.That(content, Does.Contain("Quicksort"), $"{filename} should contain Quicksort.");
        });

        TestContext.Out.WriteLine($"✅ {filename} har rätt rubriker och sorteringsmetoder ({KMOM}).");
    }

    [Test]
    public void CheckIfSortingAlgorithmsIsOk()
    {
        string filename = "SortingAlgorithms.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("public static void Insertionsort<T>(T[] A, int n) where T : IComparable<T>"), $"{filename} should contain correct definition of Insertionsort.");
            Assert.That(content, Does.Contain("public static void Quicksort<T>(T[] A, int n) where T : IComparable<T>"), $"{filename} should contain correct definition of  public Quicksort.");
            Assert.That(content, Does.Contain("private static void Quicksort<T>(T[] A, int start, int end) where T : IComparable<T>"), $"{filename} should contain correct definition of  public Quicksort.");
        });

        TestContext.Out.WriteLine($"✅ {filename} har rätt rubriker och sorteringsmetoder ({KMOM}).");
    }
}
