namespace ExampleService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;

    public Worker(ILogger<Worker> logger)
    {
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Hello Worker! {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}