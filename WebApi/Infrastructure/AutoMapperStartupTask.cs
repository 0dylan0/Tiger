using Core.Infrastructure;
namespace WebApi.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            AutoMapperConfiguration.Init();
        }

        public int Order
        {
            get
            {
                return 2;
            }
        }
    }
}