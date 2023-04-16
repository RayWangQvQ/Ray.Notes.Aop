using Shares;

namespace ScrutorDecoratorSample
{
    public class MyDecorator: ISpeakService
    {
        private readonly ISpeakService _target;
        private readonly ILogger<MyDecorator> _logger;

        public MyDecorator(ISpeakService target, ILogger<MyDecorator> logger)
        {
            _target = target;
            _logger = logger;
        }

        public void Say()
        {
            Console.WriteLine("咳咳咳");
            _target.Say();
            Console.WriteLine("谢谢");
        }
    }
}
