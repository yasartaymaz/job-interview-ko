using Business.Abstract;
using System;

namespace UIConsole
{
    class Program
    {
        private readonly IAccountService _accountService;

        public Program(IAccountService accountService)
        {
            _accountService = accountService;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
