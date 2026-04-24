namespace Heap;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom05";
    private const string NAME = "Heap";
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
            "IHeap.cs",
            "Heap.cs",
            "UsingHeap.cs",
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
            Assert.That(content, Does.Contain("using Heap"), $"{filename} should contain using Heap.");
            Assert.That(content, Does.Contain("new Heap<"), $"{filename} should contain new Heap.");
            Assert.That(content, Does.Contain("IHeap"), $"{filename} should contain declaration IHeap.");
        });

        TestContext.Out.WriteLine($"✅ {filename} har innehåll.");
    }

    [Test]
    public void CheckIfIHeapIsOk()
    {
        string filename = "IHeap.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("void Insert(T item)"), $"{filename} should contain void Insert(T item).");
            Assert.That(content, Does.Contain("T Remove()"), $"{filename} should contain T Remove().");
            Assert.That(content, Does.Contain("T Peek()"), $"{filename} should contain T Peek().");
            Assert.That(content, Does.Contain("int Size()"), $"{filename} should contain int Size().");
            Assert.That(content, Does.Contain("T[] GetAllElements()"), $"{filename} should contain T[] GetAllElements().");
            Assert.That(content, Does.Contain("IComparable"), $"{filename} should contain declaration IComparable.");
        });

        TestContext.Out.WriteLine($"✅ {filename} innehåller rätt metoddeklarationer ({KMOM}).");
    }

    [Test]
    public void CheckIfUsingHeapIsOk()
    {
        string filename = "UsingHeap.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("UsingHeap"), $"{filename} should contain the class UsingHeap.");
            Assert.That(content, Does.Contain("SortUsingAHeap"), $"{filename} should contain the method SortUsingAHeap.");
            Assert.That(content, Does.Contain("IComparable"), $"{filename} should contain declaration IComparable.");
        });

        TestContext.Out.WriteLine($"✅ {filename} innehåller klassen UsingHeap och metoden SortUsingHeap ({KMOM}).");
    }
}
