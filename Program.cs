using ExampleService;

var host = Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .Build();

host.Run();