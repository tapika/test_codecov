System.Console.WriteLine($"{{ACTIONS_RUNTIME_URL}}={{{System.Environment.GetEnvironmentVariable("ACTIONS_RUNTIME_URL")}}}");
System.Console.WriteLine($"{{ACTIONS_RUNTIME_TOKEN}}={{{System.Environment.GetEnvironmentVariable("ACTIONS_RUNTIME_TOKEN")}}}");
System.Environment.Exit(0);