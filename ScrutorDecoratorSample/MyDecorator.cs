using Shares;

namespace ScrutorDecoratorSample
{
    public class MyDecorator: ISpeakService
    {
        private readonly ISpeakService _target;

        public MyDecorator(ISpeakService target)
        {
            _target = target;
        }

        public void Say()
        {
            Console.WriteLine("咳咳咳");
            _target.Say();
            Console.WriteLine("谢谢");
        }
    }
}
