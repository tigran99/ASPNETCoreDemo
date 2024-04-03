namespace ASPNETCoreDemo.Services
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> logger;

        public MyService(ILogger<MyService> logger)
        {
            this.logger = logger;
        }

        public void MyMethod() 
        {
            logger.LogError("MyMethod is called");
        }
    }
}
