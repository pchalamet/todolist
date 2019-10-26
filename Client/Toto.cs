using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    public class Toto
    {
        public Toto(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string SayHello()
        {
            return $"Hello {Name}";
        }

        public void AlwaysFails()
        {
            throw new ArgumentException("Toto");
        }

        public async Task<string> SayHelloAsync()
        {
            await Task.Delay(1000);
            return SayHello();
        }
    }
}
