using CentralDeErros.ControllersTests;
using CentralDeErros.Core;


namespace CentralDeErros.ControllerTest
{
    public class BaseControllerTest
    {
        public readonly FakeContext context;

        public BaseControllerTest()
        {
            context = new FakeContext();
        }
    }
}
