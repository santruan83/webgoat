This is a simple C# program that contains SQL injection vulnerabilities and Path manipulation. You must have .NetFramewor k4.8 and MSBuild 16.4 or Visual Studio 2019 installed (and optionally the Fortify extension for Visual Studio 2019).

You can translate and scan the solution from the command line:
From the Windows command prompt, change to this directory (VS2019\.NetFramework4.8\Sample1), and then run the following commands:
  $ sourceanalyzer -b cs-sample -clean
  $ sourceanalyzer -b cs-sample msbuild /t:rebuild Sample1.sln
  $ sourceanalyzer -b cs-sample -scan

Alternatively, if you have Visual Studio 2019 installed you can translate and scan the solution from the Developer Command Prompt:
1. Start a Developer Command Prompt for Visual Studio 2019 window.  
2. Change to this directory (VS2019\.NetFramework4.8\Sample1) and run the following commands:
  $ sourceanalyzer -b cs-sample -clean
  $ sourceanalyzer -b cs-sample devenv Sample1.sln /REBUILD Debug
  $ sourceanalyzer -b cs-sample -scan

If you have the Fortify Extension for Visual Studio 2019 installed, you can translate and scan the solution from Visual Studio:
1. In Visual Studio 2019, open Sample1\Sample1.sln.
2. Select Extensions > Fortify > Analyze Solution.

After successful completion of the scan, you should see:
- the SQL Injection vulnerabilities 
- Unreleased Resource vulnerability
- Path Manipulation 
- Other categories might also be present, depending on the Fortify Rulepacks used in the scan.
The first SQL Injection vulnerability is reported for SqlDataAdapter object created with an argument to the main function.
The second vulnerability shows the tainted data flow through String concatenation and cloning to a call to create a SqlDataAdapter.
The Unreleased Resource vulnerability is reported for the SQLConnection object held by the variable 'conn'. The program initializes this variable with a SQLConnection resource, but never calls the 'Dispose' method on the object. According to the C# API documentation, you should call the 'Dispose' method on these type of objects so that resources are released properly.
The Path manipulation is reported at attempting to create directory with system path argument.
