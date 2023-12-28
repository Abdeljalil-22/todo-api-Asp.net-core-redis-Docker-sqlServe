var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DemoDocker_API>("demodocker.api");

builder.AddProject<Projects.DemoDocker_User_Api>("demodocker.user.api");

builder.Build().Run();
