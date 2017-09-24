using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI.Services
{
    using Abstraction;

    public class TestService : ITestService
    {
        public string Test()
        {
            return "It works!";
        }
    }
}
