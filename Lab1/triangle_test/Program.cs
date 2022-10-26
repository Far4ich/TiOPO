using System.Diagnostics;

StreamReader tests = new StreamReader("../../../Tests.txt");
StreamWriter testResults = new StreamWriter("../../../TestResults.txt", false);

string line;
List<string> allParams = new();

while (!tests.EndOfStream)
{
    line = tests.ReadLine();
    if (line != null)
    {
        allParams.Add(line);
    }
}
tests.Close();

string exeFilePath = "../../../../triangle/bin/Debug/net6.0/triangle.exe";

System.Diagnostics.Process process = new System.Diagnostics.Process();

foreach (string paramStrgs in allParams)
{
    string expectedResult = paramStrgs.Split("; ").Last() + "\r\n";
    string param = paramStrgs.Substring(0, paramStrgs.LastIndexOf(";"));

    process.StartInfo = new System.Diagnostics.ProcessStartInfo(exeFilePath, param);
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.UseShellExecute = false;
    process.Start();

    string result = "";
    while (!process.HasExited)
    {
        result += process.StandardOutput.ReadToEnd();
    }

    if (expectedResult == result)
    {
        testResults.WriteLine("sucсess;");
    }
    else
    {
        testResults.WriteLine("error;");
    }

    process.Close();
}

testResults.Close();