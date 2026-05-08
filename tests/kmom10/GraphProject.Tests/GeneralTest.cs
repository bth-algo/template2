namespace GraphProject;

[TestFixture]
public class GeneralTest
{
    private const string KMOM = "kmom10";
    private const string NAME = "GraphProject";
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
            "IHashTable.cs",
            "HashTable.cs",
            "IGraph.cs",
            "Graph.cs",
            "GraphHandler.cs"
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

        Assert.That(content, Does.Contain("Graph"), $"{filename} should contain using Graph.");

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
            Assert.That(content, Does.Contain("IComparable"), $"{filename} should contain declaration IComparable.");
        });

        TestContext.Out.WriteLine($"✅ {filename} innehåller rätt metoddeklarationer ({KMOM}).");
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
    public void CheckIfIGraphIsOk()
    {
        string filename = "IGraph.cs";
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = File.ReadAllText(filePath) ?? "";

        var nonEmptyLines = content
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        Assert.Multiple(() =>
        {
            Assert.That(content, Does.Contain("void AddEdge(T fromVertex, T toVertex, int weight)"), $"{filename} should contain void Insert(T item).");
            Assert.That(content, Does.Contain("bool RemoveEdge(T fromvertex, T tovertex)"), $"{filename} should contain T Remove().");
            Assert.That(content, Does.Contain("int GetWeight(T fromvertex, T tovertex)"), $"{filename} should contain T Peek().");
            Assert.That(content, Does.Contain("int GetNrOfVerticies()"), $"{filename} should contain int Size().");
            Assert.That(content, Does.Contain("List<T> GetAllVerticies()"), $"{filename} should contain T[] GetAllElements().");
            Assert.That(content, Does.Contain("int GetTotalWeight()"), $"{filename} should contain declaration IComparable.");
            Assert.That(content, Does.Contain("(int, List<(T, int)>) GetShortestPath(T fromvertex, T tovertex)"), $"{filename} should contain declaration IComparable.");
        });

        TestContext.Out.WriteLine($"✅ {filename} innehåller rätt metoddeklarationer ({KMOM}).");
    }
}
