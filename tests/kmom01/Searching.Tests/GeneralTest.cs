namespace Searching;
using Searching;
using System.Reflection;
using System.Threading.Tasks;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom01";
    private string _testDirectory = "";
    private string _targetDir = "";

    [OneTimeSetUp]
    public void SetupBeforeTests()
    {
        var workflowDir = Environment.GetEnvironmentVariable("PROJECT_ROOT");

        this._testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "./";
        if (!string.IsNullOrEmpty(workflowDir))
        {
            // Körs i GitHub Actions
            _targetDir = Path.Combine(workflowDir, KMOM, "Searching") ?? "";
        }
        else
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, "kmom01", "Searching") ?? "";
        }
    }

    [Test]
    public void RequiredFilesCode()
    {
        Assert.That(Directory.Exists(_targetDir), Is.True, $"Katalogen saknas: {_targetDir}");

        var requiredFiles = new[]
        {
            "Program.cs",
            "SearchAlgorithms.cs",
            "BusStop.cs",
            "orderedWords1200.txt",
        };

        foreach (var file in requiredFiles)
        {
            var fullPath = Path.Combine(_targetDir, file);
            Assert.That(File.Exists(fullPath), Is.True, $"Filen saknas: {file}");
        }
        TestContext.Out.WriteLine($"✅ All required files for {KMOM} is there.");
    }

    [Test]
    public void CheckTestCode()
    {
        string testProject = _targetDir + ".Tests";
        Assert.That(Directory.Exists(testProject), Is.True, $"Katalogen saknas: {testProject}");

        var exists = Directory.EnumerateFiles(testProject, "*.cs", SearchOption.TopDirectoryOnly).Any();
        Assert.That(exists, Is.True, $"Hittade inga .cs-filer i {testProject}");

        TestContext.Out.WriteLine($"✅ {testProject} must include at least one test class ({KMOM}).");
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
            Assert.That(content, Does.Contain("using Searching"), $"Namespace should be called Searching and used in {filename}.");
            Assert.That(content, Does.Contain("LinearSearch"), $"{filename} should contain LinearSearch.");
            Assert.That(content, Does.Contain("BinarySearch"), $"{filename} should contain BinarySearch.");
            Assert.That(content, Does.Contain("new BusStop"), $"{filename} should contain new BusStop.");
            Assert.That(content, Does.Contain("CompareTo"), $"{filename} should contain CompareTo.");
        });

        TestContext.Out.WriteLine($"✅ {filename} använder sökmetoder och BusStop ({KMOM}).");
    }
}
