namespace Searching;
using Searching;
using System.Reflection;
using System.Threading.Tasks;

[TestFixture]
public class FileContentTest
{
    private const string ORDERED_WORDS_999 = @"orderedWords999.txt";
    private const string ORDERED_WORDS_1200 = @"orderedWords1200.txt";
    private const string WORDS_999 = @"words999.txt";
    private const string WORDS_1200 = @"words1200.txt";
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
            _targetDir = Path.Combine(workflowDir, "kmom01", "Searching") ?? "";
        }
        else
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var rootDir = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", "..", ".."));
            this._targetDir = Path.Combine(rootDir, "kmom01", "Searching") ?? "";
        }
    }

    [TestCase(ORDERED_WORDS_999, 999)]
    [TestCase(ORDERED_WORDS_1200, 1200)]
    [TestCase(WORDS_999, 999)]
    [TestCase(WORDS_1200, 1200)]
    public async Task TestReadLinesInFileCorrect(string filename, int noOfLines)
    {
        var filePath = Path.Combine(this._targetDir, filename);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");

        var content = await File.ReadAllLinesAsync(filePath);
        string message = $"❌ Test that the file {filename} can read 999 words.";

        Assert.That(content, Is.Not.Empty, "❌ Content read from file {ORDERED_WORDS_999} should not be empty.");
        Assert.That(content, Has.Length.EqualTo(noOfLines), message);
        TestContext.Out.WriteLine($"✅ Filen {filename} går att läsa och har {noOfLines} ord.");
    }

    [TestCase(ORDERED_WORDS_999, WORDS_999)]
    [TestCase(ORDERED_WORDS_1200, WORDS_1200)]
    public async Task TestWordFilesAreAlikeSortedCorrect(string filenameSorted, string filename)
    {
        var filePath = Path.Combine(this._targetDir, filename);
        var filePathSorted = Path.Combine(this._targetDir, filenameSorted);

        Assert.That(File.Exists(filePath), Is.True, $"Filen saknas: {filePath}");
        Assert.That(File.Exists(filePathSorted), Is.True, $"Filen saknas: {filePathSorted}");

        var words = await File.ReadAllLinesAsync(filePath);
        Array.Sort(words);
        var wordsSorted = await File.ReadAllLinesAsync(filePathSorted);
        string message = $"❌ The files {filename} and {filenameSorted} should be alike.";

        Assert.That(words, Is.Not.Empty, "❌ The file {filename} should not be empty.");
        Assert.That(wordsSorted, Is.Not.Empty, "❌ The file {filenameSorted} should not be empty.");
        Assert.That(wordsSorted, Is.EqualTo(words), message);
        TestContext.Out.WriteLine($"✅ Filen {filename} sorterad och {filenameSorted} är lika.");
    }
}
