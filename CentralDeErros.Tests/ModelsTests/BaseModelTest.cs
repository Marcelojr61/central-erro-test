using CentralDeErros.Core;


namespace CentralDeErros.ModelsTests
{
    public class BaseModelTest
    {
        public readonly FakeContext context;

        public BaseModelTest()
        {
            context = new FakeContext();
        }
    }
}
