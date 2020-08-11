using CentralDeErros.Core;


namespace CentralDeErros.ModelsTests
{
    public class BaseModelTest
    {
        public readonly CentralDeErrosDbContext context;

        public BaseModelTest(CentralDeErrosDbContext context)
        {
            this.context = context;
        }
    }
}
