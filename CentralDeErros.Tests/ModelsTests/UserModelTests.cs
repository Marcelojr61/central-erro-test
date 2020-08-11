using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.ModelsTests
{
    public class UserModelTests : BaseModelTest
    {
        public UserModelTests(CentralDeErrosDbContext context) : base(context)
        {
        }

        [Fact(DisplayName = "User should not be null")]
        public void UserShouldNotBeNull()
        {
            
            var users = context.Users.ToList(); 
            
            foreach (var user in users)  Assert.NotNull(user);
                                 
        }       
    }
}
