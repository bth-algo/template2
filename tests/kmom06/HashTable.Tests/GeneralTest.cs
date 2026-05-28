namespace HashTable.Tests;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom06";
    private const string NAME = "HashTable";
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
            "TestElement.cs",
            "BusStop.cs",
            "BusStopKey.cs",
            "IHashTable.cs",
            "HashTable.cs",
            "Program.cs"
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

        Assert.That(content, Does.Contain("new HashTable"), $"{filename} should contain new HashTable.");
        Assert.That(content, Does.Contain("IHashTable"), $"{filename} should contain IHashTable should be used to type a variable.");

        TestContext.Out.WriteLine($"✅ {filename} har innehåll men har du testat allt.");
    }

    [Test]
    public void CheckIfIHashTableIsOk()
    {
        string filename = "IHashTable.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("bool Add(Key key, Value element)"), $"{filename} should contain void Insert(T item).");
            Assert.That(content, Does.Contain("bool Remove(Key key)"), $"{filename} should contain T Remove().");
            Assert.That(content, Does.Contain("bool Contains(Key key)"), $"{filename} should contain T Peek().");
            Assert.That(content, Does.Contain("Value Get(Key key)"), $"{filename} should contain int Size().");
            Assert.That(content, Does.Contain("double LoadFactor()"), $"{filename} should contain T[] GetAllElements().");
        });

        TestContext.Out.WriteLine($"✅ {filename} innehåller rätt metoddeklarationer ({KMOM}).");
    }

    [Test]
    public void CheckIfIDefaultConstructorIsOk()
    {
        var ctor = typeof(HashTable.HashTable<int, int>).GetConstructor(Type.EmptyTypes);

        Assert.That(ctor, Is.Not.Null, $"{NAME} should have a default constructor.");

        TestContext.Out.WriteLine($"✅ Din {NAME} har en defaultkonstruktor utan inparametrar.");
    }
}
