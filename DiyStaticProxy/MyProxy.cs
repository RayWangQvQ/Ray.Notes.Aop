using Shares;

namespace DiyStaticProxy
{
    public class MyProxy: ISpeakService
    {
        private readonly SpeakService _targetService;

        public MyProxy()
        {
            _targetService = new SpeakService();
        }

        public void Say()
        {
            Console.WriteLine("咳咳咳");
            _targetService.Say();
            Console.WriteLine("感谢大家");
        }
    }
}
